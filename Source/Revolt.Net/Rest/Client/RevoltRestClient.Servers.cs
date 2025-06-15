using Revolt.Net.Core.Entities.Servers;
using Revolt.Net.Core.JsonModels.Servers;
using Revolt.Net.Rest.Contracts.RestValues;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient
    {
        public async Task<Member> GetServerMemberAsync(GetServerMemberValues values, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonMember>(HttpMethod.Get, $"servers/{values.ServerId}/members/{values.UserId}", cancellationToken);

            return new Member(response, this);
        }
    }
}