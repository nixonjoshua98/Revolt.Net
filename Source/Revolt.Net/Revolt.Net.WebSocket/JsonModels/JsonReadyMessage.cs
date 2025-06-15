using Revolt.Net.WebSocket.Events;

namespace Revolt.Net.WebSocket.JsonModels
{
    internal sealed record JsonReadyMessage : JsonWebSocketMessage
    {
        public ReadyEvent ToEvent() => new ReadyEvent();
    }
}