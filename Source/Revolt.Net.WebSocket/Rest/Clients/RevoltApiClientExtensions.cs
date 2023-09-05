using Revolt.Net.WebSocket;

namespace Revolt.Net.Rest
{
    internal static class RevoltApiClientExtensions
    {
        public static async Task<ClientMessage> SendMessageAsync(this RevoltApiClient client, string channel, string content)
        {
            return await client.SendMessageAsync(
                channel,
                new SendMessageRequest(
                    content
                )
            );
        }

        public static async Task<ClientMessage> SendMessageAsync(
            this RevoltApiClient client,
            string channel,
            string messageId,
            string content,
            bool mention = false)
        {
            return await client.SendMessageAsync(
                channel,
                new SendMessageRequest(
                    new MessageReply(messageId, mention),
                    content
                )
            );
        }
    }
}
