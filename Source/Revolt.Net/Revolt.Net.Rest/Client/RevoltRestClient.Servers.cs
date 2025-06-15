using Revolt.Net.Core.JsonModels;
using Revolt.Net.Rest.Contracts;
using Revolt.Net.Rest.Entities;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient
    {
        public async Task<RestServerMember> GetServerMemberAsync(string serverId, string userId, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonServerMember>(HttpMethod.Get, $"servers/{serverId}/members/{userId}", cancellationToken);

            return new RestServerMember(response);
        }

        public async Task<IReadOnlyList<RestServerMemberUser>> GetServerMemberAsync(string serverId, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<GetServerMembersResponse>(HttpMethod.Get, $"servers/{serverId}/members", cancellationToken);

            var members = response.Members.ToDictionary(x => x.Id.UserId);

            return [.. response.Users.Select(u => new RestServerMemberUser(u, members[u.Id]))];
        }

        public async Task<RestServer> GetServerAsync(string serverId, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonServer>(HttpMethod.Get, $"servers/{serverId}", cancellationToken);

            return new RestServer(response);
        }
    }
}