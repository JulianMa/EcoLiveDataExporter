using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Plugins.EcoLiveDataExporter.Utils;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonCraftingTable
    {
        public string TableName { get; set; }
        public string ResourceEfficiencyModule { get; set; }
        public string OwnerName { get; set; }
        public List<string> AllowedUpgrades { get; set; }
        public int ModuleLevel { get; set; }

        public float GenericMultiplier { get; set; } = 1;

        public float SkillMultiplier { get; set; } = 1;

        public JsonCraftingTable (CraftingComponent component)
        {
            TableName = component?.Parent?.DisplayName;
            ResourceEfficiencyModule = component?.ResourceEfficiencyModule?.DisplayName;
            
            OwnerName = component?.Parent?.Owners?.Name;
            ModuleLevel = 0;

            // Extraction works the same way it is done in Eco itself
            var allowedModuleStackables = ItemAttribute.Get<AllowPluginModulesAttribute>(component?.Parent.CreatingItem?.GetType())?.GetStackables()?.ToArray() ?? new[] { TagManager.GetTagOrFail("Upgrade") };
            AllowedUpgrades = (List<string>)allowedModuleStackables.Select(x => x.DisplayName.ToString()).AsList();

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
                GenericMultiplier = efficencyModule.GenericMultiplier;
                // On Unspecific Modules, like BU1-BU4, this is always 1, so we set it to the minimum of both.
                SkillMultiplier = Math.Min(efficencyModule.SkillMultiplier, efficencyModule.GenericMultiplier);

                float[] multiplierByLevel = new float[] { 0.9f, 0.75f, 0.6f, 0.55f, 0.5f };

                ModuleLevel = Array.FindIndex(multiplierByLevel, (modifier) => modifier == SkillMultiplier) + 1;
            }
            else
            {
                Logger.Info("PluginModule is not typeOf EfficiencyModule: " + component?.Name);
            }
        }
    }
}
