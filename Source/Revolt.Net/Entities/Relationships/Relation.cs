using Revolt.Net.Entities.Users;

namespace Revolt.Net.Entities.Relationships
{
    public class Relation
    {
        public string Id { get; init; } = default!;

        public RelationshipStatus Status { get; init; }
    }
}