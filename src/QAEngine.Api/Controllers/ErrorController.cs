using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using QAEngine.Domain.Errors;
using QAEngine.Domain.Exceptions;

namespace QAEngine.Api.Controllers
{
    [Route("api/error")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ErrorController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;
            var path = exceptionHandlerPathFeature?.Path;

            var title = path is null ? "An error occured." : $"An error occured on path {path}.";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var type = ErrorCodes.Generic.InternalServerError;

            string detail = null;
            if (exception is DomainException domainException)
            {
                detail = domainException.Message;
                statusCode = domainException.StatusCode;
                type = domainException.ErrorCode;
            }
            else if (_webHostEnvironment.IsDevelopment())
            {
                detail = exception?.Message;
            }

            return this.Problem(detail: detail, statusCode: statusCode, title: title, type: type);
        }
    }
}
