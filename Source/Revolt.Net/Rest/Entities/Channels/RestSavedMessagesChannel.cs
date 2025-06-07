using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestSavedMessagesChannel : RestChannel
    {
        [JsonPropertyName("user")]
        public string UserId { get; init; } = default!;
    }
}