using Revolt.Net.Core.Entities;
using Revolt.Net.Core.Enumerations;
using Revolt.Net.Core.JsonModels;
using Revolt.Net.Rest.Contracts;
using Revolt.Net.WebSocket.Client;

namespace Revolt.Net.WebSocket.Entities
{
    public class Channel : RevoltSocketEntity
    {
        internal JsonChannel JsonModal;

        private Channel(JsonChannel data, IRevoltWebSocketClient client) : base(client)
        {
            JsonModal = data;
        }

        public string Id => JsonModal.Id;

        public ChannelType ChannelType => JsonModal.ChannelType;

        public async Task<Message> GetMessageAsync(string messageId, CancellationToken cancellationToken = default)
        {
            return await Client.GetMessageAsync(Id, messageId, cancellationToken);
        }

        public async Task<Message> SendMessageAsync(
            string? content = null,
            IEnumerable<Embed>? embeds = null,
            CancellationToken cancellationToken = default)
        {
            var req = new SendMessageValues(
                Id,
                content,
                embeds ?? [],
                []
            );

            return await Client.SendMessageAsync(req, cancellationToken);
        }

        public async Task<ChannelInvite> CreateInviteAsync(CancellationToken cancellationToken = default)
        {
            return await Client.CreateChannelInviteAsync(Id, cancellationToken);
        }

        internal static Channel CreateNew(JsonChannel data, IRevoltWebSocketClient client)
        {
            return data switch
            {
                _ => new Channel(data, client),
            };
        }
    }
}
