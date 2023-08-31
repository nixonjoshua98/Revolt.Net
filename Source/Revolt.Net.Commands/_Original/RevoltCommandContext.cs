using Revolt.Net.Clients;
using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.Entities.Users;

namespace Revolt.Commands
{
    /// <summary> The context of a command which may contain the client, user, guild, channel, and message. </summary>
    public class RevoltCommandContext : ICommandContext
    {
        public string Arguments { get; set; }
        public Message Message { get; }
        public User User { get; }
        public Channel Channel { get; }
        public RevoltBotClient Client { get; }
        public Server Server { get; }

        public RevoltCommandContext(Message message)
        {
            Arguments = message.Content;
            Message = message;
            User = message.Author;
            Channel = message.Channel;
            Client = message.Client;

            if (Channel is TextChannel)
                Server = Client.GetServer(Channel.Id);
        }
    }
}