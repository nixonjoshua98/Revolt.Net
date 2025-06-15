using Revolt.Net.Core.Entities.Abstractions;
using Revolt.Net.Core.Enumerations;
using Revolt.Net.Core.JsonModels.Channels;
using Revolt.Net.Rest.Clients;

namespace Revolt.Net.Core.Entities.Channels
{
    public class Channel : RevoltClientEntity
    {
        internal JsonChannel JsonModal;

        private Channel(JsonChannel data, RevoltRestClient client) : base(client)
        {
            JsonModal = data;
        }

        public string Id => JsonModal.Id;

        public ChannelType ChannelType => JsonModal.ChannelType;

        internal static Channel CreateNew(JsonChannel data, RevoltRestClient client)
        {
            return data switch
            {
                _ => new Channel(data, client),
            };
        }
    }
}
