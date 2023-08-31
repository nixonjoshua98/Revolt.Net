using Revolt.Net.Core;
using Revolt.Net.Core.Common.Json;
using Revolt.Net.Core.Common.Types;
using System.Text.Json;

namespace Revolt.Net.Rest
{
    public sealed class RevoltRestClient
    {
        private readonly HttpClient Client;
        private readonly RevoltConfiguration Configuration;

        public RevoltRestClient(RevoltConfiguration configuration)
        {
            Configuration = configuration;

            Client = new HttpClient(
                new HttpClientHandler
                {

                }
            );
        }

        public void AddDefaultHeader(string key, string value)
        {
            Client.DefaultRequestHeaders.Remove(key);

            Client.DefaultRequestHeaders.Add(key, value);
        }

        public async Task<T> SendAsync<T>(string method, string endpoint, Dictionary<string, object> queryString) where T : class
        {
            var qs = string.Join("&", queryString.Select(kv => $"{kv.Key}={kv.Value}"));

            return await SendAsync<T>(method, $"{endpoint}?{qs}");
        }

        public async Task<T> SendAsync<T>(string method, string endpoint) where T : class
        {
            var resp = await SendAsync(method, endpoint);

            var result = DeserializeResponse<T>(resp.Content);

            return result.Left;
        }

        private async Task<RevoltRestResponse> SendAsync(string method, string endpoint)
        {
            var url = Path.Combine(Configuration.ApiUrl, endpoint);

            using var request = new HttpRequestMessage(GetMethod(method), url);

            return await SendAsync(request);
        }

        private async Task<RevoltRestResponse> SendAsync(HttpRequestMessage request, CancellationToken cancelToken = default)
        {
            var response = await Client.SendAsync(request, cancelToken);

            var headers = response.Headers
                .ToDictionary(x => x.Key, x => x.Value.FirstOrDefault(), StringComparer.OrdinalIgnoreCase);

            var content = await response.Content.ReadAsStringAsync(cancelToken);

            return new RevoltRestResponse(response.StatusCode, headers, content);
        }

        private Union<T, Exception> DeserializeResponse<T>(string json) where T : class
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json, Serialization.Options)!;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private static HttpMethod GetMethod(string method)
        {
            return method switch
            {
                "DELETE" => HttpMethod.Delete,
                "GET" => HttpMethod.Get,
                "PATCH" => PatchMethod,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                _ => throw new ArgumentOutOfRangeException(nameof(method), $"Unknown HttpMethod: {method}"),
            };
        }

        private static readonly HttpMethod PatchMethod = new("PATCH");
    }
}
