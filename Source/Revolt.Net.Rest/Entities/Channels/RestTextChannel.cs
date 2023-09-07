using Revolt.Net.Rest;
using Revolt.Net.Rest.Helpers;

namespace Revolt.Net.Rest
{
    public class RestTextChannel : RestChannel, ITextChannel
    {
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
    }
}