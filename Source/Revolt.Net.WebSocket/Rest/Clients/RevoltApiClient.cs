using Revolt.Net.Rest.Entities;
using Revolt.Net.WebSocket;

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

        public async Task<User> GetClientUserAsync()
            => await Client.SendAsync<User>("GET", "users/@me");

        public async Task<User> GetUserAsync(string userId)
            => await Client.SendAsync<User>("GET", $"users/{userId}");

        public async Task<SocketChannel> GetChannelAsync(string channelId) =>
            await Client.SendAsync<SocketChannel>("GET", $"channels/{channelId}");

        public async Task<ServerMembersResponse> GetServerMembersAsync(string id, bool excludeOffline)
            => await Client.SendAsync<ServerMembersResponse>("GET", $"servers/{id}/members", new()
            {
                ["exclude_offline"] = excludeOffline
            });

        public async Task<ServerMember> GetServerMemberAsync(string serverId, string userId)
            => await Client.SendAsync<ServerMember>("GET", $"servers/{serverId}/members/{userId}");

        public async Task<SocketClientMessage> SendMessageAsync(string channel, SendMessageRequest message) =>
            await Client.SendAsync<SocketClientMessage>("POST", $"channels/{channel}/messages", message);

        public async Task DeleteMessageAsync(string channel, string message) =>
            await Client.SendAsync("DELETE", $"channels/{channel}/messages/{message}");

        public async Task AcknowledgeMessageAsync(string channel, string message) =>
            await Client.SendAsync("PUT", $"channels/{channel}/ack/{message}");

        public async Task<SocketMessage> GetMessageAsync(string channel, string message) =>
            await Client.SendAsync<SocketMessage>("GET", $"channels/{channel}/{message}/{message}");
    }
}
