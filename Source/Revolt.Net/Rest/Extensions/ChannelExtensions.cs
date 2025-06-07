using Revolt.Net.Core.Entities.Channels;
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

            return await channel.Client.GetMessageAsync( req, cancellationToken );
        }
    }
}
