using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Store;
using Eco.Plugins.EcoLiveDataExporter.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonStore
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public float? Balance { get; set; }
        public string CurrencyName { get; set; }
        public bool Enabled { get; set; }
        public List<JsonTradeOffer> AllOffers { get; set; }

        public JsonStore(StoreComponent storeComp)
        {
            Name = storeComp?.Parent?.Name;
            Owner = storeComp?.Parent?.Owners?.Name;
            try
            {
                Balance = storeComp?.Balance;
                CurrencyName = storeComp?.CurrencyName;
            }
            catch (System.Exception ex)
            {
                Logger.Debug($"There was a handled exception trying to read store {Name}: {ex.Message} \n{ex.StackTrace}");
            }
            Enabled = storeComp?.Enabled ?? false;
            AllOffers = storeComp?.AllOffers?.Select(t => new JsonTradeOffer(t))?.ToList();
        }
    }
}
