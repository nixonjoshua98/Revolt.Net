using Revolt.Net.Common;
using Revolt.Net.Logging;
using Revolt.Net.Websocket.Events.Incoming;

namespace Revolt.Net.Client
{
    public abstract partial class RevoltClientBase
    {
        internal AsyncEvent<MessageEvent> _Message { get; } = new();
        internal AsyncEvent<ReadyInternalEvent> _Ready { get; } = new();
        internal AsyncEvent<UserUpdateInternalEvent> _UserUpdated { get; } = new();
        internal AsyncEvent<ChannelCreateInternalEvent> _ChannelCreated { get; } = new();
        internal AsyncEvent<UserRelationshipInternalEvent> _RelationshipUpdated { get; } = new();

        public AsyncEvent<ChannelDeleteEvent> ChannelDeleted { get; } = new();
        public AsyncEvent<ServerDeleteEvent> ServerDeleted { get; } = new();
        public AsyncEvent<ReadyEvent> Ready { get; } = new();
        public AsyncEvent<MessageEvent> Message { get; } = new();
        public AsyncEvent<MessageDeleteEvent> MessageDeleted { get; } = new();
        public AsyncEvent<UserUpdateEvent> UserUpdated { get; } = new();

        public event Func<LogMessage, Task> Log { add => LogManager.Message += value; remove => LogManager.Message -= value; }
    }
}
