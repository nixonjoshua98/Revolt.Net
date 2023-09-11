using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket;

namespace Revolt.Net.Commands
{
    /// <summary>
    /// Represents a context of a command. This may include the client, server, channel, user, and message.
    /// </summary>
    public interface ICommandContext
    {
        public string Arguments { get; }
        public SocketMessage Message { get; }
        public IUser User { get; }
        public IMessageChannel Channel { get; }
        public RevoltClientBase Client { get; }
        IServer Server { get; }
    }
}