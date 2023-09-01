using Revolt.Net.Client;
using Revolt.Net.Entities.Channels;
using Revolt.Net.Entities.Messages;
using Revolt.Net.Entities.Servers;
using Revolt.Net.Entities.Users;

namespace Revolt.Net.Commands.Context
{
    /// <summary> The context of a command which may contain the client, user, guild, channel, and message. </summary>
    public class CommandContext : ICommandContext
    {
        public string Arguments { get; set; }
        public Message Message { get; }
        public User User { get; }
        public Channel Channel { get; }
        public RevoltClient Client { get; }
        public Server Server { get; }

        public CommandContext(Message message)
        {
            Arguments = message.Content;
            Message = message;
            User = message.Author;
            Channel = message.Channel;
            Client = message.Client;

            if (Channel is TextChannel textChannel)
                Server = Client.GetServer(textChannel.ServerId);
        }
    }
}