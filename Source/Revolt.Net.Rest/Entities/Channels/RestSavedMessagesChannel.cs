using Revolt.Net.Rest.Clients;
using Revolt.Net.Rest.Entities.Channels;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestSavedMessagesChannel : RestMessageChannel
    {
        [JsonPropertyName("user")]
        public string UserId { get; init; } = default!;

        public async Task<IUser> GetUserAsync() =>
            await Client.Api.GetUserAsync(UserId);

        internal RestSavedMessagesChannel(RevoltClientBase client, string id, string userId) : base(client, id)
        {
            UserId = userId;
        }

        internal new static RestSavedMessagesChannel Create(RevoltClientBase client, ChannelPayload channel)
        {
            var chnl = new RestSavedMessagesChannel(client, channel.Id, channel.UserId.Value);

            chnl.Update(channel);

            return chnl;
        }
    }
}