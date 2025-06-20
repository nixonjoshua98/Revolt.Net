﻿using System.Text.Json.Serialization;

namespace Revolt.Net.Core.JsonModels
{
    internal sealed class JsonUser
    {
        [JsonPropertyName("_id")]
        public required string Id { get; init; }

        public JsonBot? Bot { get; init; }

        public required string Username { get; init; }

        public required string Discriminator { get; init; }
    }
}
