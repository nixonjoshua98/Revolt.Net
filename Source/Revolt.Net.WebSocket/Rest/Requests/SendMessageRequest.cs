using Revolt.Net.WebSocket;

namespace Revolt.Net.Rest
{
    internal sealed class SendMessageRequest
    {
        public string Nonce { get; init; }
        public string Content { get; init; }
        public IEnumerable<MessageReply> Replies { get; init; }
        public IEnumerable<Embed> Embeds { get; init; }

        public SendMessageRequest(
            IEnumerable<MessageReply> replies, 
            string content,
            IEnumerable<Embed> embeds)
        {
            Replies = replies;
            Content = content;
            Embeds = embeds;
        }
    }
}