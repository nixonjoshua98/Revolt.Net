﻿using Revolt.Net.Entities.Users;

namespace Revolt.Net.Websocket.Events.Incoming
{
    internal sealed class UserRelationshipInternalEvent
    {
        public User User { get; init; } = default!;
        public RelationshipStatus Status { get; init; } = default!;
    }
}
