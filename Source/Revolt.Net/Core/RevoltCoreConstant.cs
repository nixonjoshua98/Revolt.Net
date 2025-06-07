using Revolt.Net.Core.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Revolt.Net.Core
{
    internal static class RevoltCoreConstant
    {
        public static JsonSerializerOptions DefaultSerializerOptions = new()
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
