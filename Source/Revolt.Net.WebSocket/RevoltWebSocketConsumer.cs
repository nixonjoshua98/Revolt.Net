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

            Socket.MessageReceived += HandleMessage;
        }

        private async Task HandleMessage(SocketMessagePayload payload)
        {
            try
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
            }
            catch (Exception ex)
            {

            }
        }

        private Task OnMessageUpdate(SocketMessagePayload payload)
        {
            var e = RestSerialization.Deserialize<MessageUpdate>(payload.Content);

            var message = State.GetMessage(e.ChannelId, e.MessageId);

            message?.Update(e.Data, State);

            return Task.CompletedTask;
        }

        private async Task OnChannelDelete(SocketMessagePayload message)
        {
            var e = RestSerialization.Deserialize<ChannelDeletePayload>(message.Content);

            State.RemoveChannel(e.Id);

            ChannelDelete?.Invoke(e.ToEvent());
        }

        private async Task OnServerDelete(SocketMessagePayload message)
        {
            var e = RestSerialization.Deserialize<ServerDeletePayload>(message.Content);

            State.RemoveServer(e.Id);

            ServerDelete?.Invoke(e.ToEvent());
        }

        private async Task OnMessageDelete(SocketMessagePayload message)
        {
            var e = RestSerialization.Deserialize<MessageDeletePayload>(message.Content);

            State.RemoveMessage(e.Channel, e.Id);

            MessageDelete?.Invoke(e.ToEvent());
        }

        private async Task OnUserRelationship(SocketMessagePayload message)
        {
            var e = RestSerialization.Deserialize<UserRelationshipPayload>(message.Content);

            State.AddUser(e.User);

            UserRelationship.Invoke(e.ToEvent());
        }

        private async Task OnChannelCreate(SocketMessagePayload message)
        {
            var e = RestSerialization.Deserialize<ChannelCreatePayload>(message.Content);

            State.AddChannel(e.Channel);

            ChannelCreate?.Invoke(e.ToEvent());
        }

        private async Task OnUserUpdate(SocketMessagePayload message)
        {
            var e = RestSerialization.Deserialize<UserUpdateMessage>(message.Content);

            State.UpdateUser(e.Id, e.Data);

            UserUpdate?.Invoke(e.ToEvent());
        }

        private async Task OnMessage(SocketMessagePayload payload)
        {
            var data = RestSerialization.Deserialize<MessagePayload>(payload.Content);

            var channel = await Client.GetChannelAsync(data.ChannelId);
            var author = await Client.GetUserAsync(data.AuthorId);

            var message = SocketMessage.Create(Client, data, channel as ITextChannel, author);

            State.AddMessage(message);

            Message?.Invoke(new MessageEvent(message));
        }

        private async Task OnReady(SocketMessagePayload message)
        {
            var e = RestSerialization.Deserialize<ReadyMessage>(message.Content);

            State.Add(e);

            Ready?.Invoke(e.ToEvent());
        }
    }
}
