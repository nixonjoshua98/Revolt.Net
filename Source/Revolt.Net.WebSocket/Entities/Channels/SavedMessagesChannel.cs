using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public class SavedMessagesChannel : Channel
    {
        [JsonPropertyName("user")]
        public string UserId { get; init; } = default!;
    }
}