using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net.WebSocket.Models
{
    internal sealed record ReceiveWebSocketMessage(WebSocketMessageType SocketMessageType, string Content);
}
