﻿using Revolt.Net.Rest;
using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public sealed class ServerMessageChannel : MessageChannel
    {
        [JsonPropertyName("server")]
        public string ServerId { get; init; } = default!;

        public string Name { get; init; } = default!;

        public string Description { get; init; } = default!;

        public string LastMessageId { get; init; } = default!;

        public bool Nsfw { get; init; }

        public async Task<RestMessage> GetLastMessageAsync() =>
            await Client.Api.GetMessageAsync(Id, LastMessageId);
    }
}
