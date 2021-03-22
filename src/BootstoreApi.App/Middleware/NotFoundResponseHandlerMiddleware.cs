using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookstoreApi.App.Middleware
{
    public class NotFoundResponseHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public NotFoundResponseHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ProblemDetailsFactory problemDetailsFactory)
        {
            await this.next(context).ConfigureAwait(true);

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound || context.Response.ContentLength is null or 0)
            {
                var notFoundProblemDetails = problemDetailsFactory.CreateProblemDetails(context, (int)HttpStatusCode.NotFound);
                await context.Response.WriteAsJsonAsync(notFoundProblemDetails).ConfigureAwait(true);
            }
        }
    }
}
