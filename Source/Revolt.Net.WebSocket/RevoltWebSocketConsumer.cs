using Revolt.Net.WebSocket.Json;
using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket
{
    internal sealed class RevoltWebSocketConsumer
    {
        private readonly RevoltState State;
        private readonly RevoltWebSocketConnection Socket;

        internal event Func<UserRelationshipEvent, Task> UserRelationship = default!;
        internal event Func<ChannelCreateEvent, Task> ChannelCreate = default!;
        internal event Func<ChannelDeleteEvent, Task> ChannelDelete = default!;
        internal event Func<MessageDeleteEvent, Task> MessageDelete = default!;
        internal event Func<ServerDeleteEvent, Task> ServerDelete = default!;
        internal event Func<UserUpdateEvent, Task> UserUpdate = default!;
        internal event Func<MessageEvent, Task> Message = default!;
        internal event Func<ReadyEvent, Task> Ready = default!;

        public RevoltWebSocketConsumer(RevoltWebSocketConnection socket, RevoltState state)
        {
            State = state;
            Socket = socket;

            Socket.MessageReceived += message =>
            {
                return message.Type switch
                {
                    "Ready" => OnReady(message),
                    "ChannelDelete" => OnChannelDelete(message),
                    "MessageDelete" => OnMessageDelete(message),
                    "UserRelationship" => OnUserRelationship(message),
                    "UserUpdate" => OnUserUpdate(message),
                    "Message" => OnMessage(message),
                    "ChannelCreate" => OnChannelCreate(message),
                    "ServerDelete" => OnServerDelete(message),
                    _ => Task.CompletedTask
                };
            };
        }

        private async Task OnChannelDelete(WebSocketMessage message)
        {
            var e = Serialization.Deserialize<ChannelDeletePayload>(message.Content);

            State.RemoveChannel(e.Id);

            ChannelDelete?.Invoke(e.ToEvent());
        }

        private async Task OnServerDelete(WebSocketMessage message)
        {
            var e = Serialization.Deserialize<ServerDeletePayload>(message.Content);

            State.RemoveServer(e.Id);

            ServerDelete?.Invoke(e.ToEvent());
        }

        private async Task OnMessageDelete(WebSocketMessage message)
        {
            var e = Serialization.Deserialize<MessageDeletePayload>(message.Content);

            State.Messages.Remove(e.Channel, e.Id);

            MessageDelete?.Invoke(e.ToEvent());
        }

        private async Task OnUserRelationship(WebSocketMessage message)
        {
            var e = Serialization.Deserialize<UserRelationshipPayload>(message.Content);

            State.AddUser(e.User);

            UserRelationship.Invoke(e.ToEvent());
        }

        private async Task OnChannelCreate(WebSocketMessage message)
        {
            var e = Serialization.Deserialize<ChannelCreatePayload>(message.Content);

            State.AddChannel(e.Channel);

            ChannelCreate?.Invoke(e.ToEvent());
        }

        private async Task OnUserUpdate(WebSocketMessage message)
        {
            var e = Serialization.Deserialize<UserUpdateMessage>(message.Content);

            State.UpdateUser(e.Id, e.Data);

            UserUpdate?.Invoke(e.ToEvent());
        }

        private async Task OnMessage(WebSocketMessage message)
        {
            var e = Serialization.Deserialize<Message>(message.Content);

            State.Messages.Add(e);

            _ = await State.GetChannelAsync(e.ChannelId);
            _ = await State.GetUserAsync(e.AuthorId);

            Message?.Invoke(new MessageEvent(e));
        }

        private async Task OnReady(WebSocketMessage message)
        {
            var e = Serialization.Deserialize<ReadyMessage>(message.Content);

            State.Add(e);

            Ready?.Invoke(e.ToEvent());
        }
    }
}
