using Revolt.Net.Core.Entities.Users;

namespace Revolt.Net.Rest.API.Responses
{
    internal sealed record ServerMembersResponse(
        IReadOnlyList<ServerMemberReference> Members,
        IReadOnlyList<User> Users
    );
}
