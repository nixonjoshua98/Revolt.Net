using Revolt.Net.Core.Entities.Relationships;

namespace Revolt.Net
{
    public interface IUser
    {
        string Id { get; init; }
        string Username { get; init; }
        Optional<UserBot> Bot { get; init; }
        RelationshipStatus Relationship { get; init; }
        bool IsOnline { get; init; }
        bool IsPrivileged { get; init; }
        UserStatus Status { get; init; }
        Badge Badges { get; init; }
        IEnumerable<Relation> Relations { get; init; }
        string DisplayName { get; init; }
        bool IsBot { get; }
    }
}
