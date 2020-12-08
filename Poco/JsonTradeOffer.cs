using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Eco.Gameplay.Components;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonTradeOffer
    {
        public bool Buying { get; set; }
        public bool HasMinDurability { get; set; }
        public bool IsSet { get; set; }
        public int Limit { get; set; }
        public int MaxNumWanted { get; set; }
        public float MinDurability { get; set; }
        public float Price { get; set; }

        public JsonTradeOfferStack ItemStack { get; set; }
        public JsonTradeOffer(TradeOffer tradeOffer)
        {
            Buying = tradeOffer.Buying;
            HasMinDurability = tradeOffer.HasMinDurability;
            IsSet = tradeOffer.IsSet;
            Limit = tradeOffer.Limit;
            MaxNumWanted = tradeOffer.MaxNumWanted;
            MinDurability = tradeOffer.MinDurability;
            Price = tradeOffer.Price;
            ItemStack = new JsonTradeOfferStack(tradeOffer.Stack);
        }
    }
}
