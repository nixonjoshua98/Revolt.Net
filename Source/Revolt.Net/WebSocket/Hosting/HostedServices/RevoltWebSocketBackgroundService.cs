using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Revolt.Net.Core.Entities.Relationships;
using Revolt.Net.Core.Json;
using Revolt.Net.Hosting.Configuration;
using Revolt.Net.Json;
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
        IWebSocketEventHub _eventHub,
        ILoggerFactory _loggerFactory,
        RevoltApiClient _apiClient
    ) : BackgroundService
    {
        private readonly RevoltConfiguration _configuration = _configurationOptions.Value;
        private readonly ILogger<RevoltWebSocketBackgroundService> _logger = _loggerFactory.CreateLogger<RevoltWebSocketBackgroundService>();

        private readonly RevoltWebSocketConn Ws = new(_loggerFactory);

        static readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
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

            await Ws.ConnectAsync(serverInfo.WebSocketUrl, stoppingToken);

            await Ws.SendAsync(new AuthenticatePayload(_configuration.Token), stoppingToken);

            await Task.WhenAll([
                SendPingLoopAsync(stoppingToken),
                ReceiveIncomingMessagesAsync(stoppingToken)
            ]);
        }

        async Task SendPingLoopAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(15_000, cancellationToken);

                await Ws.SendAsync(new PingEvent(), cancellationToken);

                _logger.LogDebug("Revolt.Net.WebSocket : Ping");
            }
        }

        async Task ReceiveIncomingMessagesAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[4096];

            while (!cancellationToken.IsCancellationRequested)
            {
                var message = await Ws.ReceiveAsync(buffer, cancellationToken);

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
