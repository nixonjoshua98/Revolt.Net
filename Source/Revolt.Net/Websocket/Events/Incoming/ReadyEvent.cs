using Revolt.Net.Entities.Channels;
using Revolt.Net.Entities.Servers;
using Revolt.Net.Entities.Users;

namespace Revolt.Net.Websocket.Events.Incoming;

public sealed record ReadyEvent;

internal sealed record ReadyInternalEvent(
    IReadOnlyList<Server> Servers,
    IReadOnlyList<Channel> Channels,
    IReadOnlyList<User> Users,
    IReadOnlyList<ServerMemberReference> Members
)
{
    internal ReadyEvent ToPublicEvent() => new();
}