using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.Messages;

namespace Revolt.Net.Commands.Hosting.HostedServices
{
    internal sealed class CommandHandlerBackgroundService(
        IWebSocketEventHub _eventHub,
        IServiceProvider _serviceProvider
    ) : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _eventHub.Message.Add(OnMessageReceivedAsync);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _eventHub.Message.Remove(OnMessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task OnMessageReceivedAsync(MessageWebSocketEvent e, CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var handler = scope.ServiceProvider.GetRequiredService<ICommandMessageHandler>();

            var context = new CommandContext(e.ToMessage());

            await handler.HandleAsync(context, cancellationToken);
        }
    }
}
