using Revolt.Net.Json;
using Revolt.Net.WebSocket.Converters;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Json
{
    internal static class WebSocketSerialization
    {
        public static JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,

            Converters =
            {
                new RevoltChannelConverter(),
                new JsonStringEnumConverter(),
                new OptionalConverterFactory(),
                new RevoltColorConverter()
            },

            WriteIndented = true
        };

        public static T Deserialize<T>(JsonNode node)
        {
            return node.Deserialize<T>(Options);
        }

        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, Options);
        }
    }
}
