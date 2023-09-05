namespace Revolt.Net.WebSocket
{
    internal sealed record UserRelationshipPayload(User User, RelationshipStatus Status)
    {
        public UserRelationshipEvent ToEvent() => new(User, Status);
    }

    public sealed record UserRelationshipEvent(User User, RelationshipStatus Status);
}
