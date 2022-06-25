using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eco.Gameplay.Items;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonItem
    {
        public IEnumerable<string> Tags { get; set; }

        public float Fuel { get; set; }

        public JsonItem(Item item)
        {
            Tags = item.TagNames(false);
            Fuel = item.Fuel;
        }
    }
}
