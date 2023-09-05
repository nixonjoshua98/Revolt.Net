namespace Revolt.Net.WebSocket
{
    public class Relation
    {
        public string Id { get; init; } = default!;

        public RelationshipStatus Status { get; init; }
    }
}