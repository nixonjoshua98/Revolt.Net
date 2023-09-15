using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket
{
    public sealed record ReadyEvent;

    internal sealed record ReadyMessage(
        IReadOnlyList<RestServer> Servers,
        IReadOnlyList<RestChannel> Channels,
        IReadOnlyList<RestUser> Users,
        IReadOnlyList<ServerMember> Members
    )
    {
        internal ReadyEvent ToEvent() => new();
    }
}