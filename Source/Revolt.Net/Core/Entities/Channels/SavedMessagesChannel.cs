using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Entities.Channels
{
    public class SavedMessagesChannel : Channel
    {
        [JsonPropertyName("user")]
        public string UserId { get; init; } = default!;
    }
}