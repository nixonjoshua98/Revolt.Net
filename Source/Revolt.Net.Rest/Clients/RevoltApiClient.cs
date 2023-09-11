using Revolt.Net.Rest.Entities.Channels;

namespace Revolt.Net.Rest
{
    internal sealed class RevoltApiClient
    {
        public readonly RevoltHttpClient Client;

        public RevoltApiClient(RevoltHttpClient client)
        {
            Client = client;
        }

        public async Task<RevoltApiInformation> GetApiInformationAsync() => 
            await Client.SendAsync<RevoltApiInformation>("GET", string.Empty);

        #region Channels

        public async Task RemoveMessageReactionsAsync(string channelId, string messageId) =>
            await Client.SendAsync("DELETE", $"channels/{channelId}/messages/{messageId}/reactions");

        public async Task<RestMessagesResponse> GetMessagesAsync(
            string channelId,
            int limit,
            string beforeMessageId,
            string afterMessageId,
            MessageSort sort,
            string nearbyMessageId,
            bool includeUsers)
        {
            return await Client.SendAsync<RestMessagesResponse>("GET", $"channels/{channelId}/messages", new()
            {
                ["limit"] = limit,
                ["before"] = beforeMessageId,
                ["after"] = afterMessageId,
                ["sort"] = sort,
                ["nearby"] = nearbyMessageId,
                ["include_users"] = includeUsers
            });
        }

        public async Task<ChannelPayload> GetChannelAsync(string channelId) =>
            await Client.SendAsync<ChannelPayload>("GET", $"channels/{channelId}");

        public async Task<RestClientMessage> SendMessageAsync(string channel, SendMessageRequest message) =>
            await Client.SendAsync<RestClientMessage>("POST", $"channels/{channel}/messages", message);

        public async Task DeleteMessageAsync(string channel, string message) =>
            await Client.SendAsync("DELETE", $"channels/{channel}/messages/{message}");

        public async Task AcknowledgeMessageAsync(string channel, string message) =>
            await Client.SendAsync("PUT", $"channels/{channel}/ack/{message}");

        public async Task<RestMessage> GetMessageAsync(string channel, string message) =>
            await Client.SendAsync<RestMessage>("GET", $"channels/{channel}/{message}/{message}");

        #endregion


        #region Users

        public async Task<RestUser> GetClientUserAsync() => 
            await Client.SendAsync<RestUser>("GET", "users/@me");

        public async Task<RestUser> GetUserAsync(string userId) => 
            await Client.SendAsync<RestUser>("GET", $"users/{userId}");

        #endregion

        #region Servers

        public async Task LeaveOrDeleteServerAsync(string id) =>
            await Client.SendAsync("DELETE", $"servers/{id}");

        public async Task<ServerMembersResponse> GetServerMembersAsync(string id, bool excludeOffline) => 
            await Client.SendAsync<ServerMembersResponse>("GET", $"servers/{id}/members", new()
            {
                ["exclude_offline"] = excludeOffline
            });

        public async Task<ServerMemberUser> GetServerMemberAsync(string serverId, string userId) => 
            await Client.SendAsync<ServerMemberUser>("GET", $"servers/{serverId}/members/{userId}");

        #endregion

    }
}
