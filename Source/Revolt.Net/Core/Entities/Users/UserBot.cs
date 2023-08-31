using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Users
{
    public class UserBot
    {
        [JsonPropertyName("owner")]
        public string OwnerId { get; init; }
    }
}
