using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Entities
{
    public sealed class RestMessage
    {
        internal JsonMessage JsonModel;

        private RestMessage(JsonMessage data)
        {
            JsonModel = data;
        }

        public string Id => JsonModel.Id;

        public string AuthorId => JsonModel.AuthorId;

        public string ChannelId => JsonModel.ChannelId;

        public string? Content => JsonModel.Content;

        internal static RestMessage CreateNew(JsonMessage data)
        {
            return new RestMessage(data);
        }
    }
}
