using Revolt.Net.Core;
using Revolt.Net.WebSocket.Messages;

namespace Revolt.Net.WebSocket.Abstractions
{
    public interface IWebSocketEventHub
    {
        AsyncEvent<ReadyWebSocketEvent> Ready { get; }
        AsyncEvent<MessageWebSocketEvent> Message { get; }

        internal Task InvokeAsync<T>(T message, CancellationToken cancellationToken) where T : WebSocketEvent;
    }
}