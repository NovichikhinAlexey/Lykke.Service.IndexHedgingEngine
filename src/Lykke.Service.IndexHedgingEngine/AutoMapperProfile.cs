using AutoMapper;
using JetBrains.Annotations;
using Lykke.Service.IndexHedgingEngine.Client.Models;
using Lykke.Service.IndexHedgingEngine.Client.Models.AssetHedgeSettings;
using Lykke.Service.IndexHedgingEngine.Client.Models.AssetLinks;
using Lykke.Service.IndexHedgingEngine.Client.Models.Audit;
using Lykke.Service.IndexHedgingEngine.Client.Models.Balances;
using Lykke.Service.IndexHedgingEngine.Client.Models.Funding;
using Lykke.Service.IndexHedgingEngine.Client.Models.HedgeLimitOrders;
using Lykke.Service.IndexHedgingEngine.Client.Models.IndexSettings;
using Lykke.Service.IndexHedgingEngine.Client.Models.IndexPrices;
using Lykke.Service.IndexHedgingEngine.Client.Models.OrderBooks;
using Lykke.Service.IndexHedgingEngine.Client.Models.Positions;
using Lykke.Service.IndexHedgingEngine.Client.Models.Reports;
using Lykke.Service.IndexHedgingEngine.Client.Models.Settings;
using Lykke.Service.IndexHedgingEngine.Client.Models.Tokens;
using Lykke.Service.IndexHedgingEngine.Client.Models.Trades;
using Lykke.Service.IndexHedgingEngine.Domain;

namespace Lykke.Service.IndexHedgingEngine
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AssetHedgeSettings, AssetHedgeSettingsModel>(MemberList.Source);
            CreateMap<AssetHedgeSettingsEditModel, AssetHedgeSettings>(MemberList.Destination)
                .ForMember(dest => dest.Approved, opt => opt.Ignore());

            CreateMap<AssetLink, AssetLinkModel>(MemberList.Source);
            CreateMap<AssetLinkModel, AssetLink>(MemberList.Destination);

            CreateMap<BalanceOperation, BalanceOperationModel>(MemberList.Source);
            CreateMap<BalanceOperationModel, BalanceOperation>(MemberList.Destination);

            CreateMap<Balance, BalanceModel>(MemberList.Source);

            CreateMap<Funding, FundingModel>(MemberList.Source);

            CreateMap<HedgeLimitOrder, HedgeLimitOrderModel>(MemberList.Source)
                .ForSourceMember(src => src.Context, opt => opt.Ignore());

            CreateMap<IndexPrice, IndexPriceModel>(MemberList.Source);
            CreateMap<AssetWeight, AssetWeightModel>(MemberList.Source);

            CreateMap<IndexSettings, IndexSettingsModel>(MemberList.Source);
            CreateMap<IndexSettingsModel, IndexSettings>(MemberList.Destination);

            CreateMap<LimitOrder, LimitOrderModel>(MemberList.Source);
            
            CreateMap<OrderBook, OrderBookModel>(MemberList.Source);

            CreateMap<Position, PositionModel>(MemberList.Source);
            
            CreateMap<AssetInvestment, AssetInvestmentModel>(MemberList.Destination);
            
            CreateMap<IndexReport, IndexReportModel>(MemberList.Source);
            
            CreateMap<PositionReport, PositionReportModel>(MemberList.Source);

            CreateMap<HedgeSettings, HedgeSettingsModel>(MemberList.Source);
            CreateMap<HedgeSettingsModel, HedgeSettings>(MemberList.Destination);

            CreateMap<TimersSettings, TimersSettingsModel>(MemberList.Source);
            CreateMap<TimersSettingsModel, TimersSettings>(MemberList.Destination);

            CreateMap<Token, TokenModel>(MemberList.Source);

            CreateMap<InternalTrade, InternalTradeModel>(MemberList.Source);

            CreateMap<VirtualTrade, VirtualTradeModel>(MemberList.Source);
            
            CreateMap<Quote, QuoteModel>(MemberList.Destination);
        }
    }
}