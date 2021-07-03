using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Plugins.EcoLiveDataExporter.Poco;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class RecipeUtil
    {

        public static string GetRecipesString()
        {
            try
            {
                var allRecipes = RecipeFamily.AllRecipes;
                Logger.Debug("Started collecting recipe information: " + allRecipes.Length);
                var recipesObjects = allRecipes.Select(t => new JsonRecipe(t)).ToList();
                var recipesJson = JsonConvert.SerializeObject(recipesObjects);
                Logger.Debug("Got recipes string");
                return recipesJson;
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to export recipes: \n {e}");
                return null;
            }
        }

        public static string GetCraftableTagItems()
        {
            try
            {
                var allRecipes = RecipeFamily.AllRecipes;
                Logger.Debug("Started collecting recipe tags information: " + allRecipes.Length);
                var craftableTags = allRecipes.SelectMany(t => t.Recipes.SelectMany(t => t.Ingredients.Select(t => t.Tag))).Where(t => t?.DisplayName != null).Distinct();
                Logger.Debug("Got craftable tags: " + craftableTags.Count());
                Logger.Debug("null tags: " + craftableTags.Where(t => t?.DisplayName == null).Count());

                var tags = craftableTags.ToDictionary(t => t?.DisplayName.NotTranslated, t => t?.TaggedItems().Select(t => t.DisplayName.NotTranslated));
                var tagsJson = JsonConvert.SerializeObject(tags);

                Logger.Debug("Got craftable tag items string");
                return tagsJson;
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to export item tags: \n {e}");
                return null;
            }
        }

        public static string GetCraftingTablesString()
        {
            try
            {
                var allCraftingTables = WorldObjectUtil.AllObjsWithComponent<CraftingComponent>();
                Logger.Debug("Started collecting crafting tables information: " + allCraftingTables.Count());
                var tablesToExport = new JsonHistCraftingTables(allCraftingTables);
                var craftingTablesString = JsonConvert.SerializeObject(tablesToExport);
                Logger.Debug("Got crafting tables string");
                return craftingTablesString;
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to export crafting tables: \n {e}");
                return null;
            }
        }
    }
}
