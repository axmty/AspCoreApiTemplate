using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace QAEngine.Api.Controllers
{
    [Route("api/error")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var detail = exceptionHandlerPathFeature?.Error.Message;

            return this.Problem(
                detail: detail, 
                statusCode: (int)HttpStatusCode.InternalServerError, 
                title: "An error occured.");
        }
    }
}
