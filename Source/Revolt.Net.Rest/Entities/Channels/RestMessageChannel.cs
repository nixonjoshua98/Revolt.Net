using Revolt.Net.Rest.Clients;
using Revolt.Net.Rest.Helpers;

namespace Revolt.Net.Rest
{
    public abstract class RestMessageChannel : RestChannel, IRestTextChannel
    {
        internal RestMessageChannel(RevoltClientBase client, string id) : base(client, id) 
        {

        }

        public async Task<IClientMessage> SendMessageAsync(
        string content = null,
        Embed embed = null,
        IEnumerable<Embed> embeds = null) =>
            await ChannelHelper.SendMessageAsync(
                client: Client,
                channel: this,
                content: content,
                embed: embed,
                embeds: embeds
        );

        public async Task<IMessage> GetMessageAsync(string messageId) =>
            await Client.Api.GetMessageAsync(Id, messageId);

        public async Task<RestMessagesResponse> GetMessagesAsync(
            int limit,
            string beforeMessageId = default,
            string afterMessageId = default,
            MessageSort sort = MessageSort.Relevance,
            string nearbyMessageId = default,
            bool includeUsers = true) =>
            await Client.Api.GetMessagesAsync(
                Id,
                limit,
                beforeMessageId,
                afterMessageId,
                sort,
                nearbyMessageId,
                includeUsers
            );
    }
}
