using System.Threading.Tasks;
using BookstoreApi.Core.Models;
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

        [HttpGet("{id}/addresses")]
        public async Task<ActionResult<Address>> GetAddressesAsync([FromRoute(Name = "id")] int customerId)
        {
            return this.Ok(await this.customersService.GetAddressesAsync(customerId).ConfigureAwait(true));
        }
    }
}
