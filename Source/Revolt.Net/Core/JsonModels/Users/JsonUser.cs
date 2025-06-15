using Revolt.Net.Core.JsonModels.Bots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Revolt.Net.Core.JsonModels.Users
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
