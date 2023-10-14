using Confluent.Kafka;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;

namespace N5.User.Api.Middleware
{
    public sealed class SocketHandler
    {
        public const int BufferSize = 4096;
        private  WebSocket _socket;
        private  readonly WebSocketConnectionManager _WebSocketConnectionManager;


        private static SocketHandler _instance;
        public void Set(WebSocket socket)
        {
            this._socket = socket;
        }
        public SocketHandler()
        {
            _WebSocketConnectionManager = new();

        }

        public static SocketHandler GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SocketHandler();
            }
            return _instance;
        }

        public WebSocket AddSocket(WebSocket socket,Guid socketId)
        {
            _WebSocketConnectionManager.AddSocket(socket, socketId);
            return socket;
        }

        public async Task SendMessage(WebSocket socket, string message )
        {
            var encoded = Encoding.UTF8.GetBytes(message);

            await socket.SendAsync(encoded, WebSocketMessageType.Text, true, CancellationToken.None);
        }

 

        public WebSocket? GetSocketId(Guid socketId)
        {
         
            return _WebSocketConnectionManager.GetSocketId(socketId);
        }

        public void RemoveSocket(Guid socketId)
        {
            _WebSocketConnectionManager.RemoveSocket(socketId);
        }


        private static async Task Acceptor(HttpContext hc, Func<Task> n)
        {
            Guid sessionValue = Guid.NewGuid();
            var sessionId = hc.Request?.Query.FirstOrDefault(d => d.Key == "id").Value;
            string ses_ = sessionId.Value.ToString();
                  Guid.TryParse(ses_.ToUpper(),out var result);
                sessionValue = result;
            if (!hc.WebSockets.IsWebSocketRequest)
                return;

            var socket = await hc.WebSockets.AcceptWebSocketAsync();
            var h = SocketHandler.GetInstance();
            var newSocket= h.AddSocket(socket, sessionValue);

            
            if (newSocket is not null )
            {
                 var encoded = Encoding.UTF8.GetBytes("xd");

                //await newSocket.SendAsync(encoded, WebSocketMessageType.Text, true, CancellationToken.None);
                await newSocket.ReceiveAsync(encoded, CancellationToken.None);


            }
        }
        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(Acceptor);
        }
    }

}