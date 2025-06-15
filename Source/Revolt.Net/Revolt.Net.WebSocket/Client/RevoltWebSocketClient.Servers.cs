using Revolt.Net.WebSocket.Entities;

namespace Revolt.Net.WebSocket.Client
{
    internal sealed partial class RevoltWebSocketClient
    {
        public async Task<Server> GetServerAsync(string serverId, CancellationToken cancellationToken)
        {
            return await _clientState.GetOrAddServerAsync(serverId, async () =>
            {
                var server = await _restClient.GetServerAsync(serverId, cancellationToken);

                return new Server(server, this);
            });
        }
    }
}
