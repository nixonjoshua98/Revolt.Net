using System.Text.RegularExpressions;

namespace Revolt.Net.Rest.RateLimit
{
    internal sealed record RoutePattern(HttpMethod? Method, Regex Regex, string Path)
    {
        public string Key = Method is null ? Path : $"{Method.Method}:{Path}";
    }

    internal static class RateLimitHelper
    {
#pragma warning disable SYSLIB1045 // Convert to 'GeneratedRegexAttribute'.

        // https://developers.revolt.chat/developers/api/ratelimits.html
        private static readonly List<RoutePattern> _routePatterns =
        [
            new(HttpMethod.Patch,   new Regex(@"users/[^/]+/?$"),               "users/:id"),
            new(HttpMethod.Post,    new Regex(@"channels/[^/]+/messages/?$"),   "channels/:id/messages"),
            new(HttpMethod.Delete,  new Regex(@"auth/?$"),                      "auth"),
            new(HttpMethod.Post,    new Regex(@"safety/report/?$"),             "safety/report"),

            new(null, new Regex(@"users/[^/]+/default_avatar/?$"),  "users/:id/default_avatar"),
            new(null, new Regex(@"users/?$"),                       "users"),
            new(null, new Regex(@"bots/?$"),                        "bots"),
            new(null, new Regex(@"channels/?$"),                    "channels"),
            new(null, new Regex(@"servers/?$"),                     "servers"),
            new(null, new Regex(@"auth/?$"),                        "auth"),
            new(null, new Regex(@"safety/?$"),                      "safety"),
            new(null, new Regex(@"swagger/?$"),                     "swagger"),
            new(null, new Regex(@".*$"),                            "*")
        ];

        public static bool TryGetRouteKey(HttpRequestMessage request, out string key)
        {
            var relativeUrl = request.RequestUri is null ? string.Empty : request.RequestUri.OriginalString;

            foreach (var pattern in _routePatterns)
            {
                if ((pattern.Method == null || pattern.Method == request.Method) && pattern.Regex.IsMatch(relativeUrl))
                {
                    key = pattern.Key;

                    return true;
                }
            }

            key = string.Empty;

            return false;
        }
    }
}
