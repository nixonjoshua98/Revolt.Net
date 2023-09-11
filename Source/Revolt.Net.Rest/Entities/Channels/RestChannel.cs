using Revolt.Net.Rest.Clients;
using Revolt.Net.Rest.Entities.Channels;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public class RestChannel : RestEntity, IChannel
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; }

        protected RestChannel(RevoltClientBase client, string id)
        {
            Id = id;
            Client = client;
        }

        internal static RestChannel Create(RevoltClientBase client, ChannelPayload channel)
        {
            return channel.ChannelType switch
            {
                ChannelType.SavedMessages => RestSavedMessagesChannel.Create(client, channel),
                ChannelType.DirectMessage => RestDirectMessageChannel.Create(client, channel),
                ChannelType.Group => RestGroupChannel.Create(client, channel),
                ChannelType.TextChannel => RestTextChannel.Create(client, channel),
                _ => new RestChannel(client, channel.Id)
            };
        }

        internal virtual void Update(ChannelPayload channel)
        {

        }
    }
}
