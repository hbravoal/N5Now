using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace N5.User.Api.Middleware
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> _sockets = new ConcurrentDictionary<Guid, WebSocket>();

        public WebSocket AddSocket(WebSocket socket)
        {
            var socketId = Guid.NewGuid();
            _sockets.TryAdd(socketId, socket);
            return socket;
        }

        public WebSocket? GetSocket(Guid socketId)
        {
            _sockets.TryGetValue(socketId, out var socket);
            return socket;
        }


        public Guid? GetSocketId(WebSocket socket)
        {
            foreach (var (key, value) in _sockets)
            {
                if (value == socket)
                {
                    return key;
                }
            }
            return null;
        }

        public void RemoveSocket(Guid socketId)
        {
            _sockets.TryRemove(socketId, out _);
        }
    }
}