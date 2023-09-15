using Revolt.Net.Rest.Json;

namespace Revolt.Net.Rest
{
    internal sealed class RevoltHttpClient
    {
        private readonly HttpClient Client;
        private readonly string ApiUrl;

        public RevoltHttpClient(string url)
        {
            ApiUrl = url;

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

        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint) where TResponse : class
        {
            var resp = await SendAsyncInternal(method, endpoint);

            return RestSerialization.Deserialize<TResponse>(resp.Content);
        }

        public async Task SendAsync(string method, string endpoint)
        {
            await SendAsyncInternal(method, endpoint);
        }

        public async Task SendAsync(string method, string endpoint, object request)
        {
            var body = RestSerialization.Serialize(request);

            await SendAsyncInternal(method, endpoint, body);
        }

        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint, object request) where TResponse : class
        {
            var body = RestSerialization.Serialize(request);

            var resp = await SendAsyncInternal(method, endpoint, body);

            return RestSerialization.Deserialize<TResponse>(resp.Content);
        }

        private async Task<RevoltRestResponse> SendAsyncInternal(string method, string endpoint)
        {
            var url = Path.Combine(ApiUrl, endpoint);

            using var request = new HttpRequestMessage(GetMethod(method), url);

            return await SendAsyncInternal(request);
        }

        private async Task<RevoltRestResponse> SendAsyncInternal(string method, string endpoint, string json)
        {
            var url = Path.Combine(ApiUrl, endpoint);

            using var request = new HttpRequestMessage(GetMethod(method), url)
            {
                Content = new StringContent(json)
            };

            return await SendAsyncInternal(request);
        }

        private async Task<RevoltRestResponse> SendAsyncInternal(HttpRequestMessage request)
        {
            var response = await Client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var headers = response.Headers
                .ToDictionary(x => x.Key, x => x.Value.FirstOrDefault(), StringComparer.OrdinalIgnoreCase);

            var content = await response.Content.ReadAsStringAsync();

            return new RevoltRestResponse(response.StatusCode, headers, content);
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
