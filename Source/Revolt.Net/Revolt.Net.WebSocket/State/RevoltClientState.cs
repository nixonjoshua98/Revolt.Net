using Revolt.Net.WebSocket.Entities;
using System.Collections.Concurrent;

namespace Revolt.Net.WebSocket.State
{
    internal sealed class RevoltClientState
    {
        private readonly ConcurrentDictionary<string, Server> Servers = [];

        public async Task<Server> GetOrAddServerAsync(string serverId, Func<Task<Server>> factory)
        {
            if (!Servers.TryGetValue(serverId, out var server))
            {
                Servers[serverId] = server = await factory.Invoke();
            }

            return server;
        }
    }
}
