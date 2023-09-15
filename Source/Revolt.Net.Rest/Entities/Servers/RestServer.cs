using Revolt.Net.Rest.Helpers;
using System.Text.Json.Serialization;

namespace Revolt.Net.Rest
{
    public sealed class RestServer : RestEntity, IServer
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        [JsonPropertyName("channels")]
        public List<string> ChannelIds { get; init; } = new();

        public async Task<IUser> GetOwnerAsync() =>
            await Client.Api.GetUserAsync(OwnerId);

        public async Task<IEnumerable<ServerMemberUser>> GetServerMembersAsync(bool excludeOffline = false) =>
            await ChannelHelper.GetServerMemberUsersAsync(Client, Id, excludeOffline);
    }
}
