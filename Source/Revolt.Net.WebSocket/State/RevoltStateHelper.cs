namespace Revolt.Net.WebSocket.State
{
    internal static class RevoltStateHelper
    {
        public static async ValueTask<T> GetOrDownloadAsync<T>(
            FetchBehaviour behaviour,
            Func<T> cacheFactory,
            Func<Task<T>> fetchFactory,
            Action<T> cacheFunc = null!)
        {
            if (behaviour == FetchBehaviour.Download)
            {
                return await _DownloadAndCache(fetchFactory, cacheFunc);
            }

            var value = cacheFactory();

            return value ??= await _DownloadAndCache(fetchFactory, cacheFunc);

            static async Task<T> _DownloadAndCache(Func<Task<T>> fetchFactory, Action<T> cacheFunc)
            {
                var value = await fetchFactory();

                if (value is not null)
                {
                    cacheFunc(value);
                }

                return value;
            }
        }
    }
}
