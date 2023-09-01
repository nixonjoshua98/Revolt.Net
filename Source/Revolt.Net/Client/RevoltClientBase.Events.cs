using Revolt.Net.Commands._Original;
using Revolt.Net.Common;
using Revolt.Net.Logging;
using Revolt.Net.Websocket.Events.Incoming;

namespace Revolt.Net.Client
{
    public abstract partial class RevoltClientBase
    {
        internal AsyncEvent<Func<MessageEvent, Task>> _Message { get; } = new();
        internal AsyncEvent<Func<ReadyInternalEvent, Task>> _Ready { get; } = new();
        internal AsyncEvent<Func<UserUpdateInternalEvent, Task>> _UserUpdated { get; } = new();
        internal AsyncEvent<Func<ChannelCreateInternalEvent, Task>> _ChannelCreated { get; } = new();
        internal AsyncEvent<Func<UserRelationshipInternalEvent, Task>> _RelationshipUpdated { get; } = new();

        public AsyncEvent<Func<ChannelDeleteEvent, Task>> ChannelDeleted { get; } = new();
        public AsyncEvent<Func<ServerDeleteEvent, Task>> ServerDeleted { get; } = new();
        public AsyncEvent<Func<ReadyEvent, Task>> Ready { get; } = new();
        public AsyncEvent<Func<MessageEvent, Task>> Message { get; } = new();
        public AsyncEvent<Func<MessageDeleteEvent, Task>> MessageDeleted { get; } = new();
        public AsyncEvent<Func<UserUpdateEvent, Task>> UserUpdated { get; } = new();

        public event Func<LogMessage, Task> Log { add => LogManager.Message += value; remove => LogManager.Message -= value; }
    }
}
