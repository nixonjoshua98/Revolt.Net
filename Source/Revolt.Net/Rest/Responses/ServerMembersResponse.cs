using Revolt.Net.Core.Entities.Members;

namespace Revolt.Net.Rest
{
    internal sealed record ServerMembersResponse(
        IReadOnlyList<ServerMemberReference> Members,
        IReadOnlyList<RestUser> Users
    );
}
