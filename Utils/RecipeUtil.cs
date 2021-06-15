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
    }
}
