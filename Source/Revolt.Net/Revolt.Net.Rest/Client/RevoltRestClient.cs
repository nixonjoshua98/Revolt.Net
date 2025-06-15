using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Revolt.Net.Core.Common;
using Revolt.Net.Core.Configuration;
using Revolt.Net.Rest.Common;
using Revolt.Net.Rest.Extensions;
using Revolt.Net.Rest.RateLimit;
using System.Collections.Concurrent;
using System.Net;
using System.Text.Json;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient(
        IHttpClientFactory _clientFactory,
        IOptions<RevoltConfiguration> _configurationOptions,
        ILogger<RevoltRestClient> _logger
    )
    {
        private readonly RevoltConfiguration _configuration = _configurationOptions.Value;
        private readonly ConcurrentDictionary<string, RateLimitBucket> _routeKeysToBuckets = new();

        private async Task<T> SendRequestAsync<T>(HttpMethod method, HttpContent content, string relativePath, CancellationToken cancellationToken)
        {
            return await SendRequestPrivateAsync<T>(() => new HttpRequestMessage(method, relativePath) { Content = content }, cancellationToken);
        }

        private async Task<T> SendRequestAsync<T>(HttpMethod method, string relativePath, CancellationToken cancellationToken)
        {
            return await SendRequestPrivateAsync<T>(() => new HttpRequestMessage(method, relativePath), cancellationToken);
        }

        private async Task SendRequestAsync(HttpMethod method, string relativePath, CancellationToken cancellationToken)
        {
            using var response = await SendRequestPrivateAsync(() => new HttpRequestMessage(method, relativePath), cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        private async Task<T> SendRequestPrivateAsync<T>(Func<HttpRequestMessage> requestFactory, CancellationToken cancellationToken)
        {
            using var response = await SendRequestPrivateAsync(requestFactory, cancellationToken);

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(json, Serialization.DefaultOptions)
                ?? throw new Exception("Failed to read response json");
        }

        private async Task<HttpResponseMessage> SendRequestPrivateAsync(Func<HttpRequestMessage> requestFactory, CancellationToken cancellationToken)
        {
            using var request = requestFactory();

            using var client = await CreateHttpClientAsync();

            RateLimitBucket? bucket = null;

            if (RateLimitHelper.TryGetRouteKey(request, out var key) &&
                _routeKeysToBuckets.TryGetValue(key, out bucket) &&
                bucket.IsRateLimited)
            {
                _logger.LogDebug("Revolt.Net.Rest : Waiting due to bucket '{BucketId}' being rate limited", bucket.BucketId);

                await bucket.WaitAsync(cancellationToken);
            }

            var response = await client.SendAsync(request, cancellationToken);

            UpdateBucketFromResponse(response, bucket);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                return await SendRequestPrivateAsync(requestFactory, cancellationToken);
            }

            return response;
        }

        private Task<HttpClient> CreateHttpClientAsync()
        {
            var client = _clientFactory.CreateClient();

            client.BaseAddress = _configuration.ServerUrl;

            client.DefaultRequestHeaders.Add(RevoltRestConstant.BotTokenHeader, _configuration.Token);

            return Task.FromResult(client);
        }

        private void UpdateBucketFromResponse(HttpResponseMessage response, RateLimitBucket? existingBucket)
        {
            if (response.Headers.TryGetValues(RevoltRestConstant.RateLimitBucket, out var bucketHeaders))
            {
                var bucketId = bucketHeaders.First();

                // Unsure if the bucket id will ever change but we can handle it easily anyway
                if (existingBucket is not null && existingBucket.BucketId != bucketId)
                {
                    _routeKeysToBuckets.Remove(existingBucket.BucketId, out _);
                }

                var limit = response.Headers.Get<int>(RevoltRestConstant.RateLimitLimit);
                var remaining = response.Headers.Get<int>(RevoltRestConstant.RateLimitRemaining);
                var resetAfterMs = response.Headers.Get<float>(RevoltRestConstant.RateLimitResetAfter);

                var resetAfter = TimeSpan.FromMilliseconds(resetAfterMs);

                if (RateLimitHelper.TryGetRouteKey(response.RequestMessage!, out var key))
                {
                    var routeBucket = _routeKeysToBuckets.GetOrAdd(
                        key,
                        _ => new RateLimitBucket(bucketId, limit, remaining, resetAfter)
                    );

                    routeBucket.Update(remaining, resetAfter);
                }

                _logger.LogDebug("Revolt.Net.Rest : Updated bucket '{BucketId}'", bucketId);
            }
        }
    }
}