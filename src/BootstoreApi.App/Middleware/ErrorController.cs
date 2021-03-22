using System;
using System.Net;
using BookstoreApi.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BookstoreApi.App.Middleware
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        public static readonly string DevelopmentEnvironmentName = "Development";

        [Route("/error-development")]
        public IActionResult ErrorDevelopment([FromServices] IHostEnvironment webHostEnvironment)
        {
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context == null || webHostEnvironment.EnvironmentName != DevelopmentEnvironmentName)
            {
                return this.NotFound();
            }

            var code = HttpStatusCode.InternalServerError;
            var detail = context.Error.StackTrace;
            var message = context.Error.Message;
            try
            {
                code = ExceptionStatusCodeConverter.Convert(context.Error.GetType());
            }
            catch (Exception e) when (e is InvalidOperationException or ArgumentException)
            {
                message = $"An exception was thrown while converting the exception to an HTTP status code: {e.Message}.";
                detail = e.StackTrace;
            }

            return this.Problem(detail: detail, title: message, statusCode: (int)code);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context == null)
            {
                return this.NotFound();
            }

            var code = HttpStatusCode.InternalServerError;
            var message = context.Error.GetType().IsAssignableTo(typeof(DomainException))
                ? context.Error.Message 
                : "An internal server error occured while processing the request.";
            try
            {
                code = ExceptionStatusCodeConverter.Convert(context.Error.GetType());
            }
            catch (Exception e) when (e is InvalidOperationException or ArgumentException)
            {
                // Keep the default code and message to hide the details in non-development environments.
            }

            return this.Problem(title: message, statusCode: (int)code);
        }
    }
}
