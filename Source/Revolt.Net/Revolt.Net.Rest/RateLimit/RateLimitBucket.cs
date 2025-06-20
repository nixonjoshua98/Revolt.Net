﻿namespace Revolt.Net.Rest.RateLimit
{
    internal sealed class RateLimitBucket(string bucketId, int limit, int remaining, TimeSpan resetAfter)
    {
        private readonly SemaphoreSlim _waitLock = new(1, 1);

        public int Limit { get; } = limit;
        public string BucketId { get; } = bucketId;
        public int Remaining { get; private set; } = remaining;
        public DateTimeOffset ResetsAt { get; private set; } = DateTimeOffset.UtcNow.Add(resetAfter);

        public bool IsRateLimited => Remaining <= 0 && DateTimeOffset.UtcNow <= ResetsAt;

        public void Update(int remaining, TimeSpan resetsAfter)
        {
            Remaining = remaining;
            ResetsAt = DateTimeOffset.UtcNow.Add(resetsAfter);
        }

        public async Task WaitAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _waitLock.WaitAsync(cancellationToken);

                if (IsRateLimited)
                {
                    var ts = ResetsAt - DateTimeOffset.UtcNow;

                    if (ts > TimeSpan.Zero)
                    {
                        await Task.Delay(ts, cancellationToken);
                    }
                }
            }
            finally
            {
                _waitLock.Release();
            }
        }
    }
}
