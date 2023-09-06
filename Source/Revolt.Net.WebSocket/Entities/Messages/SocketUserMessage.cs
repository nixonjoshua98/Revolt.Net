using Revolt.Net.WebSocket.State;

namespace Revolt.Net.WebSocket.Entities.Messages
{
    public sealed class SocketUserMessage : SocketMessage
    {
        internal SocketUserMessage(
            RevoltSocketClient client, 
            Message message,
            IChannel channel,
            IUser user) : base(client, message, channel, user)
        {

        }

        internal override void Update(Message message, RevoltState state)
        {
            base.Update(message, state);
        }

    }
}
