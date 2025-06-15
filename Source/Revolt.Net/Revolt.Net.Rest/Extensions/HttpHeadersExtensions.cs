using System.Globalization;
using System.Net.Http.Headers;

namespace Revolt.Net.Rest.Extensions
{
    internal static class HttpHeadersExtensions
    {
        public static T Get<T>(this HttpHeaders headers, string key) where T : IParsable<T>
        {
            headers.TryGetValues(key, out var values);

            var value = (values ?? throw new Exception($"Header '{key}' not found")).First();

            return T.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}
