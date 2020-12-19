using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Eco.Gameplay.Components;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonTradeOffer
    {
        public string ItemName { get; set; }
        public bool Buying { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public int? Limit { get; set; }
        public int? MaxNumWanted { get; set; }
        public float? MinDurability { get; set; }

        public JsonTradeOffer(TradeOffer tradeOffer)
        {
            ItemName = tradeOffer?.Stack?.Item?.DisplayName;
            Buying = tradeOffer?.Buying ?? false;
            Price = tradeOffer?.Price;
            Quantity = tradeOffer?.Stack?.Quantity;
            Limit = tradeOffer?.Limit;
            MaxNumWanted = tradeOffer?.MaxNumWanted;
            MinDurability = tradeOffer?.MinDurability;
        }
    }
}
