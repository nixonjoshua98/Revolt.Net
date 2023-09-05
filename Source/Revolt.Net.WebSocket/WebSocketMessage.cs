using System.Text.Json.Nodes;

namespace Revolt.Net.WebSocket
{
    internal sealed record WebSocketMessage(string Type, JsonNode Content);
}
