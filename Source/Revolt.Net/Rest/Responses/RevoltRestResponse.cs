using System.Net;

namespace Revolt.Net.Rest.Responses
{
    internal sealed record RevoltRestResponse(
        HttpStatusCode StatusCode,
        IReadOnlyDictionary<string, string> Headers,
        string Content
    );
}
