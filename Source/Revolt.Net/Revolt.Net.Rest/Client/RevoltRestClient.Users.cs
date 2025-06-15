using Revolt.Net.Core.JsonModels;
using Revolt.Net.Rest.Entities;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient
    {
        public async Task<RestUser> GetUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonUser>(HttpMethod.Get, $"users/{userId}", cancellationToken);

            return new RestUser(response);
        }
    }
}
