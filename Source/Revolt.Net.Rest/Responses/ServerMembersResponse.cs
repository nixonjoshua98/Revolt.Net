namespace Revolt.Net.Rest
{
    internal sealed record ServerMembersResponse(
        IReadOnlyList<ServerMemberReference> Members,
        IReadOnlyList<RestUser> Users
    );
}
