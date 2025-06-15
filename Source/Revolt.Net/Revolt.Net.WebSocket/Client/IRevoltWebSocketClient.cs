using Revolt.Net.Core.Common;
using Revolt.Net.Core.JsonModels;
using Revolt.Net.Rest.Contracts;
using Revolt.Net.WebSocket.Entities;
using Revolt.Net.WebSocket.Events;
using Revolt.Net.WebSocket.JsonModels;

namespace Revolt.Net.WebSocket.Client
{
    public interface IRevoltWebSocketClient
    {
        AsyncEvent<ReadyEvent> Ready { get; }
        AsyncEvent<MessageReceivedEvent> MessageReceived { get; }
        AsyncEvent<ChannelStartTypingEvent> ChannelStartTyping { get; }
        AsyncEvent<ChannelStopTypingEvent> ChannelStopTyping { get; }

        internal AsyncEvent<JsonMessageReceivedMessage> RawMessageReceived { get; }

        internal Task<Message> CreateMessageAsync(JsonMessage message, CancellationToken cancellationToken);

        Task<Server> GetServerAsync(string serverId, CancellationToken cancellationToken);

        internal Task InvokeAsync<T>(T message, CancellationToken cancellationToken) where T : JsonWebSocketMessage;
        Task PinMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default);
        Task UnPinMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default);
        internal Task<Message> SendMessageAsync(SendMessageValues values, CancellationToken cancellationToken = default);
        Task<User> GetUserAsync(string userId, CancellationToken cancellationToken = default);
        Task DeleteMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default);
        Task<Message> GetMessageAsync(string channelId, string messageId, CancellationToken cancellationToken = default);
        Task<Channel> GetChannelAsync(string channelId, CancellationToken cancellationToken = default);
        Task<ChannelInvite> CreateChannelInviteAsync(string channelId, CancellationToken cancellationToken);
        Task<IReadOnlyList<ServerMemberUser>> GetServerMemberUsersAsync(string serverId, CancellationToken cancellationToken);
        internal Task<Message> EditMessageAsync(EditMessageValues values, CancellationToken cancellationToken = default);
    }
}