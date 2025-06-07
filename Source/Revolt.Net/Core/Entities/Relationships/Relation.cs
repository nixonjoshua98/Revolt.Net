using Revolt.Net.Core.Entities.Relationships;

namespace Revolt.Net
{
    public class Relation
    {
        public string Id { get; init; } = default!;

        public RelationshipStatus Status { get; init; }
    }
}