using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.JsonModels.Servers;
using Revolt.Net.Rest.Contracts.RestValues;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient
    {
        public async Task<ServerMember> GetServerMemberAsync(GetServerMemberValues values, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonServerMember>(HttpMethod.Get, $"servers/{values.ServerId}/members/{values.UserId}", cancellationToken);

            return new ServerMember(response, this);
        }
    }
}