using Revolt.Net.Client;
using Revolt.Net.Entities.Channels;
using Revolt.Net.Entities.Messages;
using Revolt.Net.Entities.Servers;
using Revolt.Net.Entities.Users;

namespace Revolt.Net.Commands.Context
{
    /// <summary>
    /// Represents a context of a command. This may include the client, guild, channel, user, and message.
    /// </summary>
    public interface ICommandContext
    {
        public string Arguments { get; set; }
        public Message Message { get; }
        public User User { get; }
        public Channel Channel { get; }
        public RevoltClient Client { get; }
        public Server Server { get; }
    }
}