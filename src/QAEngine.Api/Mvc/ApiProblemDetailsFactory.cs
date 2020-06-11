using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using QAEngine.Domain.Errors;
using QAEngine.Domain.Exceptions;

namespace QAEngine.Api.Mvc
{
    public class ApiProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ApiProblemDetailsFactory(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext, int? _1, string _2, string _3, string _4, string _5)
        {
            var context = httpContext.Features.Get<IExceptionHandlerPathFeature>();
            var title = context?.Path is null ? "An error occured." : $"An error occured on path {context.Path}.";
            var status = 500;
            var type = ErrorCodes.Generic.InternalServerError;
            string detail = null;

            if (context?.Error is DomainException domainException)
            {
                status = domainException.StatusCode;
                type = domainException.ErrorCode;
                detail = domainException.Message;
                httpContext.Response.StatusCode = status;
            }
            else if (_webHostEnvironment.IsDevelopment())
            {
                detail = context?.Error.Message;
            }

            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Type = type,
                Detail = detail
            };

            this.AddTraceIdentifier(httpContext, problemDetails);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? _1, string _2, string _3, string _4, string _5)
        {
            if (modelStateDictionary == null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = 400,
                Type = ErrorCodes.Generic.ValidationFailed,
                Title = "One or more validation errors occured, see 'errors' for more details."
            };

            this.AddTraceIdentifier(httpContext, problemDetails);

            return problemDetails;
        }

        private void AddTraceIdentifier(HttpContext httpContext, ProblemDetails problemDetails)
        {
            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }
        }
    }
}
