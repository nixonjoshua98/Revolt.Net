using Revolt.Net.WebSocket;

namespace Revolt.Net.Commands.Context
{
    /// <summary>
    /// Represents a context of a command. This may include the client, server, channel, user, and message.
    /// </summary>
    public interface ICommandContext
    {
        public string Arguments { get; set; }
        public SocketMessage Message { get; }
        public User User { get; }
        public Channel Channel { get; }
        public RevoltSocketClient Client { get; }
        public Server Server { get; }
    }
}