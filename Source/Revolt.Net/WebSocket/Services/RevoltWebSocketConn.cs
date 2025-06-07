using Microsoft.Extensions.Logging;
using Revolt.Net.Rest.Json;
using Revolt.Net.WebSocket.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Revolt.Net.WebSocket.Services
{
    internal sealed class RevoltWebSocketConn(ILoggerFactory loggerFactory)
    {
        private readonly ILogger<RevoltWebSocketConn> _logger = loggerFactory.CreateLogger<RevoltWebSocketConn>();

        private readonly ClientWebSocket WebSocket = new();

        public async Task ConnectAsync(string websocketUri, CancellationToken cancellationToken)
        {
            var uri = new Uri(websocketUri);

            await WebSocket.ConnectAsync(uri, cancellationToken);

            _logger.LogDebug("WebSocket : Connected");
        }

        public async Task SendAsync<T>(T message, CancellationToken cancellationToken) where T : class
        {
            var json = JsonSerializer.Serialize(message, Serialization.Options);

            var bytes = Encoding.UTF8.GetBytes(json);

            await WebSocket.SendAsync(bytes, WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage, cancellationToken);
        }

        public async Task<ReceiveWebSocketMessage> ReceiveAsync(byte[] buffer, CancellationToken cancellationToken)
        {
            var result = await WebSocket.ReceiveAsync(buffer, cancellationToken);

            var content = Encoding.UTF8.GetString(buffer, 0, result.Count);

            var message = new ReceiveWebSocketMessage(result.MessageType, content);

            return message;
        }
    }
}
