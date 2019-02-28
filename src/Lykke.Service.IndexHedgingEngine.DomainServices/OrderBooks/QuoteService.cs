﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using JetBrains.Annotations;
using Lykke.Common.Log;
using Lykke.Service.IndexHedgingEngine.Domain;
using Lykke.Service.IndexHedgingEngine.Domain.Constants;
using Lykke.Service.IndexHedgingEngine.Domain.Handlers;
using Lykke.Service.IndexHedgingEngine.Domain.Services;
using Lykke.Service.IndexHedgingEngine.DomainServices.Extensions;

namespace Lykke.Service.IndexHedgingEngine.DomainServices.OrderBooks
{
    [UsedImplicitly]
    public class QuoteService : IQuoteService, IIndexHandler
    {
        private readonly IQuoteThresholdSettingsService _quoteThresholdSettingsService;
        private readonly IInstrumentService _instrumentService;
        private readonly ILog _log;
        private readonly InMemoryCache<Quote> _cache;
        private readonly InMemoryCache<Quote> _indicesPricesCache;
        private readonly HashSet<string> _exchanges = new HashSet<string>();

        public QuoteService(
            IQuoteThresholdSettingsService quoteThresholdSettingsService,
            IInstrumentService instrumentService,
            ILogFactory logFactory)
        {
            _quoteThresholdSettingsService = quoteThresholdSettingsService;
            _instrumentService = instrumentService;
            _cache = new InMemoryCache<Quote>(GetKey, true);
            _indicesPricesCache = new InMemoryCache<Quote>(GetKey, true);
            _log = logFactory.CreateLog(this);
        }

        public IReadOnlyCollection<Quote> GetAll()
        {
            return _cache.GetAll();
        }

        public IReadOnlyCollection<string> GetExchanges()
            => _exchanges.ToArray();

        public Quote GetByAssetPairId(string source, string assetPairId)
        {
            if (source == ExchangeNames.Virtual)
            {
                List<Quote> quotes = _indicesPricesCache.GetAll()
                    .Where(o => o.AssetPairId == assetPairId)
                    .ToList();

                if (!quotes.Any())
                    return null;

                decimal avgMid = quotes.Select(o => o.Mid).Average();

                return new Quote(assetPairId, quotes.Max(o => o.Time), avgMid, avgMid, ExchangeNames.Virtual);
            }

            return _cache.Get($"{source}-{assetPairId}");
        }

        public async Task UpdateAsync(Quote quote)
        {
            if (!await _instrumentService.IsAssetPairExistAsync(quote.AssetPairId))
                return;

            if (quote.Ask == 0 || quote.Bid == 0 || quote.Mid == 0)
            {
                _log.WarningWithDetails("Invalid quote received", quote);
                return;
            }

            Quote currentQuote = _cache.Get(GetKey(quote));

            if (currentQuote != null)
            {
                QuoteThresholdSettings quoteThresholdSettings = await _quoteThresholdSettingsService.GetAsync();

                if (quoteThresholdSettings.Enabled &&
                    Math.Abs(currentQuote.Mid - quote.Mid) / currentQuote.Mid > quoteThresholdSettings.Value)
                {
                    _log.WarningWithDetails("Invalid quote", new
                    {
                        Quote = quote,
                        CurrentQuote = currentQuote,
                        Threshold = quoteThresholdSettings.Value
                    });
                }
                else
                {
                    _exchanges.Add(quote.Source);
                    _cache.Set(quote);
                }
            }
            else
            {
                _exchanges.Add(quote.Source);
                _cache.Set(quote);
            }
        }

        public Task HandleIndexAsync(Index index)
        {
            foreach (var assetWeight in index.Weights)
            {
                Quote quote = new Quote($"{assetWeight.AssetId}USD", index.Timestamp, assetWeight.Price,
                    assetWeight.Price, index.Name);

                _indicesPricesCache.Set(quote);
            }

            return Task.CompletedTask;
        }

        private static string GetKey(Quote quote)
            => GetKey(quote.Source, quote.AssetPairId);

        private static string GetKey(string source, string assetPairId)
            => $"{source}-{assetPairId}";
    }
}
