using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Plugins.EcoLiveDataExporter.Poco;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using Eco.Gameplay.Items.Recipes;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class RecipeUtil
    {

        public static string GetRecipesString()
        {
            try
            {
                Logger.Debug("Started collecting recipe information: " + RecipeManager.AllRecipeFamilies.Length);
                var recipes = new JsonRecipes(RecipeManager.AllRecipeFamilies);
                var recipesJson = JsonConvert.SerializeObject(recipes);
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
                var allRecipes = RecipeManager.AllRecipeFamilies;
                Logger.Debug("Started collecting recipe tags information: " + allRecipes.Length);
                var craftableTags = allRecipes.SelectMany(t => t.Recipes.SelectMany(t => t.Ingredients.Select(t => t.Tag))).Where(t => t?.DisplayName != null).Distinct();
                Logger.Debug("Got craftable tags: " + craftableTags.Count());
                Logger.Debug("null tags: " + craftableTags.Where(t => t?.DisplayName == null).Count());

                var tags = new JsonTags(craftableTags);
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

        public static string GetAllItems()
        {
            try
            {
                Logger.Debug("Started collecting all items information");

                var items = new JsonItems();
                var itemsJson = JsonConvert.SerializeObject(items);

                Logger.Debug("Got items string");
                return itemsJson;
            }
            catch (Exception e)
            {
                Logger.Error($"Got an exception trying to export all items: \n {e}");
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
