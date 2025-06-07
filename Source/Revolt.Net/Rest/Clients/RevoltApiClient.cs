namespace Revolt.Net.Rest
{
    internal sealed class RevoltApiClient(HttpClient _httpClient) : RevoltApiClientBase(_httpClient)
    {
        public async Task LeaveOrDeleteServerAsync(string id) =>
            await SendAsync("DELETE", $"servers/{id}");

        public async Task RemoveMessageReactionsAsync(string channelId, string messageId) =>
            await SendAsync("DELETE", $"channels/{channelId}/messages/{messageId}/reactions");

        public async Task<RevoltApiInformation> GetApiInformationAsync(CancellationToken cancellationToken)
            => await SendAsync<RevoltApiInformation>("GET", string.Empty, cancellationToken);

        public async Task<RestUser> GetClientUserAsync()
            => await SendAsync<RestUser>("GET", "users/@me");

        public async Task<RestUser> GetUserAsync(string userId)
            => await SendAsync<RestUser>("GET", $"users/{userId}");

        public async Task<RestChannel> GetChannelAsync(string channelId) =>
            await SendAsync<RestChannel>("GET", $"channels/{channelId}");

        public async Task<ServerMembersResponse> GetServerMembersAsync(string id, bool excludeOffline)
            => await SendAsync<ServerMembersResponse>("GET", $"servers/{id}/members?exclude_offline={excludeOffline}");

        public async Task<ServerMember> GetServerMemberAsync(string serverId, string userId)
            => await SendAsync<ServerMember>("GET", $"servers/{serverId}/members/{userId}");

        public async Task<RestClientMessage> SendMessageAsync(string channel, SendMessageRequest message) =>
            await SendAsync<RestClientMessage>("POST", $"channels/{channel}/messages", message);

        public async Task DeleteMessageAsync(string channel, string message) =>
            await SendAsync("DELETE", $"channels/{channel}/messages/{message}");

        public async Task AcknowledgeMessageAsync(string channel, string message) =>
            await SendAsync("PUT", $"channels/{channel}/ack/{message}");

        public async Task<RestMessage> GetMessageAsync(string channel, string message) =>
            await SendAsync<RestMessage>("GET", $"channels/{channel}/{message}/{message}");
    }
}
