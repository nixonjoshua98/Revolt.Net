using Revolt.Net.WebSocket.Entities;

namespace Revolt.Net.WebSocket.Client
{
    internal sealed partial class RevoltWebSocketClient
    {
        public async Task<User> GetUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var user = await _restClient.GetUserAsync(userId, cancellationToken);

            return new User(user.JsonModel, this);
        }
    }
}
