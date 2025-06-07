using Revolt.Net.Core.Entities.Relationships;

namespace Revolt.Net.WebSocket
{
    public sealed record UserRelationshipEvent(IUser User, RelationshipStatus Status);
}
