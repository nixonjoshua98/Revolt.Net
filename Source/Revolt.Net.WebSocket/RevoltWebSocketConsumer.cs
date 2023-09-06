using Revolt.Net.Rest;
using Revolt.Net.Rest.Json;
using Revolt.Net.WebSocket.Payloads;
using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket
{
    internal sealed class RevoltWebSocketConsumer
    {
        private readonly RevoltState State;
        private readonly RevoltSocketClient Client;
        private readonly RevoltWebSocketConnection Socket;

        internal event Func<UserRelationshipEvent, Task> UserRelationship = default!;
        internal event Func<ChannelCreateEvent, Task> ChannelCreate = default!;
        internal event Func<ChannelDeleteEvent, Task> ChannelDelete = default!;
        internal event Func<MessageDeleteEvent, Task> MessageDelete = default!;
        internal event Func<ServerDeleteEvent, Task> ServerDelete = default!;
        internal event Func<UserUpdateEvent, Task> UserUpdate = default!;
        internal event Func<MessageEvent, Task> Message = default!;
        internal event Func<ReadyEvent, Task> Ready = default!;

        public RevoltWebSocketConsumer(RevoltSocketClient client, RevoltWebSocketConnection socket, RevoltState state)
        {
            State = state;
            Client = client;
            Socket = socket;

            Socket.MessageReceived += async payload =>
            {
                await (payload.Type switch
                {
                    "Ready" => OnReady(payload),
                    "ChannelDelete" => OnChannelDelete(payload),
                    "MessageDelete" => OnMessageDelete(payload),
                    "UserRelationship" => OnUserRelationship(payload),
                    "UserUpdate" => OnUserUpdate(payload),
                    "Message" => OnMessage(payload),
                    "ChannelCreate" => OnChannelCreate(payload),
                    "ServerDelete" => OnServerDelete(payload),
                    "MessageUpdate" => OnMessageUpdate(payload),
                    _ => Task.CompletedTask
                });
            };
        }

        private Task OnMessageUpdate(SocketMessagePayload payload)
        {
            var e = Serialization.Deserialize<MessageUpdate>(payload.Content);

            var message = State.GetMessage(e.ChannelId, e.MessageId);

            message?.Update(e.Data, State);

            return Task.CompletedTask;
        }

        private async Task OnChannelDelete(SocketMessagePayload message)
        {
            var e = Serialization.Deserialize<ChannelDeletePayload>(message.Content);

            State.RemoveChannel(e.Id);

            ChannelDelete?.Invoke(e.ToEvent());
        }

        private async Task OnServerDelete(SocketMessagePayload message)
        {
            var e = Serialization.Deserialize<ServerDeletePayload>(message.Content);

            State.RemoveServer(e.Id);

            ServerDelete?.Invoke(e.ToEvent());
        }

        private async Task OnMessageDelete(SocketMessagePayload message)
        {
            var e = Serialization.Deserialize<MessageDeletePayload>(message.Content);

            State.RemoveMessage(e.Channel, e.Id);

            MessageDelete?.Invoke(e.ToEvent());
        }

        private async Task OnUserRelationship(SocketMessagePayload message)
        {
            var e = Serialization.Deserialize<UserRelationshipPayload>(message.Content);

            State.AddUser(e.User);

            UserRelationship.Invoke(e.ToEvent());
        }

        private async Task OnChannelCreate(SocketMessagePayload message)
        {
            var e = Serialization.Deserialize<ChannelCreatePayload>(message.Content);

            State.AddChannel(e.Channel);

            ChannelCreate?.Invoke(e.ToEvent());
        }

        private async Task OnUserUpdate(SocketMessagePayload message)
        {
            var e = Serialization.Deserialize<UserUpdateMessage>(message.Content);

            State.UpdateUser(e.Id, e.Data);

            UserUpdate?.Invoke(e.ToEvent());
        }

        private async Task OnMessage(SocketMessagePayload socketMessage)
        {
            var data = Serialization.Deserialize<MessagePayload>(socketMessage.Content);

            var channel = await Client.GetChannelAsync(data.ChannelId);
            var author = await Client.GetUserAsync(data.AuthorId);

            var message = SocketMessage.Create(Client, data, channel, author);

            State.AddMessage(message);

            Message?.Invoke(new MessageEvent(message));
        }

        private async Task OnReady(SocketMessagePayload message)
        {
            var e = Serialization.Deserialize<ReadyMessage>(message.Content);

            State.Add(e);

            Ready?.Invoke(e.ToEvent());
        }
    }
}
