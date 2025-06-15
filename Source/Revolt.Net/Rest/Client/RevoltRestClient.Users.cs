using Revolt.Net.Core.Entities.Channels;
using Revolt.Net.Core.Entities.Users;
using Revolt.Net.Core.JsonModels.Channels;
using Revolt.Net.Core.JsonModels.Users;
using Revolt.Net.Rest.Contracts.RestValues;

namespace Revolt.Net.Rest.Clients
{
    internal sealed partial class RevoltRestClient
    {
        public async Task<User> GetUserAsync(GetUserValues values, CancellationToken cancellationToken = default)
        {
            var response = await SendRequestAsync<JsonUser>(HttpMethod.Get, $"users/{values.UserId}", cancellationToken);

            return new User(response, this);
        }
    }
}
