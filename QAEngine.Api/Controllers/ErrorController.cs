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

            var detail = (exceptionHandlerPathFeature != null)
                ? $"{exceptionHandlerPathFeature.Path}: {exceptionHandlerPathFeature.Error.Message}"
                : null;

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = detail,
                Title = "An error occured.",
            };

            return new ObjectResult(problemDetails)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
