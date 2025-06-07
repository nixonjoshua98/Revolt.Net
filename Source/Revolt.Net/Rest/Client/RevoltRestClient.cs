using Microsoft.Extensions.Options;
using Revolt.Net.Core.Common;
using Revolt.Net.Core.Hosting.Configuration;
using Revolt.Net.Rest.Common;
using System.Text.Json;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient(IHttpClientFactory _clientFactory, IOptions<RevoltConfiguration> _configurationOptions)
    {
        private readonly RevoltConfiguration _configuration = _configurationOptions.Value;

        async Task<T> SendRequestAsync<T>(HttpMethod method, HttpContent content, string relativePath, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(method, relativePath)
            {
                Content = content
            };

            return await SendRequestPrivateAsync<T>(request, cancellationToken);
        }

        async Task<T> SendRequestAsync<T>(HttpMethod method, string relativePath, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(method, relativePath)
            {

            };

            return await SendRequestPrivateAsync<T>(request, cancellationToken);
        }

        async Task SendRequestAsync(HttpMethod method, string relativePath, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(method, relativePath);

            using var response = await SendRequestPrivateAsync(request, cancellationToken);
        }

        async Task<T> SendRequestPrivateAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using var response = await SendRequestPrivateAsync(request, cancellationToken);

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonSerializer.Deserialize<T>(json, Serialization.DefaultOptions)
                ?? throw new Exception("Failed to read response json");
        }

        async Task<HttpResponseMessage> SendRequestPrivateAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using var client = await CreateHttpClientAsync();

            var response = await client.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return response;
        }

        private Task<HttpClient> CreateHttpClientAsync()
        {
            var client = _clientFactory.CreateClient();

            client.BaseAddress = _configuration.ServerUrl;

            client.DefaultRequestHeaders.Remove(RevoltRestConstant.BotTokenHeader);

            client.DefaultRequestHeaders.Add(RevoltRestConstant.BotTokenHeader, _configuration.Token);

            return Task.FromResult(client);
        }
    }
}
