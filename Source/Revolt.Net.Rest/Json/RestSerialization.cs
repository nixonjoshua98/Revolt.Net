using Revolt.Net.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest.Json
{
    internal static class RestSerialization
    {
        public static JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,

            Converters =
            {
                //new RevoltChannelConverter(),
                new JsonStringEnumConverter(),
                new OptionalConverterFactory(),
                new RevoltColorConverter()
            },

            WriteIndented = true
        };

        public static T Deserialize<T>(string value)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(value, Options);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialize: " + ex);
            }

            return default!;
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
