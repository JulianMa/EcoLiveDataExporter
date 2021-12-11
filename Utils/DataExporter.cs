using System;
using System.IO;
using System.IO.Compression;
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
            return BuildUrlAndSendRequest(file, path, jsonString, client.PutAsync);
        }
        public static Task AddToFile(string file, string path, string jsonString)
        {
            return BuildUrlAndSendRequest(file, path, jsonString, client.PatchAsync);
        }

        private static Task BuildUrlAndSendRequest(string file, string path, string jsonString, Func<string, HttpContent, Task<HttpResponseMessage>> sendAsyncRequest)
        {
            if (Config.Data.DbOutputAppDeprecated == null || Config.Data.DbOutputAppDeprecated.Length == 0)
            {
                Logger.Debug(@"DbOutputApp is not configured. Request not sent to {file}/{path}");
                return Task.CompletedTask;
            }
            var url = $"{Config.Data.DbOutputAppDeprecated}/{file}?path={path}";
            return SendJsonRequest(url, CompressRequestContent(jsonString), sendAsyncRequest);
        }

        private static HttpContent CompressRequestContent(string content)
        {
            var compressedStream = new MemoryStream();
            using (var contentStream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    contentStream.CopyTo(gzipStream);
                }
            }

            var byteArray = compressedStream.ToArray();
            Logger.Debug("byte array size: " + byteArray.Length);
            var httpContent = new ByteArrayContent(byteArray);
            httpContent.Headers.Add("Content-encoding", "gzip");
            httpContent.Headers.Add("Content-type", "application/gzip");
            return httpContent;
        }

        private static async Task SendJsonRequest(string url, HttpContent content, Func<string, HttpContent,Task<HttpResponseMessage>> sendAsyncRequest)
        {
            try
            {
                Logger.Debug($"Sending data to database on url: {url}");
                var asyncRequest = await sendAsyncRequest(url, content);
                Logger.Debug($"Write to url {url} completed with statusCode {asyncRequest.StatusCode} and response {asyncRequest.ReasonPhrase}");
            }
            catch(Exception e)
            {
                Logger.Error($"Handled an exception trying to execute request on url {url} with stacktrace: \n {e}");
            }
        }
    }
}
