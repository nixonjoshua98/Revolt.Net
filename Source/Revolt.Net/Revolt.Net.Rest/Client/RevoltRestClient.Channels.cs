using Revolt.Net.Core.JsonModels;
using Revolt.Net.Rest.Entities;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient
    {
        public async Task<RestChannel> GetChannelAsync(string channelId, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonChannel>(HttpMethod.Get, $"channels/{channelId}", cancellationToken);

            return RestChannel.CreateNew(response);
        }

        public async Task<RestChannelInvite> CreateChannelInviteAsync(string channelId, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonChannelInvite>(HttpMethod.Post, $"channels/{channelId}/invites", cancellationToken);

            return new RestChannelInvite(response);
        }
    }
}
