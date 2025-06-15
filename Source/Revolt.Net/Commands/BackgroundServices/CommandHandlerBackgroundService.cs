using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.Events.Messages;

namespace Revolt.Net.Commands.BackgroundServices
{
    internal sealed class CommandHandlerBackgroundService(
        IRevoltWebSocketClient _eventHub,
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

        private async Task OnMessageReceivedAsync(MessageEvent e, CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var handler = scope.ServiceProvider.GetRequiredService<ICommandMessageHandler>();

            var context = new CommandContext(e.Message);

            await handler.HandleAsync(context, cancellationToken);
        }
    }
}
