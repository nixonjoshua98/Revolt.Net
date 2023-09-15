using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket;

namespace Revolt.Net.Commands
{
    public class CommandContext : ICommandContext
    {
        public string Arguments { get; }
        public SocketMessage Message { get; }
        public IUser User { get; }
        public IMessageChannel Channel { get; }
        public RevoltClientBase Client { get; }
        public IServer Server { get; }

        public CommandContext(SocketMessage message)
        {
            Arguments = message.Content;
            Message = message;
            User = message.Author;
            Channel = message.Channel;
            Client = message.Client;
        }
    }
}