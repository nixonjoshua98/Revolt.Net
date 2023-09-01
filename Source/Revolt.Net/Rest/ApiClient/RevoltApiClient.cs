using Revolt.Net.Entities;
using Revolt.Net.Entities.Channels;
using Revolt.Net.Entities.Messages;
using Revolt.Net.Entities.Users;
using Revolt.Net.Rest.Requests;
using Revolt.Net.Rest.Responses;

namespace Revolt.Net.Rest.ApiClient
{
    internal sealed class RevoltApiClient
    {
        public readonly RevoltRestClient Client;

        public RevoltApiClient(RevoltRestClient client)
        {
            Client = client;
        }

        public async Task<RevoltApiInformation> GetApiInformationAsync()
            => await Client.SendAsync<RevoltApiInformation>("GET", string.Empty);

        public async Task<User> GetClientUserAsync()
            => await Client.SendAsync<User>("GET", "users/@me");

        public async Task<User> GetUserAsync(string userId)
            => await Client.SendAsync<User>("GET", $"users/{userId}");

        public async Task<Channel> GetChannelAsync(string channelId) =>
            await Client.SendAsync<Channel>("GET", $"channels/{channelId}");

        public async Task<ServerMembersResponse> GetServerMembersAsync(string id, bool excludeOffline)
            => await Client.SendAsync<ServerMembersResponse>("GET", $"servers/{id}/members", new()
            {
                ["exclude_offline"] = excludeOffline
            });

        public async Task<ServerMember> GetServerMemberAsync(string serverId, string userId)
            => await Client.SendAsync<ServerMember>("GET", $"servers/{serverId}/members/{userId}");

        public async Task<ClientMessage> SendMessageAsync(string channel, SendMessageRequest message) =>
            await Client.SendAsync<ClientMessage>("POST", $"channels/{channel}/messages", message);

        public async Task DeleteMessageAsync(string channel, string message) =>
            await Client.SendAsync("DELETE", $"channels/{channel}/messages/{message}");

        public async Task AcknowledgeMessageAsync(string channel, string message) =>
            await Client.SendAsync("PUT", $"channels/{channel}/ack/{message}");

        public async Task<Message> GetMessageAsync(string channel, string message) =>
            await Client.SendAsync < Message>("GET", $"channels/{channel}/{message}/{message}");
    }
}
