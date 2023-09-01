using Revolt.Net.Entities.Users;

namespace Revolt.Net.Rest.Responses
{
    internal sealed record ServerMembersResponse(
        IReadOnlyList<ServerMemberReference> Members,
        IReadOnlyList<User> Users
    );
}
