using Revolt.Net.Core;
using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket.Events.Channels;
using Revolt.Net.WebSocket.Events.Messages;
using Revolt.Net.WebSocket.JsonModels;

namespace Revolt.Net.WebSocket.Abstractions
{
    public interface IRevoltWebSocketClient
    {
        AsyncEvent<JsonReadyMessage> Ready { get; }

        AsyncEvent<MessageEvent> MessageReceived { get; }
        AsyncEvent<ChannelStartTypingEvent> ChannelStartTyping { get; }
        AsyncEvent<ChannelStopTypingEvent> ChannelStopTyping { get; }
        internal RevoltRestClient RestClient { get; }

        internal Task InvokeAsync<T>(T message, CancellationToken cancellationToken) where T : JsonWebSocketMessage;
    }
}