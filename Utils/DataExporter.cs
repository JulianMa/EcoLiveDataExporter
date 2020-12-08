using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class DataExporter
    {
        private static readonly HttpClient client = new HttpClient();
        
        public static Task WriteToFile(string file, string path, string jsonString) {
            var url = $"{Config.Data.DbOutputApp}/{file}?path={path}";
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return SendJsonRequest(url, content);
            
        }

        private static async Task SendJsonRequest(string url, HttpContent content)
        {
            try
            {
                Logger.Debug($"Uploading data to database on url: {url}");
                var asyncRequest = await client.PutAsync(url, content);
                Logger.Debug($"Write to url {url} completed with statusCode {asyncRequest.StatusCode} and response {asyncRequest.ReasonPhrase}");
            }
            catch(Exception e)
            {
                Logger.Error($"Handled an exception trying to execute request on url {url} with stacktrace: {e.StackTrace}");
            }
        }
    }
}
