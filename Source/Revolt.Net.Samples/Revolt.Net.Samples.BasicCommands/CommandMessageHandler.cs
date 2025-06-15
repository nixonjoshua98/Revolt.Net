using Microsoft.Extensions.Hosting;
using Revolt.Net.Commands;
using Revolt.Net.Commands.Abstractions;
using Revolt.Net.Commands.Extensions;
using Revolt.Net.WebSocket.Client;
using Revolt.Net.WebSocket.Events;

namespace Revolt.Net.Samples.BasicCommands
{
    internal sealed class CommandMessageHandler(
        IRevoltCommandProcessor _commandProcessor,
        IRevoltWebSocketClient _socketClient
    ) : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _socketClient.MessageReceived.Add(OnMessageReceived);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _socketClient.MessageReceived.Remove(OnMessageReceived);

            return Task.CompletedTask;
        }

        private async Task OnMessageReceived(MessageReceivedEvent e, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(e.Message.Content) && e.Message.HasStringPrefix("!", out var idx))
            {
                var context = new CommandContext(e.Message);

                var result = await _commandProcessor.ExecuteAsync(context, idx, cancellationToken);

                if (result.Error is not null)
                {
                    await e.Message.ReplyAsync(result.ErrorMessage, cancellationToken: cancellationToken);
                }
            }
        }
    }
}
