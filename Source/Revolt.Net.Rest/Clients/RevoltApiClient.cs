namespace Revolt.Net.Rest
{
    internal sealed class RevoltApiClient
    {
        public readonly RevoltRestClient Client;

        public RevoltApiClient(RevoltRestClient client)
        {
            Client = client;
        }

        public async Task LeaveOrDeleteServerAsync(string id) =>
            await Client.SendAsync("DELETE", $"servers/{id}");

        public async Task RemoveMessageReactionsAsync(string channelId, string messageId) =>
            await Client.SendAsync("DELETE", $"channels/{channelId}/messages/{messageId}/reactions");

        public async Task<RevoltApiInformation> GetApiInformationAsync()
            => await Client.SendAsync<RevoltApiInformation>("GET", string.Empty);

        public async Task<RestUser> GetClientUserAsync()
            => await Client.SendAsync<RestUser>("GET", "users/@me");

        public async Task<RestUser> GetUserAsync(string userId)
            => await Client.SendAsync<RestUser>("GET", $"users/{userId}");

        public async Task<RestChannel> GetChannelAsync(string channelId) =>
            await Client.SendAsync<RestChannel>("GET", $"channels/{channelId}");

        public async Task<ServerMembersResponse> GetServerMembersAsync(string id, bool excludeOffline)
            => await Client.SendAsync<ServerMembersResponse>("GET", $"servers/{id}/members", new()
            {
                ["exclude_offline"] = excludeOffline
            });

        public async Task<ServerMember> GetServerMemberAsync(string serverId, string userId)
            => await Client.SendAsync<ServerMember>("GET", $"servers/{serverId}/members/{userId}");

        public async Task<RestClientMessage> SendMessageAsync(string channel, SendMessageRequest message) =>
            await Client.SendAsync<RestClientMessage>("POST", $"channels/{channel}/messages", message);

        public async Task DeleteMessageAsync(string channel, string message) =>
            await Client.SendAsync("DELETE", $"channels/{channel}/messages/{message}");

        public async Task AcknowledgeMessageAsync(string channel, string message) =>
            await Client.SendAsync("PUT", $"channels/{channel}/ack/{message}");

        public async Task<RestMessage> GetMessageAsync(string channel, string message) =>
            await Client.SendAsync<RestMessage>("GET", $"channels/{channel}/{message}/{message}");
    }
}
