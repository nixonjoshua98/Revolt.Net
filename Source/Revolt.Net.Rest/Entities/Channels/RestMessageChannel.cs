using Revolt.Net.Rest;
using Revolt.Net.Rest.Helpers;

namespace Revolt.Net.Rest
{
    public abstract class RestMessageChannel : RestChannel
    {
        public async Task<RestClientMessage> SendMessageAsync(string content = null, Embed embed = null, IEnumerable<Embed> embeds = null) =>
            await ChannelHelper.SendMessageAsync(
                client: Client,
                channel: this,
                content: content,
                embed: embed,
                embeds: embeds
            );

        public async ValueTask<RestMessage> GetMessageAsync(string messageId) =>
            await Client.Api.GetMessageAsync(Id, messageId);
    }
}