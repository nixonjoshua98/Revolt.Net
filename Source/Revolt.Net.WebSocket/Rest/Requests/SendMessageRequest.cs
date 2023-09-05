using Revolt.Net.WebSocket;

namespace Revolt.Net.Rest
{
    internal sealed class SendMessageRequest
    {
        public string Nonce { get; init; }
        public string Content { get; init; }
        public IEnumerable<MessageReply> Replies { get; init; }

        public SendMessageRequest(string content)
        {
            Content = content;
        }

        public SendMessageRequest(MessageReply messageReply, string content)
        {
            Replies = new MessageReply[1] { messageReply };
            Content = content;
        }
    }
}