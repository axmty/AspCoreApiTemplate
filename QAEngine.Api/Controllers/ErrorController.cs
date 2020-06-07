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

            var detail = $"{exceptionHandlerPathFeature.Path}: {exceptionHandlerPathFeature.Error.Message}";

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = detail,
                Title = "An error occured.",
            };

            problemDetails.Extensions["stacktrace"] = exceptionHandlerPathFeature.Error.StackTrace;
            problemDetails.Extensions["innerexception"] = exceptionHandlerPathFeature.Error.InnerException;

            return new ObjectResult(problemDetails)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
