using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public sealed class SocketServer : SocketEntity, IServer
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        [JsonPropertyName("channels")]
        public List<string> ChannelIds { get; init; } = new();

        public IEnumerable<ServerMember> Members => Client.State.GetServerMembers(Id);

        public ServerMember GetServerMember(string userId) => Client.State.GetServerMember(Id, userId);

        public async ValueTask<IUser> GetOwnerAsync() =>
            await Client.Api.GetUserAsync(OwnerId);

        public async Task<IEnumerable<ServerMember>> GetServerMembersAsync(bool excludeOffline = false)
        {
            var resp = await Client.Api.GetServerMembersAsync(Id, excludeOffline);

            Client.State.Add(Id, resp);

            return Members;
        }
    }
}
