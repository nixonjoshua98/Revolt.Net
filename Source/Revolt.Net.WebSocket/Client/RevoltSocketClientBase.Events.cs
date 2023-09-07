using Revolt.Net.Core.Logging;

namespace Revolt.Net.WebSocket
{
    public abstract partial class RevoltSocketClientBase
    {
        public event Func<UserRelationshipEvent, Task> UserRelationship = default!;
        public event Func<ChannelCreateEvent, Task> ChannelCreate = default!;
        public event Func<ChannelDeleteEvent, Task> ChannelDelete = default!;
        public event Func<MessageDeleteEvent, Task> MessageDelete = default!;
        public event Func<ServerDeleteEvent, Task> ServerDelete = default!;
        public event Func<UserUpdateEvent, Task> UserUpdate = default!;
        public event Func<MessageEvent, Task> Message = default!;
        public event Func<ReadyEvent, Task> Ready = default!;

        public event Func<LogMessage, Task> Log { add => LogManager.Message += value; remove => LogManager.Message -= value; }

        internal void SetupEvents(RevoltWebSocketConsumer consumer)
        {
            consumer.UserRelationship += e => UserRelationship?.Invoke(e);
            consumer.ChannelCreate += e => ChannelCreate?.Invoke(e);
            consumer.ChannelDelete += e => ChannelDelete?.Invoke(e);
            consumer.MessageDelete += e => MessageDelete?.Invoke(e);
            consumer.ServerDelete += e => ServerDelete?.Invoke(e);
            consumer.UserUpdate += e => UserUpdate?.Invoke(e);
            consumer.Message += e => Message?.Invoke(e);
            consumer.Ready += e => Ready?.Invoke(e);
        }
    }
}
