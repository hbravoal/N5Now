using Microsoft.AspNetCore.Mvc;
using N5.Eda.RequestReply.Interface;
using N5.Event.User;
using N5.TryCatch.Extend;
using N5.User.Domain.DTO;
using System.Net;

namespace N5.User.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : Controller
{
    private readonly IRequestReplyExecute _requestReply;

    public PermissionController(IRequestReplyExecute requestReply) => _requestReply = requestReply;

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


    #region Helpers

    private IActionResult HttpErrorHandler(Exception error)
        => StatusCode((int)HttpStatusCode.InternalServerError, error.Message);

    #endregion Helpers
}