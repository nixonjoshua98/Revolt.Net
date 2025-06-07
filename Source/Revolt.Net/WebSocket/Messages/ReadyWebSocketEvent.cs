using Revolt.Net.Core.Entities.Members;
using Revolt.Net.Rest;
using Revolt.Net.WebSocket.Entities.Servers;

namespace Revolt.Net.WebSocket.Messages
{
    public sealed record ReadyWebSocketEvent : WebSocketEvent
    {
        public required IReadOnlyList<RestUser> Users { get; init; }

        public required IReadOnlyList<SocketServer> Servers { get; init; }

        public required IReadOnlyList<RestChannel> Channels { get; init; }

        public required IReadOnlyList<ServerMemberReference> Members { get; init; }
    }
}