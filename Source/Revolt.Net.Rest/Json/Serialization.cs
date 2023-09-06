using Revolt.Net.Core;
using Revolt.Net.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest.Json
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
                new OptionalConverterFactory(),
                new RevoltColorConverter()
            },

            WriteIndented = true
        };

        public static T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, Options);
        }

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
