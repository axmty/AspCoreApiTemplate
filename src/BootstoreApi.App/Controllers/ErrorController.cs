using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BookstoreApi.App.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error-development")]
        public IActionResult ErrorDevelopment([FromServices] IHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                return this.NotFound();
            }

            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            return this.Problem(
                detail: context.Error.StackTrace,
                title: "Message: " + context.Error.Message);
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            return this.Problem(title: "Message: " + context.Error.Message);
        }
    }
}
