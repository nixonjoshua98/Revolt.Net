using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Contracts
{
    internal sealed class GetServerMembersResponse
    {
        public required IReadOnlyList<JsonServerMember> Members { get; init; }
        public required IReadOnlyList<JsonUser> Users { get; init; }
    }
}
