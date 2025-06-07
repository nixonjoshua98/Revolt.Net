using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Revolt.Net.Core.Common;
using Revolt.Net.Core.Hosting.Configuration;
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

        async Task<T> SendRequestAsync<T>(HttpMethod method, HttpContent content, string relativePath, CancellationToken cancellationToken)
        {
            return await SendRequestPrivateAsync<T>(() => new HttpRequestMessage(method, relativePath) { Content = content }, cancellationToken);
        }

        async Task<T> SendRequestAsync<T>(HttpMethod method, string relativePath, CancellationToken cancellationToken)
        {
            return await SendRequestPrivateAsync<T>(() => new HttpRequestMessage(method, relativePath), cancellationToken);
        }

        async Task SendRequestAsync(HttpMethod method, string relativePath, CancellationToken cancellationToken)
        {
            using var response = await SendRequestPrivateAsync(() => new HttpRequestMessage(method, relativePath), cancellationToken);
        }

        async Task<T> SendRequestPrivateAsync<T>(Func<HttpRequestMessage> requestFactory, CancellationToken cancellationToken)
        {
            using var response = await SendRequestPrivateAsync(requestFactory, cancellationToken);

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonSerializer.Deserialize<T>(json, Serialization.DefaultOptions)
                ?? throw new Exception("Failed to read response json");
        }

        async Task<HttpResponseMessage> SendRequestPrivateAsync(Func<HttpRequestMessage> requestFactory, CancellationToken cancellationToken)
        {
            using var request = requestFactory();

            using var client = await CreateHttpClientAsync();

            if (TryGetBucket(request, out var bucket) && bucket.IsRateLimited)
            {
                _logger.LogDebug("Revolt.Net.Rest : Waiting due to bucket '{BucketId}'", bucket.BucketId);

                await bucket.WaitAsync(cancellationToken);
            }

            var response = await client.SendAsync(request, cancellationToken);

            UpdateBucketFromResponse(response, bucket);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                return await SendRequestPrivateAsync(requestFactory, cancellationToken);
            }

            response.EnsureSuccessStatusCode();

            return response;
        }

        private bool TryGetBucket(HttpRequestMessage request, out RateLimitBucket bucket)
        {
            bucket = null!;

            return RateLimitHelper.TryGetRouteKey(request, out var key) && _routeKeysToBuckets.TryGetValue(key, out bucket!);
        }

        private Task<HttpClient> CreateHttpClientAsync()
        {
            var client = _clientFactory.CreateClient();

            client.BaseAddress = _configuration.ServerUrl;

            client.DefaultRequestHeaders.Remove(RevoltRestConstant.BotTokenHeader);

            client.DefaultRequestHeaders.Add(RevoltRestConstant.BotTokenHeader, _configuration.Token);

            return Task.FromResult(client);
        }

        void UpdateBucketFromResponse(HttpResponseMessage response, RateLimitBucket? existingBucket)
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
