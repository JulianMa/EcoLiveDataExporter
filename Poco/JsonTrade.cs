using Eco.Gameplay.GameActions;
using Eco.Shared.Items;

using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonTrade
    {
        public JsonDateTime OccurredAt { get; set; }
        public string ShopName { get; set; }
        public string ShopOwner { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public float NumberOfItems { get; set; }
        public bool IsBuyAction { get; set; }
        public string ItemTraded { get; set; }
        public float CurrencyAmount { get; set; }
        public string CurrencyName { get; set; }
        public int Time { get; set; }

        public JsonTrade() { }

        public JsonTrade(CurrencyTrade trade)
        {
            OccurredAt = new JsonDateTime(DateTime.UtcNow);
            ShopName = trade.WorldObjectItem?.Name;
            ShopOwner = trade.ShopOwner?.Name;
            BuyerName = trade.Buyer?.Name;
            SellerName = trade.Seller?.Name;
            NumberOfItems = trade.NumberOfItems;
            IsBuyAction = trade.BoughtOrSold == BoughtOrSold.Buying;
            ItemTraded = trade.ItemUsed?.DisplayName;
            CurrencyAmount = trade.CurrencyAmount;
            CurrencyName = trade.Currency?.Name;
            Time = trade.Time;
        }
    }
}
