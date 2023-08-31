using Revolt.Net.Clients;
using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Messages;
using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.Entities.Users;

namespace Revolt.Commands
{
    /// <summary>
    ///     Represents a context of a command. This may include the client, guild, channel, user, and message.
    /// </summary>
    public interface ICommandContext
    {
        public string Arguments { get; set; }
        public Message Message { get; }
        public User User { get; }
        public Channel Channel { get; }
        public RevoltBotClient Client { get; }
        public Server Server { get; }
    }
}