namespace Revolt.Net.WebSocket
{
    internal sealed record UserRelationshipPayload(SocketUser User, RelationshipStatus Status)
    {
        public UserRelationshipEvent ToEvent() => new(User, Status);
    }

    public sealed record UserRelationshipEvent(SocketUser User, RelationshipStatus Status);
}
