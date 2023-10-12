using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace N5.User.Api.Middleware
{
    public class ChatWebSocketHandler
    {
        private readonly WebSocketConnectionManager _connectionManager;
        private readonly ILogger<ChatWebSocketHandler> _logger;

        public ChatWebSocketHandler(WebSocketConnectionManager connectionManager, ILogger<ChatWebSocketHandler> logger)
        {
            _connectionManager = connectionManager;
            _logger = logger;
        }

        public async Task HandleWebSocket(HttpContext context, WebSocket webSocket,string sessionId)
        {
            var socketId = _connectionManager.AddSocket(webSocket);

            _logger.LogInformation($"WebSocket connection established with ID {socketId}");

            while (webSocket.State == WebSocketState.Open)
            {
                var message = await ReceiveMessageAsync(webSocket);
                if (message != null)
                {
                    _logger.LogInformation($"Received message from ID {socketId}: {message}");
                    
                }
            }

            //_connectionManager.RemoveSocket(sessionId);
            _logger.LogInformation($"WebSocket connection closed with ID {socketId}");
        }

        private async Task<string?> ReceiveMessageAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.CloseStatus.HasValue)
            {
                return null;
            }

            return System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
        }

    }
}