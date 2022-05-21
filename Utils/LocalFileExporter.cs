using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class LocalFileExporter
    {
        // Overrites file on disk
        public static async Task WriteToFile(string fileName, string jsonString)
        {
            string filePath = $"WebClient/WebBin/Data/{fileName}.json";
            Logger.Debug($"Writting to {filePath}");
            await using FileStream createStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(createStream, jsonString);
        }
    }
}
