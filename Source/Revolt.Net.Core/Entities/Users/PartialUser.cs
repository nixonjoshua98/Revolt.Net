﻿namespace Revolt.Net
{
    public sealed class PartialUser
    {
        public Optional<PartialUserStatus> Status { get; init; } = default!;
    }
}
