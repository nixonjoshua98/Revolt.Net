using Revolt.Net.Rest;

namespace Revolt.Net.WebSocket
{
    public sealed class SocketUserMessage : SocketMessage
    {
        internal SocketUserMessage(RevoltSocketClient client, MessagePayload message, IMessageChannel channel, IUser user) : base(client, message, channel, user)
        {

        }

    }
}
