    using System.Diagnostics.Metrics;
    using Microsoft.AspNetCore.Mvc;
    using N5.Eda.Interfaces;
    using N5.Eda.RequestReply.Interface;
    using N5.Event.User;
    using N5.TryCatch.Extend;
    using N5.User.Api.Middleware;
    using N5.User.Domain.DTO;
    using System.Net;
    using System.Net.WebSockets;
    using System.Text;
    using OpenTelemetry.Trace;
    namespace N5.User.Api.Controllers;
    using System.Diagnostics;
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : Controller
    {
        private readonly IRequestReplyExecute _requestReply;
        //private readonly IBroker _broker;
        //private readonly SocketHandler _SocketHandler;
        //public PermissionController(IRequestReplyExecute requestReply, IBroker broker, SocketHandler osck) => (_requestReply,_broker, _SocketHandler) = (requestReply,broker, osck);
        

        public PermissionController(IRequestReplyExecute requestReply)
        {
            
            _requestReply = requestReply;
        }
            
        private void DoSomething()
        {
            // Simulando trabajo
            System.Threading.Thread.Sleep(100);
        }
        [HttpPost]
        [Route("JustCreate")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePermissionCompleteDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> JustCreate([FromBody] CreatePermissionDto request)
        {
            using (var activity = new Activity("ProcessRequest").Start())
            {
                Activity.Current = activity;
                // Aquí agregas lógica de procesamiento de la solicitud
                // Puedes agregar atributos a la traza
                activity.SetTag("request.id", Guid.NewGuid().ToString());

                activity.SetTag("request.user", "xD");
                activity.SetStartTime(DateTime.UtcNow);
                // Lógica de negocio
                DoSomething();
            }
          
            
            var h = SocketHandler.GetInstance();
            var newSocket = h.GetSocketId(Guid.Parse("30a031bb-349c-4421-a387-ee50b1a3bf44"));
            if (newSocket is not null)
            {
                if(newSocket.State == WebSocketState.Open)
                {
                    var encoded = Encoding.UTF8.GetBytes("x22d");
                    await newSocket.SendAsync(encoded, WebSocketMessageType.Text, true, CancellationToken.None);
                }
              
            }

            //await _broker.Publish(EventUser.CreatePermission, request);
            return Ok();
        }

        [HttpPost]
        [Route("Get")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetPermissionCompleteDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromBody] GetPermissionDto request)
             => await this.Try<IActionResult>(
                 () => ModelState.IsValid ?
                     Ok(_requestReply.Wait<GetPermissionCompleteDTO>(
                         request,
                         EventUser.GetPermission,
                         EventUser.GetPermissionComplete,
                         new TimeSpan(0, 0, 70)
                     ).Result) :
                     BadRequest())
                 .Catch(HttpErrorHandler)
                 .Apply();


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatePermissionCompleteDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDto request)
         => await this.Try<IActionResult>(
             () => ModelState.IsValid ?
                 Ok(_requestReply.Wait<CreatePermissionCompleteDTO>(
                     request,
                     EventUser.CreatePermission,
                     EventUser.CreatePermissionComplete,
                     new TimeSpan(0, 0, 70)
                 ).Result) :
                 BadRequest())
             .Catch(HttpErrorHandler)
             .Apply();


        [HttpPost]
        [Route("Modify")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModifyPermissionCompleteDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Modify([FromBody] ModifyPermissionDto request)
         => await this.Try<IActionResult>(
             () => ModelState.IsValid ?
                 Ok(_requestReply.Wait<ModifyPermissionCompleteDTO>(
                     request,
                     EventUser.ModifyPermission,
                     EventUser.ModifyPermissionComplete,
                     new TimeSpan(0, 0, 70)
                 ).Result) :
                 BadRequest())
             .Catch(HttpErrorHandler)
             .Apply();

        #region Helpers

        private IActionResult HttpErrorHandler(Exception error)
            => StatusCode((int)HttpStatusCode.InternalServerError, error.Message);

        #endregion Helpers
    }