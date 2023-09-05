using System.Text.Json.Serialization;

namespace Revolt.Net
{
    public class UserBot
    {
        [JsonPropertyName("owner")]
        public string OwnerId { get; init; }
    }
}
