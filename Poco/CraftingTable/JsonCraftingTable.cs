using Eco.Gameplay.Components;
using Eco.Gameplay.Modules;
using Eco.Plugins.EcoLiveDataExporter.Utils;
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

        public float genericMultiplier { get; set; } = 1;

        public float skillMultiplier { get; set; } = 1;

        public JsonCraftingTable (CraftingComponent component)
        {
            TableName = component?.Parent?.DisplayName;
            ResourceEfficiencyModule = component?.ResourceEfficiencyModule?.DisplayName;
            
            OwnerName = component?.Parent?.Owners?.Name;
            ModuleLevel = 0;

            if (null == (component?.ResourceEfficiencyModule))
            {
                return;
            }

            /**
            * Typecast to EfficiencyModule, will be null when not possible.
            * The MetaData states, that the return Type is PluginModule. My Guess is, that this will always be an EfficencyModule and the Type is just there for future? changes.
            * All Upgrades i looked into in `__core__\AutoGen\PluginModule` are Implemented as EfficiencyModule.
            */
            EfficiencyModule efficencyModule = component?.ResourceEfficiencyModule as EfficiencyModule;
            if (efficencyModule != null)
            {
                genericMultiplier = efficencyModule.GenericMultiplier;
                // On Unspecific Modules, like BU1-BU4, this is always 1, so we set it to the minimum of both.
                skillMultiplier = Math.Min(efficencyModule.SkillMultiplier, efficencyModule.GenericMultiplier);

                float[] multiplierByLevel = new float[] { 0.9f, 0.75f, 0.6f, 0.55f, 0.5f };

                ModuleLevel = Array.FindIndex(multiplierByLevel, (modifier) => modifier == skillMultiplier) + 1;
            }
            else
            {
                Logger.Info("PluginModule is not typeOf EfficiencyModule: " + component?.Name);
            }
        }
    }
}
