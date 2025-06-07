using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Revolt.Net.Core.Json;
using Revolt.Net.Hosting.Configuration;
using Revolt.Net.Rest;
using Revolt.Net.WebSocket.Abstractions;
using Revolt.Net.WebSocket.Messages;
using Revolt.Net.WebSocket.Services;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Revolt.Net.WebSocket.Hosting.HostedServices
{
    internal sealed class RevoltWebSocketBackgroundService(
        IOptions<RevoltConfiguration> _configurationOptions,
        ILoggerFactory _loggerFactory,
        IWebSocketEventHub _eventHub,
        RevoltApiClient _apiClient
    ) : BackgroundService
    {
        private readonly RevoltWebSocketConn _connection = new(_loggerFactory);
        private readonly RevoltConfiguration _configuration = _configurationOptions.Value;
        private readonly ILogger<RevoltWebSocketBackgroundService> _logger = _loggerFactory.CreateLogger<RevoltWebSocketBackgroundService>();

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        };

        static RevoltWebSocketBackgroundService()
        {
            _serializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

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
                await Task.Delay(15_000, cancellationToken);

                await _connection.SendAsync(new PingWebSocketEvent(), cancellationToken);

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
                    var node = JsonNode.Parse(message.Content) ??
                        throw new Exception("Json node parsing failed");

                    var typedMessaged = JsonSerializer.Deserialize<WebSocketEvent>(node, _serializerOptions)
                        ?? throw new Exception("Message failed to be deserialized");

                    _logger.LogInformation("Revolt.Net.WebSocket : {MessageType}", node["type"]);

                    await _eventHub.InvokeAsync(typedMessaged, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Revolt.Net.WebSocket : Message deserialize");
                }
            }
        }
    }
}
