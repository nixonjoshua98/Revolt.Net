﻿namespace Revolt.Net.WebSocket
{
    public class AttachmentMetadata
    {
        public string Type { get; init; } = default!;
        public long? Width { get; init; } = default!;
        public long? Height { get; init; } = default!;
    }
}