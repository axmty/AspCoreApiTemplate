using System.Linq;
using System.Threading.Tasks;
using BookstoreApi.Core.Exceptions;
using BookstoreApi.Core.Models;
using BookstoreApi.Core.Repositories;

namespace BookstoreApi.Core.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository customersRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
            this.customersRepository = customersRepository;
        }

        public async Task<CollectionResponse<Customer>> GetAllAsync()
        {
            var (customers, count) = await this.customersRepository.GetAllAsync().ConfigureAwait(true);

            return await Task.FromResult(new CollectionResponse<Customer>(customers.Select(Map), 1, count, 1)).ConfigureAwait(true);
        }

        public async Task<Customer> GetAsync(int id)
        {
            var customer = await this.customersRepository.GetAsync(id).ConfigureAwait(true);

            return customer == null
                ? throw new ResourceNotFoundException($"Customer with ID {id} does not exist.")
                : Map(customer);
        }

        private static Customer Map(Entities.Customer customer)
        {
            var result = new Customer
            {
                Id = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };

            return result;
        }
    }
}
