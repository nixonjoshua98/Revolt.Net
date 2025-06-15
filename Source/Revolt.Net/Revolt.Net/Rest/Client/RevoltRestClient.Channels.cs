using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.JsonModels.Channels;
using Revolt.Net.Rest.Contracts.RestValues;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient
    {
        public async Task<Channel> GetChannelAsync(GetChannelValues values, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonChannel>(HttpMethod.Get, $"channels/{values.ChannelId}", cancellationToken);

            return Channel.CreateNew(response, this);
        }

        public async Task<ChannelInvite> CreateChannelInviteAsync(CreateChannelInviteValues values, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonChannelInvite>(HttpMethod.Post, $"channels/{values.ChannelId}/invites", cancellationToken);

            return new ChannelInvite(response, this);
        }
    }
}
