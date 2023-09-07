using Revolt.Net.Core.Entities.Relationships;
using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket
{
    internal sealed record UserRelationshipPayload(RestUser User, RelationshipStatus Status)
    {
        public UserRelationshipEvent ToEvent() => new(User, Status);
    }

    public sealed record UserRelationshipEvent(IUser User, RelationshipStatus Status);
}
