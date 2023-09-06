using Revolt.Net.Rest;
using Revolt.Net.WebSocket.State;
using System.Threading.Channels;

namespace Revolt.Net.WebSocket.Helpers
{
    internal static class ChannelHelper
    {
        public static async Task DeleteMessageAsync(
            RevoltSocketClient client,
            string channelId,
            string messageId)
        {
            await client.Api.DeleteMessageAsync(channelId, messageId);

            client.State.RemoveMessage(channelId, messageId);
        }

        public static async ValueTask<SocketChannel> GetChannelAsync(
            RevoltSocketClient client,
            string channelId,
            FetchBehaviour behaviour)
        {
            return await FetchHelper.GetOrDownloadAsync(
                behaviour,
                () => client.State.GetChannel(channelId),
                () => client.Api.GetChannelAsync(channelId),
                client.State.AddChannel
            );
        }

        public static async ValueTask<SocketMessage> GetMessageAsync(
            RevoltSocketClient client,
            string channelId,
            string messageId,
            FetchBehaviour behaviour)
        {
            return await FetchHelper.GetOrDownloadAsync(
                behaviour,
                () => client.State.GetMessage(channelId, messageId),
                () => client.Api.GetMessageAsync(channelId, messageId),
                client.State.AddMessage
            );
        }

        public static async Task<SocketClientMessage> SendMessageAsync(
            RevoltSocketClient client,
            string channelId, 
            string messageId = null,
            string content = null,
            Embed embed = null,
            IEnumerable<Embed> embeds = null,
            bool mention = false)
        {
            bool hasReply = MessageReply.TryCreate(messageId, mention, out var reply);

            var resp = await client.Api.SendMessageAsync(
                channelId,
                new SendMessageRequest(
                    hasReply ? new MessageReply[] { reply } : Enumerable.Empty<MessageReply>(),
                    content,
                    CreateEmbedsEnumerable(embeds, embed)
                )
            );

            if (reply is not null)
            {
                client.State.AddMessage(resp);
            }

            return resp;
        }

        private static IEnumerable<Embed> CreateEmbedsEnumerable(IEnumerable<Embed> embeds, Embed embed)
        {
            embeds ??= Enumerable.Empty<Embed>();

            return embed is null ?
                embeds : embeds.Append(embed);
        }
    }
}
