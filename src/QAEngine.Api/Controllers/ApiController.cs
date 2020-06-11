using Microsoft.AspNetCore.Mvc;

namespace QAEngine.Api.Controllers
{
    public class ApiController : ControllerBase
    {
        public override CreatedAtActionResult CreatedAtAction(string actionName, object routeValues)
        {
            return this.CreatedAtAction(
                actionName.EndsWith("Async") ? actionName[0..^5] : actionName,
                routeValues,
                null);
        }
    }
}
