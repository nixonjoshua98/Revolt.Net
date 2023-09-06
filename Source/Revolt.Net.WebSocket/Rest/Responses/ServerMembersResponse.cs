using Revolt.Net.WebSocket;

namespace Revolt.Net.Rest
{
    internal sealed record ServerMembersResponse(
        IReadOnlyList<ServerMemberReference> Members,
        IReadOnlyList<SocketUser> Users
    );
}
