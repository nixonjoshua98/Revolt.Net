using Revolt.Net.Core.Enums;

namespace Revolt.Net.Core.Entities.Relationships
{
    public class Relation
    {
        public string Id { get; init; } = default!;

        public RelationshipStatus Status { get; init; }
    }
}