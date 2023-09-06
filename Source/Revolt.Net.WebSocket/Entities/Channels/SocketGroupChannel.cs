using Revolt.Net.WebSocket.Helpers;
using Revolt.Net.WebSocket.State;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public class SocketGroupChannel : SocketMessageChannel
    {
        public string Name { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        public string Description { get; init; } = default!;

        public SocketUser Owner => Client.State.GetUser(OwnerId);

        public async ValueTask<SocketUser> GetOwnerAsync(FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload) =>
            await UserHelper.GetUserAsync(Client, OwnerId, behaviour);
    }
}