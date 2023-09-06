using Revolt.Net.WebSocket.Helpers;
using Revolt.Net.WebSocket.State;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket
{
    public sealed class SocketServer : SocketEntity
    {
        [JsonPropertyName("_id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("owner")]
        public string OwnerId { get; init; } = default!;

        [JsonPropertyName("channels")]
        public List<string> ChannelIds { get; init; } = new();

        public SocketUser Owner => Client.State.GetUser(OwnerId);

        public IEnumerable<ServerMember> Members => Client.State.GetServerMembers(Id);

        public ServerMember GetServerMember(string userId) => Client.State.GetServerMember(Id, userId);

        public async ValueTask<SocketUser> GetOwnerAsync(FetchBehaviour behaviour = FetchBehaviour.CacheThenDownload) =>
            await UserHelper.GetUserAsync(Client, OwnerId, behaviour);

        public async Task<IEnumerable<ServerMember>> GetServerMembersAsync(bool excludeOffline = false)
        {
            var resp = await Client.Api.GetServerMembersAsync(Id, excludeOffline);

            Client.State.Add(Id, resp);

            return Members;
        }
    }
}
