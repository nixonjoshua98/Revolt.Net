using Revolt.Net.Core.Converters;
using Revolt.Net.WebSocket.Converters;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Json
{
    internal static class Serialization
    {
        public static JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,

            Converters =
            {
                new RevoltChannelConverter(),
                new JsonStringEnumConverter(),
                new OptionalConverterFactory()
            },

            WriteIndented = true
        };

        public static T Deserialize<T>(JsonNode node)
        {
            return node.Deserialize<T>(Options);
        }

        public static T Deserialize<T>(string message)
        {
            return JsonSerializer.Deserialize<T>(message, Options)!;
        }

        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, Options);
        }
    }
}
