using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Revolt.Net.Core.Common;
using Revolt.Net.Core.Configuration;
using Revolt.Net.Rest.Clients;
using Revolt.Net.WebSocket.Client;
using Revolt.Net.WebSocket.JsonModels;
using Revolt.Net.WebSocket.Payloads;
using Revolt.Net.WebSocket.Services;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Revolt.Net.WebSocket.BackgroundServices
{
    internal sealed class WebSocketBackgroundService(
        IOptions<RevoltConfiguration> _configurationOptions,
        ILoggerFactory _loggerFactory,
        IRevoltWebSocketClient _socketClient,
        RevoltRestClient _apiClient
    ) : BackgroundService
    {
        private readonly RevoltWebSocketConnection _connection = new(_loggerFactory);
        private readonly RevoltConfiguration _configuration = _configurationOptions.Value;
        private readonly ILogger<WebSocketBackgroundService> _logger = _loggerFactory.CreateLogger<WebSocketBackgroundService>();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var serverInfo = await _apiClient.GetApiInformationAsync(stoppingToken);

            await _connection.ConnectAsync(serverInfo.WebSocketUrl, stoppingToken);

            await _connection.SendAsync(new AuthenticatePayload(_configuration.Token), stoppingToken);

            await Task.WhenAll([
                SendPingLoopAsync(stoppingToken),
                ReceiveIncomingMessagesAsync(stoppingToken)
            ]);
        }

        private async Task SendPingLoopAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(10_000, cancellationToken);

                await _connection.SendAsync(new PingPayload(), cancellationToken);

                _logger.LogDebug("Revolt.Net.WebSocket : Ping");
            }
        }

        private async Task ReceiveIncomingMessagesAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[4096];

            while (!cancellationToken.IsCancellationRequested)
            {
                var message = await _connection.ReceiveAsync(buffer, cancellationToken);

                try
                {
                    var node = JsonNode.Parse(message.Content)
                        ?? throw new Exception("Json node parsing failed");

                    var typedMessaged = node.Deserialize<JsonWebSocketMessage>(Serialization.DefaultOptions)
                        ?? throw new Exception("Message failed to be deserialized");

                    _logger.LogDebug("Revolt.Net.WebSocket : {MessageType}", node["type"]);

                    await _socketClient.InvokeAsync(typedMessaged, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Revolt.Net.WebSocket");
                }
            }
        }
    }
}