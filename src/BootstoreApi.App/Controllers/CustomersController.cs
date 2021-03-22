using System.Threading.Tasks;
using BookstoreApi.Core.Entities;
using BookstoreApi.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.App.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetAllAsync()
        {
            return this.Ok(await this.customersService.GetAllAsync().ConfigureAwait(true));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetAsync([FromRoute(Name = "id")]int id)
        {
            return this.Ok(await this.customersService.GetAsync(id).ConfigureAwait(true));
        }
    }
}
