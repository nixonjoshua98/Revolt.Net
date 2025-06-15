using Revolt.Net.Core.Enumerations;
using Revolt.Net.Core.JsonModels;

namespace Revolt.Net.Rest.Entities
{
    internal sealed class RestChannel
    {
        internal JsonChannel JsonModel;

        private RestChannel(JsonChannel jsonModel)
        {
            JsonModel = jsonModel;
        }

        public string Id => JsonModel.Id;

        public ChannelType ChannelType => JsonModel.ChannelType;

        public static RestChannel CreateNew(JsonChannel data)
        {
            return new RestChannel(data);
        }
    }
}
