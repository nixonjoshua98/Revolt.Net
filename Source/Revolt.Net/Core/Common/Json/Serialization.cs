using Revolt.Net.Core.Common.Converters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Common.Json
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

        public static T Deserialize<T>(string message)
        {
            return JsonSerializer.Deserialize<T>(message, Options)!;
        }
    }
}
