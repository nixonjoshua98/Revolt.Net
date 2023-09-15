using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Revolt.Net.Rest.Entities.Channels
{
    internal sealed class ChannelPayload
    {
        public ChannelType ChannelType { get; init; }

        [JsonPropertyName("_id")]
        public string Id { get; init; }

        [JsonPropertyName("user")]
        public Optional<string> UserId { get; init; }

        [JsonPropertyName("server")]
        public Optional<string> ServerId { get; init; }

        public Optional<bool> Active { get; init; }

        public Optional<IEnumerable<string>> Recipients { get; init; }

        public Optional<string> LastMessageId { get; init; }

        public Optional<string> Name { get; init; }

        [JsonPropertyName("owner")]
        public Optional<string> OwnerId { get; init; }

        public Optional<string> Description { get; init; }

        public Optional<bool> Nsfw {  get; init; } 
    }
}
