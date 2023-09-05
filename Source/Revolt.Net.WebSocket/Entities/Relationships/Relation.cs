using Revolt.Net.Core.Entities.Users;

namespace Revolt.Net.WebSocket
{
    public class Relation
    {
        public string Id { get; init; } = default!;

        public RelationshipStatus Status { get; init; }
    }
}