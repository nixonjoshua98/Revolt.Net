using Revolt.Net.WebSocket.Helpers;
using Revolt.Net.WebSocket.State;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public class SocketSavedMessagesChannel : SocketChannel
    {
        [JsonPropertyName("user")]
        public string UserId { get; init; } = default!;

        public IUser User => Client.State.GetUser(UserId);

        public async ValueTask<IUser> GetUserAsync(FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload) =>
            await UserHelper.GetUserAsync(Client, UserId, behaviour);
    }
}