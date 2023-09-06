using System.Text.Json.Nodes;

namespace Revolt.Net.WebSocket
{
    internal sealed record SocketMessagePayload(string Type, JsonNode Content);
}
