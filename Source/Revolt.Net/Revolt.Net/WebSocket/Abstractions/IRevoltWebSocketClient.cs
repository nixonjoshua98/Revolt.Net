using Revolt.Net.Core.Common;
using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket.Events;
using Revolt.Net.WebSocket.JsonModels;

namespace Revolt.Net.WebSocket.Abstractions
{
    public interface IRevoltWebSocketClient
    {
        AsyncEvent<ReadyEvent> Ready { get; }
        AsyncEvent<MessageReceivedEvent> MessageReceived { get; }
        AsyncEvent<ChannelStartTypingEvent> ChannelStartTyping { get; }
        AsyncEvent<ChannelStopTypingEvent> ChannelStopTyping { get; }
        internal RevoltRestClient RestClient { get; }

        internal Task InvokeAsync<T>(T message, CancellationToken cancellationToken) where T : JsonWebSocketMessage;
    }
}