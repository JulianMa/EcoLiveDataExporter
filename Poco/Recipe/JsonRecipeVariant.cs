using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonRecipeVariant
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public List<JsonRecipeIngredient> Ingredients { get; set; }
        public List<JsonRecipeProduct> Products { get; set; }
    }
}
