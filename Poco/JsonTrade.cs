using Eco.Gameplay.GameActions;
using Eco.Shared.Items;

using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonTrade
    {
        public string ShopName { get; set; }
        public string ShopOwner { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public float NumberOfItems { get; set; }
        public bool IsBuyAction { get; set; }

        public JsonTrade() { }

        public JsonTrade(TradeAction trade)
        {
            ShopName = trade.WorldObjectItem.Name;
            ShopOwner = trade.ShopOwner.Name;
            BuyerName = trade.Buyer.Name;
            SellerName = trade.Seller.Name;
            NumberOfItems = trade.NumberOfItems;
            IsBuyAction = trade.BoughtOrSold == BoughtOrSold.Buying;
        }
    }
}
