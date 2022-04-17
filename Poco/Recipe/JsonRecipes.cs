using Eco.Gameplay.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Poco
{
    public class JsonRecipes
    {
        public string PluginVersion { get; set; }
        public int Version { get; set; }
        public List<JsonRecipe> Recipes { get; set; }
        public JsonDateTime ExportedAt { get; set; }
        public JsonRecipes(RecipeFamily[] allRecipes)
        {
            PluginVersion = EcoLiveData.PluginVersion.ToString();
            Version = 2;
            Recipes = allRecipes.Select(t => new JsonRecipe(t)).ToList();
            ExportedAt = new JsonDateTime(DateTime.UtcNow);
        }
    }
}
