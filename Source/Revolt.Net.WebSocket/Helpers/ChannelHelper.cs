﻿using Revolt.Net.Rest;

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
        }

        public static async ValueTask<Channel> GetChannelAsync(
            RevoltSocketClient client,
            string channelId,
            FetchBehaviour behaviour)
        {
            return await FetchHelper.GetOrDownloadAsync(
                behaviour,
                () => client.State.GetChannel(channelId),
                () => client.Api.GetChannelAsync(channelId)
            );
        }

        public static async Task<ClientMessage> SendMessageAsync(
            RevoltSocketClient client,
            string channelId, 
            string messageId = null,
            string content = null,
            Embed embed = null,
            IEnumerable<Embed> embeds = null,
            bool mention = false)
        {
            bool hasReply = MessageReply.TryCreate(messageId, mention, out var reply);

            return await client.Api.SendMessageAsync(
                channelId,
                new SendMessageRequest(
                    hasReply ? new MessageReply[] { reply } : Enumerable.Empty<MessageReply>(),
                    content,
                    CreateEmbedsEnumerable(embeds, embed)
                )
            );
        }

        private static IEnumerable<Embed> CreateEmbedsEnumerable(IEnumerable<Embed> embeds, Embed embed)
        {
            embeds ??= Enumerable.Empty<Embed>();

            return embed is null ?
                embeds : embeds.Append(embed);
        }
    }
}
