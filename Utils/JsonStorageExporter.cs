using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Eco.Plugins.EcoLiveDataExporter.Poco;

using Newtonsoft.Json;

namespace Eco.Plugins.EcoLiveDataExporter.Utils
{
    public class JsonStorageExporter
    {
        private static readonly HttpClient client = new HttpClient();

        // Updates or create db file
        public static async Task WriteToJsonStorage(string fileName, string jsonString)
        {
            Logger.Debug($"Updating bin for db {fileName}.");
            await BuildUrlAndSendRequest(fileName, jsonString, client.PostAsync);
        }

        private static Task<HttpResponseMessage> BuildUrlAndSendRequest(string fileName, string jsonString, Func<string, HttpContent, Task<HttpResponseMessage>> sendAsyncRequest)
        {
            var url = $"https://getpantry.cloud/apiv1/pantry/{Config.Data.SecretKey}/basket/{fileName}";
            return SendJsonRequest(url, new StringContent(jsonString, Encoding.UTF8, "application/json"), sendAsyncRequest);
        }

        private static async Task<HttpResponseMessage> SendJsonRequest(string url, HttpContent content, Func<string, HttpContent,Task<HttpResponseMessage>> sendAsyncRequest)
        {
            try
            {
                Logger.Debug($"Sending data to database on url: {url}");
                var asyncRequest = await sendAsyncRequest(url, content);
                Logger.Debug($"Write to url {url} completed with statusCode {asyncRequest.StatusCode} and response {asyncRequest.ReasonPhrase}");
                return asyncRequest;
            }
            catch(Exception e)
            {
                Logger.Error($"Handled an exception trying to execute request on url {url} with stacktrace: \n {e}");
            }
            return null;
        }
    }
}
