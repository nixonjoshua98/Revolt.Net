using Revolt.Net.Rest.Json;

namespace Revolt.Net.Rest
{
    public abstract class RevoltApiClientBase(HttpClient _httpClient)
    {
        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint, CancellationToken cancellationToken = default) where TResponse : class
        {
            var resp = await SendAsyncInternal(method, endpoint, cancellationToken);

            return Serialization.Deserialize<TResponse>(resp.Content);
        }

        public async Task SendAsync(string method, string relativePath, CancellationToken cancellationToken = default)
        {
            await SendAsyncInternal(method, relativePath, cancellationToken);
        }

        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint, object request, CancellationToken cancellationToken = default) where TResponse : class
        {
            var json = Serialization.Serialize(request);

            var resp = await SendAsyncInternal(method, endpoint, json, cancellationToken);

            return Serialization.Deserialize<TResponse>(resp.Content);
        }

        private async Task<RevoltRestResponse> SendAsyncInternal(string method, string relativePath, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(GetMethod(method), relativePath);

            return await SendAsyncInternal(request, cancellationToken);
        }

        private async Task<RevoltRestResponse> SendAsyncInternal(string method, string relativePath, string json, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(GetMethod(method), relativePath)
            {
                Content = new StringContent(json)
            };

            return await SendAsyncInternal(request, cancellationToken);
        }

        private async Task<RevoltRestResponse> SendAsyncInternal(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);

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


    public sealed class RevoltRestClient
    {
        private readonly HttpClient Client;
        private readonly string ApiUrl;

        public RevoltRestClient(string url)
        {
            ApiUrl = url;

            Client = new HttpClient();
        }

        public void AddDefaultHeader(string key, string value)
        {
            Client.DefaultRequestHeaders.Remove(key);

            Client.DefaultRequestHeaders.Add(key, value);
        }

        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint) where TResponse : class
        {
            var resp = await SendAsyncInternal(method, endpoint);

            return Serialization.Deserialize<TResponse>(resp.Content);
        }

        public async Task SendAsync(string method, string endpoint)
        {
            await SendAsyncInternal(method, endpoint);
        }

        public async Task SendAsync(string method, string endpoint, object request)
        {
            var body = Serialization.Serialize(request);

            await SendAsyncInternal(method, endpoint, body);
        }

        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint, object request) where TResponse : class
        {
            var body = Serialization.Serialize(request);

            var resp = await SendAsyncInternal(method, endpoint, body);

            return Serialization.Deserialize<TResponse>(resp.Content);
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
