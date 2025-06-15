using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Revolt.Net.Core.JsonModels.Bots
{
    internal sealed class JsonBot
    {
        [JsonPropertyName("owner")]
        public required string OwnerId { get; init; }
    }
}
