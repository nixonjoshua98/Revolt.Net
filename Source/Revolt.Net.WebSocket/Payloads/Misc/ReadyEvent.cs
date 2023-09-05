namespace Revolt.Net.WebSocket
{
    public sealed record ReadyEvent;

    internal sealed record ReadyMessage(
        IReadOnlyList<Server> Servers,
        IReadOnlyList<Channel> Channels,
        IReadOnlyList<User> Users,
        IReadOnlyList<ServerMemberReference> Members
    )
    {
        internal ReadyEvent ToEvent() => new();
    }
}