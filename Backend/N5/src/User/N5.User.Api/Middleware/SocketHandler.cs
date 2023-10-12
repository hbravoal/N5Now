using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace N5.User.Api.Middleware
{
    public class SocketHandler
    {
        public const int BufferSize = 4096;
        private readonly ConcurrentDictionary<Guid, WebSocket> _sockets = new ConcurrentDictionary<Guid, WebSocket>();
        private readonly WebSocket _socket;

        private SocketHandler(WebSocket socket)
        {
            _socket = socket;
        }

        public WebSocket AddSocket(WebSocket socket,Guid socketId)
        {
            _sockets.TryAdd(socketId, socket);
            return socket;
        }

        public async Task SendMessage(WebSocket socket, string message )
        {
            var encoded = Encoding.UTF8.GetBytes(message);

            await socket.SendAsync(encoded, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public WebSocket? GetSocket(Guid socketId)
        {
            _sockets.TryGetValue(socketId, out var socket);
            return socket;
        }


        public WebSocket? GetSocketId(Guid socketId)
        {
            foreach (var (key, value) in _sockets)
            {
                if (key == socketId)
                {
                    return value;
                }
            }
            return null;
        }

        public void RemoveSocket(Guid socketId)
        {
            _sockets.TryRemove(socketId, out _);
        }

        private async Task EchoLoop()
        {
            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);

            while (_socket.State == WebSocketState.Open)
            {
                var incoming = await _socket.ReceiveAsync(seg, CancellationToken.None);
                var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                await _socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private static async Task Acceptor(HttpContext hc, Func<Task> n)
        {
            Guid sessionValue = Guid.NewGuid();
            var sessionId = hc.Request?.Query.FirstOrDefault(d => d.Key == "id");
            if (sessionId is not null) {
                  Guid.TryParse(sessionId.Value.ToString(),out var result);
                sessionValue = result;
            }
            if (!hc.WebSockets.IsWebSocketRequest)
                return;

            var socket = await hc.WebSockets.AcceptWebSocketAsync();
            var h = new SocketHandler(socket);
            h.AddSocket(socket, sessionValue);

            var newSocket = h.GetSocketId(sessionValue);
            await h.SendMessage(newSocket, "text");
            await h.EchoLoop();
        }
        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(Acceptor);
        }
    }
}