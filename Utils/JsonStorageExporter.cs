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

        private static async Task<JsonConfigFile> GetJsonConfigFile()
        {
            Logger.Debug($"Reading config data");
            var configResult = await BuildUrlAndSendRequest(Config.Data.JsonStorageId, "", GetAsyncHelper);
            if (configResult != null && configResult.IsSuccessStatusCode)
            {
                try
                {
                    var jsonString = await configResult?.Content?.ReadAsStringAsync();
                    Logger.Debug($"Got config: : {jsonString}");
                    var config = JsonConvert.DeserializeObject<JsonConfigFile>(jsonString);
                    Logger.Debug($"Managed to deserialize config file object! Got {config.Dbs.Count} dbs!");
                    return config;
                }
                catch (Exception e)
                {
                    Logger.Error($"Handled an exception trying to interpret config with stacktrace: \n {e}");
                }
            }

            return new JsonConfigFile() { Dbs = new List<JsonConfigDb>() };
        }

        private static List<JsonConfigDb> AddDbToList(List<JsonConfigDb> existingDbs, JsonConfigDb newDb)
        {
            var newDbList = existingDbs.Where(db => db.Name != newDb.Name).ToList();
            newDbList.Add(newDb);
            return newDbList;
        }

        // Updates or create db file and returns updated list of dbs
        public static async Task<List<JsonConfigDb>> ReplaceJsonFile(string fileName, string jsonString, List<JsonConfigDb> existingDbs)
        {
            var db = existingDbs.Find(db => db.Name == fileName);
            if (db != null)
            {
                Logger.Debug($"Found bin for db {fileName}: {db.Bin}. Updating it now.");
                var putResponse = await BuildUrlAndSendRequest(db.Bin, jsonString, client.PutAsync);
                if (putResponse != null) {
                    return AddDbToList(existingDbs, db.UpdateFileNow());
                }
            }
            else
            {
                Logger.Debug($"Could not find bin for db {fileName}. Creating it now.");
                var postResponse = await BuildUrlAndSendRequest("", jsonString, client.PostAsync);
                if (postResponse != null)
                {
                    var newFileString = await postResponse?.Content?.ReadAsStringAsync();
                    Logger.Debug($"Created new bin for file {fileName} with string result: {newFileString}");
                    dynamic newFileObj = JsonConvert.DeserializeObject(newFileString);
                    var newBinUrl = Convert.ToString(newFileObj?.uri);
                    Logger.Debug($"Got new file uri: {newBinUrl}");
                    var newBin = newBinUrl.Replace("https://api.jsonstorage.net/v1/json/", "");
                    Logger.Debug($"New bin for file {fileName}: {newBin}");

                    return AddDbToList(existingDbs, new JsonConfigDb(fileName, newBin));
                }
            }
            return null;
        }

        // Main file on this class! Gets config (db list), updates the file with provided content and finally updates the config.
        // When appendFileInsteadOfReplace is true, the existing file is read and the updated
        public static async Task WriteToJsonStorage(string fileName, string jsonString)
        {
            if (Config.Data.JsonStorageId == null || Config.Data.JsonStorageId.Length == 0)
                return;

            var config = await GetJsonConfigFile();
            var updatedDbs = await ReplaceJsonFile(fileName, jsonString, config.Dbs);
            if (updatedDbs != null)
            {
                // Update config file
                Logger.Debug($"Finishing by updating config object!");
                var updatedConfig = new JsonConfigFile() { Dbs = updatedDbs };
                await BuildUrlAndSendRequest(Config.Data.JsonStorageId, JsonConvert.SerializeObject(updatedConfig), client.PutAsync);
            }
        }

        private static Task<HttpResponseMessage> GetAsyncHelper(string url, HttpContent _)
        {
            return client.GetAsync(url);
        }

        private static Task<HttpResponseMessage> BuildUrlAndSendRequest(string fileBin, string jsonString, Func<string, HttpContent, Task<HttpResponseMessage>> sendAsyncRequest)
        {
            var url = $"https://api.jsonstorage.net/v1/json/{fileBin}";
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
