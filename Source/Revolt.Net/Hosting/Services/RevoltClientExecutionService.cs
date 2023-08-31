using Microsoft.Extensions.Hosting;
using Revolt.Net.Clients;
using Revolt.Net.Core.Clients;

namespace Revolt.Net.Hosting.Services
{
    internal sealed class RevoltClientExecutionService : IHostedService
    {
        private readonly IRevolutClient _client;

        public RevoltClientExecutionService(IRevolutClient client)
        {
            _client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.LoginAsync(cancellationToken);

            await _client.RunAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.LogoutAsync(cancellationToken);
        }
    }
}
