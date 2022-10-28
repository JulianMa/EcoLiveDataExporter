using System;
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
                try
                {
                    string filePath = $"WebClient/WebBin/data/{fileName}.json";
                    Logger.Debug($"Saving file {fileName} to disk.");

                    await using (FileStream fs = File.Create(filePath))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
                        fs.Write(info, 0, info.Length);
                    }
                    
                    Logger.Debug($"File {fileName} successfully written to disk.");
                }
                catch (Exception e)
                {
                    Logger.Error($"Handled an exception trying to save a file to disk with stacktrace: \n {e}");
                }
            }
        }

        public static async Task AppendToFile(string fileName, string dataString)
        {
            if(Config.Data.SaveFilesLocally)
            {
                try
                {
                    string filePath = $"WebClient/WebBin/data/{fileName}.csv";
                    Logger.Debug($"Appending data to file: {fileName} on disk.");

                    await using (FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    await using (StreamWriter sw = new StreamWriter(aFile)) {
                        sw.WriteLine(dataString);
                    }
                    
                    Logger.Debug($"Data was successfully append to file {fileName} on disk.");
                }
                catch (Exception e)
                {
                    Logger.Error($"Handled an exception trying to append to the file: {fileName} with stacktrace: \n {e}");
                }
            }
        }

        public static string[] ReadFileLines(string fileName){
            if(fileName == null) return null;
            var filePath = $"WebClient/WebBin/data/{fileName}.csv";
            if(File.Exists(filePath))
            {
                try
                {
                    var allLines =  File.ReadAllLines(filePath);
                    Logger.Debug($"Data was successfully read from file: {fileName}.");
                    return allLines;
                }
                catch (Exception e)
                {
                    Logger.Error($"Handled an exception trying to read from file: {fileName} with stacktrace: \n {e}");
                }
            }
            return null;
        }
    }
}
