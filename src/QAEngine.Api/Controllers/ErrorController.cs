using Microsoft.AspNetCore.Authorization;
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
            return this.Problem();
        }
    }
}
