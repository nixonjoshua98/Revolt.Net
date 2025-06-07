namespace Revolt.Net.Rest.Responses
{
    internal sealed record SendMessageRequest(string Content, IEnumerable<Embed> Embeds);
}