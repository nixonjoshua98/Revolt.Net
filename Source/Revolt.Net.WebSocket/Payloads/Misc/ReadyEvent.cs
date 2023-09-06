using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket
{
    public sealed record ReadyEvent;

    internal sealed record ReadyMessage(
        IReadOnlyList<SocketServer> Servers,
        IReadOnlyList<RestChannel> Channels,
        IReadOnlyList<RestUser> Users,
        IReadOnlyList<ServerMemberReference> Members
    )
    {
        internal ReadyEvent ToEvent() => new();
    }
}