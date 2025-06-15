using System.Text.Json;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core.Common
{
    internal static class Serialization
    {
        public static JsonSerializerOptions DefaultOptions = new()
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter(),
            },
        };
    }
}
