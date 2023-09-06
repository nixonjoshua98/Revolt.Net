using Revolt.Net.WebSocket.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.WebSocket.Helpers
{
    internal static class FetchHelper
    {
        public static async ValueTask<T> GetOrDownloadAsync<T>(
            FetchBehaviour behaviour,
            Func<T> cacheFactory,
            Func<Task<T>> fetchFactory,
            Action<T> fetchCallback)
        {
            if (behaviour == FetchBehaviour.DownloadOnly)
            {
                return await DownloadAndCache(fetchFactory, fetchCallback);
            }

            var value = cacheFactory();

            return value ??= await DownloadAndCache(fetchFactory, fetchCallback);
        }

        private static async Task<T> DownloadAndCache<T>(Func<Task<T>> fetchFactory, Action<T> cacheFunc)
        {
            var value = await fetchFactory();

            if (value is not null)
            {
                cacheFunc.Invoke(value);
            }

            return value;
        }
    }
}
