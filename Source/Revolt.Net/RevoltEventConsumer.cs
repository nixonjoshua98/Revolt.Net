using Revolt.Net.Clients;
using Revolt.Net.State;
using Revolt.Net.Websocket.Events.Incoming;

namespace Revolt.Net
{
    internal sealed class RevoltEventConsumer
    {
        private readonly RevoltClient Client;
        private readonly RevoltState State;

        public RevoltEventConsumer(RevoltClient client)
        {
            Client = client;
            State = client.State;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            Client._Ready.Add(OnReady);
            Client._Message.Add(OnMessageEvent);
            Client._UserUpdated.Add(OnUserUpdated);
            Client._ChannelCreated.Add(OnChannelCreated);
            Client._RelationshipUpdated.Add(OnRelationshipUpdated);

            Client.MessageDeleted.Add(OnMessageDeleted);
            Client.ServerDeleted.Add(OnServerDeleted);
            Client.ChannelDeleted.Add(OnChannelDeleted);
        }

        private Task OnChannelCreated(ChannelCreateInternalEvent @event)
        {
            State.AddChannel(@event.Channel);

            return Task.CompletedTask;
        }

        private Task OnChannelDeleted(ChannelDeleteEvent @event)
        {
            State.RemoveChannel(@event.ChannelId);

            return Task.CompletedTask;
        }

        private Task OnServerDeleted(ServerDeleteEvent @event)
        {
            State.RemoveServer(@event.ServerId);

            return Task.CompletedTask;
        }

        private Task OnRelationshipUpdated(UserRelationshipInternalEvent @event)
        {
            State.AddUser(@event.User);

            return Task.CompletedTask;
        }

        private async Task OnUserUpdated(UserUpdateInternalEvent @event)
        {
            State.UpdateUser(@event.UserId, @event.User);

            await Client.UserUpdated.InvokeAsync(@event.ToPublicEvent());
        }

        private async Task OnMessageEvent(MessageEvent @event)
        {
            State.AddMessage(@event.Message);

            _ = await State.GetChannelAsync(@event.Message.ChannelId);
            _ = await State.GetUserAsync(@event.Message.AuthorId);

            await Client.Message.InvokeAsync(@event);
        }

        private Task OnMessageDeleted(MessageDeleteEvent @event)
        {
            State.RemoveMessage(@event.ChannelId, @event.MessageId);

            return Task.CompletedTask;
        }

        private async Task OnReady(ReadyInternalEvent @event)
        {
            State.Add(@event);

            await Client.Ready.InvokeAsync(@event.ToPublicEvent());
        }
    }
}
