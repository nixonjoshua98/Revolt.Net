using Revolt.Net.Entities.Messages;
using Revolt.Net.Rest.Requests;

namespace Revolt.Net.Rest.ApiClient
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
