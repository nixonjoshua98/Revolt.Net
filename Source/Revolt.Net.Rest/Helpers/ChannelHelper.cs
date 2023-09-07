using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Rest.Helpers
{
    internal static class ChannelHelper
    {
        public static async Task<IChannel> GetChannelAsync(
            RevoltClientBase client,
            string id)
        {
            var channel = await client.Api.GetChannelAsync(id);

            channel?.SetClient(client);

            return channel;
        }

        public static async Task<RestClientMessage> SendMessageAsync(
            RevoltClientBase client,
            ITextChannel channel,
            string messageId = null,
            string content = null,
            Embed embed = null,
            IEnumerable<Embed> embeds = null,
            bool mention = false)
        {
            bool hasReply = MessageReply.TryCreate(messageId, mention, out var reply);

            var message = await client.Api.SendMessageAsync(
                channel.Id,
                new SendMessageRequest(
                    hasReply ? new MessageReply[] { reply } : Enumerable.Empty<MessageReply>(),
                    content,
                    CreateEmbedsEnumerable(embeds, embed)
                )
            );

            message?.Update(client, channel, client.User);

            return message;
        }

        private static IEnumerable<Embed> CreateEmbedsEnumerable(IEnumerable<Embed> embeds, Embed embed)
        {
            embeds ??= Enumerable.Empty<Embed>();

            return embed is null ?
                embeds : embeds.Append(embed);
        }
    }
}
