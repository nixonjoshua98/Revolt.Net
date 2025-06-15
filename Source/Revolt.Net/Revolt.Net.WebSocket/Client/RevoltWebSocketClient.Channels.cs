using Revolt.Net.Core.JsonModels;
using Revolt.Net.WebSocket.Entities;

namespace Revolt.Net.WebSocket.Client
{
    internal sealed partial class RevoltWebSocketClient
    {
        public async Task<Channel> GetChannelAsync(string channelId, CancellationToken cancellationToken = default)
        {
            var channel = await _restClient.GetChannelAsync(channelId, cancellationToken);

            return await CreateChannelAsync(channel.JsonModel, cancellationToken);
        }

        public async Task<ChannelInvite> CreateChannelInviteAsync(string channelId, CancellationToken cancellationToken)
        {
            var data = await _restClient.CreateChannelInviteAsync(channelId, cancellationToken);

            return new ChannelInvite(data.JsonModel, this);
        }

        public async Task<IReadOnlyList<ServerMemberUser>> GetServerMemberUsersAsync(string serverId, CancellationToken cancellationToken)
        {
            var data = await _restClient.GetServerMemberAsync(serverId, cancellationToken);

            return [.. data.Select(u => new ServerMemberUser(u.JsonUser, u.JsonMember, this))];
        }

        public Task<Channel> CreateChannelAsync(JsonChannel channel, CancellationToken cancellationToken)
        {
            return Task.FromResult(Channel.CreateNew(channel, this));
        }
    }
}
