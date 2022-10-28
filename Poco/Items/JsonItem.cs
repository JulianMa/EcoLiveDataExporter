using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eco.Gameplay.Items;
using Eco.Shared.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonItem
    {
        public IEnumerable<string> Tags { get; set; }

        public float Fuel { get; set; }

        public Dictionary<string, Dictionary<string,string>> PropertyInfos { get; set; }
        public JsonItem(Item item)
        {
            Tags = item.TagNames(false);
            Fuel = item.Fuel;
            var PropertyInfosRaw = item?.GetType().GetProperties();
            PropertyInfos = PropertyInfosRaw.ToDictionary(x => x.Name, x => new Dictionary<string,string>{{x.PropertyType.Name, x.GetValue(item)?.ToString()}});
        }
    }
}
