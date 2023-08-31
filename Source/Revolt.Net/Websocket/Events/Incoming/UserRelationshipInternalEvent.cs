using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Core.Enums;

namespace Revolt.Net.Websocket.Events.Incoming
{
    internal sealed class UserRelationshipInternalEvent
    {
        public User User { get; init; } = default!;
        public RelationshipStatus Status { get; init; } = default!;
    }
}
