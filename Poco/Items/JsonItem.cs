using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eco.Gameplay.Items;
using Eco.Plugins.EcoLiveDataExporter.Utils;
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
            try
            {
                Tags = item.TagNames(false);
                Fuel = item.Fuel;
                var PropertyInfosRaw = item?.GetType().GetProperties();
                PropertyInfos = PropertyInfosRaw.ToDictionary(x => x.Name, x => new Dictionary<string, string>(GetItemValue(x, item)));
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to get properties of item {item.DisplayName}: \n {e}");
            }
        }

        private KeyValuePair<string, string>[] GetItemValue(PropertyInfo propInfo, Item item)
        {
            try
            {
                return new[] { new KeyValuePair<string, string>(propInfo.PropertyType.Name, propInfo?.GetValue(item)?.ToString() ?? "") };
            }
            catch (Exception e)
            {
                return new[] { new KeyValuePair<string, string>(propInfo.PropertyType.Name, "<unknown>")};
            }
        }
    }
}
