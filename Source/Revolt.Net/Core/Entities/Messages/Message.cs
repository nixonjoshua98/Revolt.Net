using Revolt.Net.Core.Entities.Abstractions;
using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Core.Exceptions;
using Revolt.Net.Core.JsonModels.Messages;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Messages
{
    public class Message : RevoltClientEntity
    {
        internal JsonMessage JsonModel;

        internal Message(JsonMessage data, RevoltRestClient restClient) : base(restClient)
        {
            JsonModel = data;

            Author = new User(
                RevoltException.ThrowIfNull(data.User, nameof(data.User)), 
                restClient
            );
        }

        public string Id => JsonModel.Id;

        public string AuthorId => JsonModel.AuthorId;

        public string ChannelId => JsonModel.ChannelId;

        public string? Content => JsonModel.Content;

        public User Author { get; init; }

        internal static Message CreateNew(JsonMessage message, RevoltRestClient restClient)
        {
            return message.Member switch
            {
                null => new Message(message, restClient),
                _ => new ServerMessage(message, restClient)
            };
        }
    }
}
