using Revolt.Net.WebSocket.Helpers;
using Revolt.Net.WebSocket.State;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public sealed class SocketTextChannel : SocketMessageChannel
    {
        [JsonPropertyName("server")]
        public string ServerId { get; init; } = default!;

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public string LastMessageId { get; init; } = default!;

        public bool Nsfw { get; init; }

        public SocketServer Server => Client.State.GetServer(ServerId);

        public SocketMessage LastMessage => Client.State.GetMessage(Id, LastMessageId);

        public async ValueTask<SocketMessage> GetLastMessageAsync() =>
            await ChannelHelper.GetMessageAsync(Client, Id, LastMessageId, FetchBehaviour.CacheThenDownload);
    }
}
