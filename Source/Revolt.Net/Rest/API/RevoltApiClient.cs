using Revolt.Net.Core.Entities;
using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Rest.API.Responses;

namespace Revolt.Net.Rest.API
{
    internal sealed class RevoltApiClient
    {
        public readonly RevoltRestClient Client;

        public RevoltApiClient(RevoltRestClient client)
        {
            Client = client;
        }

        public async Task<RevoltApiInformation?> GetApiInformationAsync()
            => await Client.SendAsync<RevoltApiInformation>("GET", string.Empty);

        public async Task<User?> GetSelfUserAsync()
            => await Client.SendAsync<User>("GET", "users/@me");

        public async Task<User?> GetUserAsync(string userId)
            => await Client.SendAsync<User>("GET", $"users/{userId}");

        public async Task<Channel?> GetChannelAsync(string channelId) =>
            await Client.SendAsync<Channel>("GET", $"channels/{channelId}");

        public async Task<ServerMembersResponse?> GetServerMembersAsync(string id, bool excludeOffline)
            => await Client.SendAsync<ServerMembersResponse>("GET", $"servers/{id}/members", new()
            {
                ["exclude_offline"] = excludeOffline
            });

        public async Task<ServerMember?> GetServerMemberAsync(string serverId, string userId)
            => await Client.SendAsync<ServerMember>("GET", $"servers/{serverId}/members/{userId}");


    }
}
