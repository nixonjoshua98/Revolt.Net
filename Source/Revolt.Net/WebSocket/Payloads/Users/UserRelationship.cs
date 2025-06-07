using Revolt.Net.Core.Entities.Relationships;
using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket
{
    public sealed record UserRelationshipEvent(IUser User, RelationshipStatus Status);
}
