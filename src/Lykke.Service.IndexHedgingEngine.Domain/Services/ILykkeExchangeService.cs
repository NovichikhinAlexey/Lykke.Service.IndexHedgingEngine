using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.IndexHedgingEngine.Domain.Services
{
    public interface ILykkeExchangeService
    {
        Task ApplyAsync(string assetPairId, LimitOrder limitOrder);

        Task ApplyAsync(string assetPairId, IReadOnlyCollection<LimitOrder> limitOrders);

        Task CancelAsync(string assetPairId);

        Task<string> CashInAsync(string clientId, string assetId, decimal amount, string userId, string comment);

        Task<string> CashOutAsync(string clientId, string assetId, decimal amount, string userId, string comment);
    }
}
