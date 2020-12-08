using Eco.Gameplay.Components;

using System.Collections.Generic;
using System.Linq;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonStore
    {
        public List<JsonTradeOffer> AllOffers { get; set; }
        public float Balance { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameData { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        //public List<JsonTradeOffer> BuyOffers { get; set; }
        //public List<JsonTradeOffer> SellOffers { get; set; }
        public JsonStore(StoreComponent storeComp)
        {
            AllOffers = storeComp.AllOffers.Select(t => new JsonTradeOffer(t)).ToList();
            Balance = storeComp.Balance;
            CurrencyName = storeComp.CurrencyName;
            Enabled = storeComp.Enabled;
            //storeComp.ItemsInStock
            Name = storeComp.Name;
            //BuyOffers = storeComp.StoreData.BuyOffers.Select(t => new JsonTradeOffer(t)).ToList();
            //SellOffers = storeComp.StoreData.SellOffers.Select(t => new JsonTradeOffer(t)).ToList();
            CurrencyNameData = storeComp.StoreData.Currency.Name;
        }
    }
}
