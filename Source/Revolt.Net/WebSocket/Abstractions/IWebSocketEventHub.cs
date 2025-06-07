using Revolt.Net.Core;
using Revolt.Net.WebSocket.Messages;

namespace Revolt.Net.WebSocket.Abstractions
{
    internal interface IWebSocketEventHub
    {
        AsyncEvent<ReadyWebSocketEvent> Ready { get; }

        internal Task InvokeAsync<T>(T message, CancellationToken cancellationToken) where T : WebSocketEvent;
    }
}