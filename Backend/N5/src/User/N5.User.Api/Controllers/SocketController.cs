using Microsoft.AspNetCore.Mvc;
using N5.User.Api.Middleware;

namespace N5.User.Api.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatWebSocketHandler _webSocketHandler;

        public ChatController(ChatWebSocketHandler webSocketHandler)
        {
            _webSocketHandler = webSocketHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                
                await _webSocketHandler.HandleWebSocket(HttpContext, webSocket, "@@@");
            }
            else
            {
                return BadRequest("WebSocket is not supported.");
            }

            return Ok();
        }
    }
}
