﻿namespace Lykke.Service.IndexHedgingEngine.Domain.Settings
{
    /// <summary>
    /// Represents cross asset pair setting.
    /// </summary>
    public class CrossAssetPairSettings
    {
        /// <summary>
        /// The identifier of the index asset pair.
        /// </summary>
        public string IndexAssetPairId { get; set; }

        /// <summary>
        /// The name of the cross asset pair exchange.
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// The identifier of the cross asset pair.
        /// </summary>
        public string AssetPairId { get; set; }

        /// <summary>
        /// If cross asset pair must be inverted
        /// </summary>
        public bool IsInverted { get; set; }
    }
}