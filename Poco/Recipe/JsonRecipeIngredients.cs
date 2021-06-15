using System;
using System.Collections.Generic;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonRecipeIngredient
    {
        public bool IsSpecificItem { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public float Ammount { get; set; }
        public bool IsStatic { get; set; }
    }
}
