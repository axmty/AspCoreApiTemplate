using Microsoft.AspNetCore.Builder;

namespace BookstoreApi.App.Middleware
{
    public static class NotFoundResponseHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseNotFoundHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotFoundResponseHandlerMiddleware>();
        }
    }
}
