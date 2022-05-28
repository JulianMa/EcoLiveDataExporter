using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class LocalFileExporter
    {
        // Overrites file on disk
        public static async Task WriteToFile(string fileName, string jsonString)
        {
            // Only send request when the secretKey is configured
            if (Config.Data.SaveFilesLocally)
            {
                string filePath = $"WebClient/WebBin/data/{fileName}.json";
                Logger.Debug($"Saving file {fileName} to disk.");
                await using (FileStream fs = File.Create(filePath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
                    fs.Write(info, 0, info.Length);
                }
            }
        }
    }
}
