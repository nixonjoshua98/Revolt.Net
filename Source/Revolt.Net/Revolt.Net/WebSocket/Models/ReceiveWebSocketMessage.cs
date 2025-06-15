using System.Net.WebSockets;

namespace Revolt.Net.WebSocket.Models
{
    internal sealed record ReceiveWebSocketMessage(WebSocketMessageType SocketMessageType, string Content);
}
