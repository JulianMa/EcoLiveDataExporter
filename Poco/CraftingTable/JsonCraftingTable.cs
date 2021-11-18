using Eco.Gameplay.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonCraftingTable
    {
        public string TableName { get; set; }
        public string ResourceEfficiencyModule { get; set; }
        public string OwnerName { get; set; }
        //public List<string> Benefits { get; set; }
        public int ModuleLevel { get; set; }
        public JsonCraftingTable (CraftingComponent component)
        {
            TableName = component?.Parent?.DisplayName;
            ResourceEfficiencyModule = component?.ResourceEfficiencyModule?.DisplayName;
            OwnerName = component?.Parent?.Owners?.Name;
            ModuleLevel = 0;
            //Benefits = component?.ResourceEfficiencyModule?.Benefits.Select(t => t.NotTranslated).ToList();

            //Get the 25% string out of the string "Decreases resource cost for the table work orders by <style=\"Positive\">25%</style>."
            //Level 5 is currently never fetched, since it will not work for all recipes in the table
            var ResourceBenefitString = component?.ResourceEfficiencyModule?.Benefits?.Select(t => t.NotTranslated).Where(t => t.IndexOf("Decreases resource cost for the table work orders by") >= 0).FirstOrDefault();
            if (ResourceBenefitString == null) { return; }
            if (ResourceBenefitString.IndexOf("10%") > 0)
            {
                ModuleLevel = 1;
            } else if (ResourceBenefitString.IndexOf("25%") > 0)
            {
                ModuleLevel = 2;
            }
            else if (ResourceBenefitString.IndexOf("40%") > 0)
            {
                ModuleLevel = 3;
            }
            else if (ResourceBenefitString.IndexOf("45%") > 0)
            {
                ModuleLevel = 4;
            }
        }
    }
}
