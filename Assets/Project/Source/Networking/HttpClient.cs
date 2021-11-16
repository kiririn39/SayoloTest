using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Project.Source.Networking
{
    public static class HttpClient
    {
        private const string WrongContentTypeException = "Unexpected response content type";


        public static async Task<T> GetXmlAsAsync<T>(Uri uri, CancellationToken cancellationToken)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                using (var response = await client.GetAsync(uri, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();

                    if (!HttpContentType.Text.Equals(response.Content.Headers.ContentType.MediaType))
                        throw new HttpRequestException(WrongContentTypeException);

                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    {
                        var serializer = new XmlSerializer(typeof(T));
                        return (T) serializer.Deserialize(contentStream);
                    }
                }
            }
        }

        public static async Task<T> PostJsonAsAsync<T>(Uri uri, string content, CancellationToken cancellationToken)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                using (var json = new StringContent(content))
                {
                    using (var response = await client.PostAsync(uri, json, cancellationToken))
                    {
                        response.EnsureSuccessStatusCode();

                        if (!HttpContentType.Json.Equals(response.Content.Headers.ContentType.MediaType))
                            throw new HttpRequestException(WrongContentTypeException);

                        using (var contentStream = await response.Content.ReadAsStreamAsync())
                        {
                            using (StreamReader sr = new StreamReader(contentStream))
                            {
                                using (JsonReader reader = new JsonTextReader(sr))
                                {
                                    JsonSerializer serializer = new JsonSerializer();

                                    return serializer.Deserialize<T>(reader);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static async Task GetVideoAsync(Uri uri, string path, CancellationToken cancellationToken)
        {
            using (var client = new WebClient())
            {
                cancellationToken.Register(client.CancelAsync);

                await client.DownloadFileTaskAsync(uri, path);
            }
        }

        public static async Task<byte[]> DownloadImage(Uri uri, CancellationToken cancellationToken)
        {
            using (var client = new WebClient())
            {
                cancellationToken.Register(client.CancelAsync);
                return await client.DownloadDataTaskAsync(uri);
            }
        }
    }
}