using Revolt.Net.Rest.Contracts;

namespace Revolt.Net.Rest.Clients
{
    internal partial class RevoltRestClient
    {
        public async Task<RevoltApiInformation> GetApiInformationAsync(CancellationToken cancellationToken)
        {
            return await SendRequestAsync<RevoltApiInformation>(HttpMethod.Get, string.Empty, cancellationToken);
        }
    }

}
