using Eco.Gameplay.Components;
using Eco.Gameplay.Items;

using System.Linq;
using System.Collections.Generic;
using System.Text;
using Eco.Gameplay.Objects;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    // Utils imported from fmz repo
    public class FMZUtils
    {
        public static List<string> GetTablesForRecipe(RecipeFamily recipe)
        {
            return CraftingComponent.TablesForRecipe(recipe.GetType()).Select(type => GetTableNameFromUILink(WorldObject.UILink(type, false))).ToList();
        }

        public static string GetTableNameFromUILink(string uiLink)
        {
            int startIndex1 = uiLink.IndexOf("</");
            int startIndex2 = uiLink.LastIndexOf(">", startIndex1) + 1;
            return uiLink.Substring(startIndex2, startIndex1 - startIndex2);
        }
    }
}
