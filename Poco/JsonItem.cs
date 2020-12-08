using Eco.Gameplay.Items;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonItem
    {
        public string Name { get; set; }
        public bool CanBeCurrency { get; set; }
        public string CarriedDescription { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }
        public bool IsCarried { get; set; }
        public bool IsFuel { get; set; }
        public bool IsTool { get; set; }
        public bool IsUnique { get; set; }
        public bool MakesRoads { get; set; }
        public string MarkedUpName { get; set; }
        public int MaxStackSize { get; set; }
        public bool ResourcePile { get; set; }
        public string ItemName { get; set; }
        public int Weight { get; set; }
        public JsonItem(Item item)
        {
            Name = item.Name;
            CanBeCurrency = item.CanBeCurrency;
            CarriedDescription = item.CarriedDescription;
            Category = item.Category;
            Group = item.Group;
            IsCarried = item.IsCarried;
            IsFuel = item.IsFuel;
            IsTool = item.IsTool;
            IsUnique = item.IsUnique;
            MakesRoads = item.MakesRoads;
            MarkedUpName = item.MarkedUpName;
            MaxStackSize = item.MaxStackSize;
            ResourcePile = item.ResourcePile;
            ItemName = item.Type.Name;
            Weight = item.Weight;
        }
    }
}
