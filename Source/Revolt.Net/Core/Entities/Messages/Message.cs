using Revolt.Net.Core.Entities.Abstractions;
using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.JsonModels.Messages;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Messages
{
    public sealed class Message : RevoltClientEntity
    {
        internal JsonMessage JsonModel;

        internal Message(JsonMessage message, RevoltRestClient restClient) : base(restClient)
        {
            JsonModel = message;
            Author = new Member(message.Member, restClient);
        }

        public string Id => JsonModel.Id;

        public string AuthorId => JsonModel.AuthorId;

        public string ChannelId => JsonModel.ChannelId;

        public string? Content => JsonModel.Content;

        public Member Author { get; private set; }

        internal void UpdateJsonModel(JsonMessage message)
        {
            JsonModel = message;
            Author = new Member(message.Member, Client);
        }
    }
}
