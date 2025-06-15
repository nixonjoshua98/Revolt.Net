using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.Exceptions;
using Revolt.Net.Core.JsonModels.Messages;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Messages
{
    public sealed class ServerMessage : Message
    {
        internal ServerMessage(JsonMessage message, RevoltRestClient restClient) : base(message, restClient)
        {
            Author = new ServerMemberUser(
                RevoltException.ThrowIfNull(message.User, nameof(message.User)),
                RevoltException.ThrowIfNull(message.Member, nameof(message.Member)),
                restClient
            );
        }

        public string ServerId => Author.ServerId;

        public new ServerMemberUser Author { get; init; }
    }
}
