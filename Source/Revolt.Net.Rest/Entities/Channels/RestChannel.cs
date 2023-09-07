using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public abstract class RestChannel : RestEntity, IChannel
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        public ChannelType ChannelType { get; init; }

        protected RestChannel()
        {

        }
    }
}
