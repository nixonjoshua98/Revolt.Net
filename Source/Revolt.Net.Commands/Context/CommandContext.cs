using Revolt.Net.WebSocket;

namespace Revolt.Net.Commands.Context
{
    /// <summary> The context of a command which may contain the client, user, guild, channel, and message. </summary>
    public class CommandContext : ICommandContext
    {
        public string Arguments { get; set; }
        public SocketMessage Message { get; }
        public IUser User { get; }
        public SocketChannel Channel { get; }
        public RevoltSocketClient Client { get; }
        public SocketServer Server { get; }

        public CommandContext(SocketMessage message)
        {
            Arguments = message.Content;
            Message = message;
            User = message.Author;
            Channel = message.Channel;
            Client = message.Client;

            if (Channel is SocketTextChannel textChannel)
                Server = Client.GetServer(textChannel.ServerId);
        }
    }
}