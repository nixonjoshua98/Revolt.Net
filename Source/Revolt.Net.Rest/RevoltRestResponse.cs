using System.Net;

namespace Revolt.Net.Rest
{
    internal sealed record RevoltRestResponse(
        HttpStatusCode StatusCode,
        IReadOnlyDictionary<string, string> Headers,
        string Content
    );
}
