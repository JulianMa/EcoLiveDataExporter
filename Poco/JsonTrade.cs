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
        public string Style { get; set; }
        public string ShopName { get; set; }
        public string ShopOwner { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public float NumberOfItems { get; set; }
        public bool IsBuyAction { get; set; }
        public string ItemTraded { get; set; }
        public float CurrencyAmount { get; set; }
        public string CurrencyName { get; set; }
        public string SourceBankAccount { get; set; }
        public string TargetBankAccount { get; set; }
        public string Reason { get; set; }
        public int Time { get; set; }

        public JsonTrade() { }

        public JsonTrade(CurrencyTrade trade)
        {
            OccurredAt = new JsonDateTime(DateTime.UtcNow);
            Style = "CurrencyTrade";
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

        public JsonTrade(BarterTrade trade)
        {
            OccurredAt = new JsonDateTime(DateTime.UtcNow);
            Style = "BarterTrade";
            ShopName = trade.WorldObjectItem?.Name;
            ShopOwner = trade.ShopOwner?.Name;
            BuyerName = trade.Buyer?.Name;
            SellerName = trade.Seller?.Name;
            NumberOfItems = trade.NumberOfItems;
            IsBuyAction = trade.BoughtOrSold == BoughtOrSold.Buying;
            ItemTraded = trade.ItemUsed?.DisplayName;
            Time = trade.Time;
        }

        public JsonTrade(TransferMoney trade)
        {
            OccurredAt = new JsonDateTime(DateTime.UtcNow);
            Style = "TransferMoney";
            SourceBankAccount = trade.SourceBankAccount.Name;
            TargetBankAccount = trade.TargetBankAccount.Name;
            BuyerName = trade.Citizen?.Name;
            SellerName = trade.Receiver?.Name;
            NumberOfItems = trade.Count;
            CurrencyAmount = trade.CurrencyAmount;
            CurrencyName = trade.Currency?.Name;
            Reason = trade.Reason.ToString();
            Time = trade.Time;
        }

        override public string ToString(){
            string trade = null;
            trade = $"OccurredAt: {OccurredAt.StringRepresentation}, Style: {Style}, ShopName: {ShopName}, ShopOwner: {ShopOwner}, BuyerName: {BuyerName}, SellerName: {SellerName}, NumberOfItems: {NumberOfItems}, IsBuyAction: {IsBuyAction}, ItemTraded: {ItemTraded}, CurrencyAmount: {CurrencyAmount}, CurrencyName: {CurrencyName}, SourceBankAccount: {SourceBankAccount}, TargetBankAccount: {TargetBankAccount}, Reason: {Reason}, Time: {Time}";
            return trade;

        }
    }
}
