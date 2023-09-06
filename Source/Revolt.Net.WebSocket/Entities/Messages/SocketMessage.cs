using Revolt.Net.Rest.Helpers;
using Revolt.Net.WebSocket.Entities.Messages;
using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket
{
    public abstract class SocketMessage : SocketEntity, IMessage
    {
        public string Id { get; private set; }

        public IUser Author { get; private set; }

        public IChannel Channel { get; private set; }

        public string Content { get; private set; }

        internal SocketMessage(RevoltSocketClient client, Message message, IChannel channel, IUser user)
        {
            Channel = channel;
            Client = client;
            Author = user;

            Id = message.Id;
        }

        internal static SocketMessage Create(
            RevoltSocketClient client, 
            Message message,
            IChannel channel,
            IUser user)
        {
            var inst = new SocketUserMessage(client, message, channel, user);

            inst.Update(message, client.State);

            return inst;
        }

        internal virtual void Update(Message message, RevoltState state)
        {
            message.Content.Match(x => Content = x);
        }

        public async Task AcknowledgeAsync() =>
            await Client.Api.AcknowledgeMessageAsync(Channel.Id, Id);

        public async Task DeleteAsync() =>
            await Client.Api.DeleteMessageAsync(Channel.Id, Id);

        public async Task RemoveReactionsAsync() =>
            await Client.Api.RemoveMessageReactionsAsync(Channel.Id, Id);

        public async Task<IClientMessage> ReplyAsync(string content, Embed embed = null, IEnumerable<Embed> embeds = null, bool mention = false) =>
            await ChannelHelper.SendMessageAsync(
                client: Client,
                channel: Channel,
                content: content,
                embed: embed,
                mention: mention,
                messageId: Id
            );
    }
}
