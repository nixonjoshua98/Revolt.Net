using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket.Helpers
{
    internal static class MessageHelper
    {
        public static async Task<ClientMessage> SendAsync(
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

            client.State.TryAddMessage(resp);

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
