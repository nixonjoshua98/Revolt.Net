namespace Revolt.Net.WebSocket
{
    public sealed record ReadyEvent;

    internal sealed record ReadyMessage(
        IReadOnlyList<SocketServer> Servers,
        IReadOnlyList<SocketChannel> Channels,
        IReadOnlyList<SocketUser> Users,
        IReadOnlyList<ServerMemberReference> Members
    )
    {
        internal ReadyEvent ToEvent() => new();
    }
}