namespace Revolt.Net.Rest
{
    internal sealed record ServerMembersResponse(
        IReadOnlyList<ServerMember> Members,
        IReadOnlyList<RestUser> Users
    );
}
