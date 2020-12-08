using Eco.Gameplay.Items;

using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonTradeOfferStack
    {
        public float FractionalQuantity { get; set; }
        public JsonItem Item { get; set; }
        public int Quantity { get; set; }
        public string TypeString { get; set; }
        public float Weight { get; set; }
        public JsonTradeOfferStack(ItemStack itemStack)
        {
            FractionalQuantity = itemStack.FractionalQuantity;
            Item = new JsonItem(itemStack.Item);
            Quantity = itemStack.Quantity;
            TypeString = itemStack.TypeString;
            Weight = itemStack.Weight;
        }
    }
}
