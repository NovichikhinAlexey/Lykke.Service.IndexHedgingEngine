using System;
using JetBrains.Annotations;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.IndexHedgingEngine.Domain;

namespace Lykke.Service.IndexHedgingEngine.AzureRepositories.Trades
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateIfDirty)]
    public class InternalTradeEntity : AzureTableEntity
    {
        private TradeType _type;
        private DateTime _date;
        private decimal _price;
        private decimal _volume;
        private TradeStatus _status;
        private decimal _oppositeVolume;

        public InternalTradeEntity()
        {
        }

        public InternalTradeEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string Id { get; set; }

        public string LimitOrderId { get; set; }

        public string ExchangeOrderId { get; set; }

        public string AssetPairId { get; set; }

        public TradeType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public decimal Volume
        {
            get => _volume;
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public TradeStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public decimal OppositeVolume
        {
            get => _oppositeVolume;
            set
            {
                if (_oppositeVolume != value)
                {
                    _oppositeVolume = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public string OppositeWalletId { get; set; }

        public string OppositeLimitOrderId { get; set; }
    }
}
