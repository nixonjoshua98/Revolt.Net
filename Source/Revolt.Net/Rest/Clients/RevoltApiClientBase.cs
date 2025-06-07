using Revolt.Net.Core;
using Revolt.Net.Rest.Responses;
using System.Text.Json;

namespace Revolt.Net.Rest
{
    public abstract class RevoltApiClientBase(HttpClient _httpClient)
    {
        protected async Task<TResponse> SendAsync<TResponse>(string method, string endpoint, CancellationToken cancellationToken = default) where TResponse : class
        {
            var resp = await SendAsyncInternal(method, endpoint, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(resp.Content, RevoltCoreConstant.DefaultSerializerOptions)!;
        }

        protected async Task SendAsync(string method, string relativePath, CancellationToken cancellationToken = default)
        {
            await SendAsyncInternal(method, relativePath, cancellationToken);
        }

        public async Task<TResponse> SendAsync<TResponse>(string method, string endpoint, object request, CancellationToken cancellationToken = default) where TResponse : class
        {
            var json = JsonSerializer.Serialize(request, RevoltCoreConstant.DefaultSerializerOptions);

            var resp = await SendAsyncInternal(method, endpoint, json, cancellationToken);

            return JsonSerializer.Deserialize<TResponse>(resp.Content, RevoltCoreConstant.DefaultSerializerOptions)!;
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
}
