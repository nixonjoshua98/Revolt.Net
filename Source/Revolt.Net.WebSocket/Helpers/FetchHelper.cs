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
            Func<Task<T>> fetchFactory)
        {
            if (behaviour == FetchBehaviour.DownloadOnly)
            {
                return await fetchFactory.Invoke();
            }

            return cacheFactory() ?? await fetchFactory.Invoke();
        }
    }
}
