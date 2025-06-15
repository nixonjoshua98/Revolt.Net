using Revolt.Net.Core.Entities;
using Revolt.Net.Core.Exceptions;
using Revolt.Net.Core.JsonModels;
using Revolt.Net.Rest.Contracts;
using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public class Message : RevoltSocketEntity
    {
        internal JsonMessage JsonModel = default!;

        internal Message(JsonMessage data, IRevoltWebSocketClient client) : base(client)
        {
            Update(data);
        }

        public string Id => JsonModel.Id;

        public string AuthorId => JsonModel.AuthorId;

        public string ChannelId => JsonModel.ChannelId;

        public string? Content => JsonModel.Content;

        public User Author { get; private set; } = default!;

        public async Task<Message> ReplyAsync(
            string? content = null,
            bool mention = false,
            bool failIfNotExists = true,
            IEnumerable<Embed>? embeds = null,
            CancellationToken cancellationToken = default)
        {
            var req = new SendMessageValues(
                ChannelId,
                content,
                embeds ?? [],
                [new JsonMessageReply(Id, mention, failIfNotExists)]
            );

            return await Client.SendMessageAsync(req, cancellationToken);
        }

        internal void Update(JsonMessage data)
        {
            JsonModel = data;

            Author = new User(
                RevoltException.ThrowIfNull(data.User, nameof(data.User)),
                Client
            );
        }

        public async Task DeleteAsync(CancellationToken cancellationToken = default)
        {
            await Client.DeleteMessageAsync(ChannelId, Id, cancellationToken);
        }

        public async Task EditAsync(
            string? content = null,
            IEnumerable<Embed>? embeds = null,
            CancellationToken cancellationToken = default)
        {
            var req = new EditMessageValues(
                ChannelId,
                Id,
                content,
                embeds
            );

            var message = await Client.EditMessageAsync(req, cancellationToken);

            Update(message.JsonModel);
        }

        public async Task PinAsync(CancellationToken cancellationToken = default)
        {
            await Client.PinMessageAsync(ChannelId, Id, cancellationToken);
        }

        public async Task UnPinAsync(CancellationToken cancellationToken = default)
        {
            await Client.UnPinMessageAsync(ChannelId, Id, cancellationToken);
        }

        public async Task<Channel> GetChannelAsync(CancellationToken cancellationToken = default)
        {
            return await Client.GetChannelAsync(ChannelId, cancellationToken);
        }
    }
}
