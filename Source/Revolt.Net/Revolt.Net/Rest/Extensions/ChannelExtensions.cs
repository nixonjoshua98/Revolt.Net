using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Embeds;
using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Rest.Contracts.RestValues;

namespace Revolt.Net.Rest.Extensions
{
    public static class ChannelExtensions
    {
        public static async Task<Message> GetMessageAsync(
            this Channel channel,
            string messageId,
            CancellationToken cancellationToken = default)
        {
            var req = new GetMessageValues(
                channel.Id,
                messageId
            );

            return await channel.Client.GetMessageAsync(req, cancellationToken);
        }

        public static async Task<Message> SendMessageAsync(
            this Channel channel,
            string? content = null,
            IEnumerable<Embed>? embeds = null,
            CancellationToken cancellationToken = default)
        {
            var req = new SendMessageValues(
                channel.Id,
                content,
                embeds ?? [],
                []
            );

            return await channel.Client.SendMessageAsync(req, cancellationToken);
        }

        //public static async Task<ChannelInvite> CreateInviteAsync(
        //    this Channel channel,
        //    CancellationToken cancellationToken = default)
        //{
        //    var req = new CreateChannelInviteValues(
        //        channel.Id
        //    );

        //    return await channel.Client.CreateChannelInviteAsync(req, cancellationToken);
        //}
    }
}
