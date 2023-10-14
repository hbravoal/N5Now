using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace N5.User.Api.Middleware
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> _sockets = new ConcurrentDictionary<Guid, WebSocket>();

        public WebSocket AddSocket(WebSocket socket,Guid socketId)
        {
            _sockets.TryAdd(socketId, socket);
            return socket;
        }

        public WebSocket? GetSocket(Guid socketId)
        {
            _sockets.TryGetValue(socketId, out var socket);
            return socket;
        }


        public WebSocket? GetSocketId(Guid socket)
        {
            foreach (var (key, value) in _sockets)
            {
                if (key== socket)
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
    }
}