using Revolt.Net.Enums;

namespace Revolt.Net.Entities.Relationships
{
    public class Relation
    {
        public string Id { get; init; } = default!;

        public RelationshipStatus Status { get; init; }
    }
}