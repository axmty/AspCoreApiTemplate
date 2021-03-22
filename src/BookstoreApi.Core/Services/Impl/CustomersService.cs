using System.Linq;
using System.Threading.Tasks;
using BookstoreApi.Core.Exceptions;
using BookstoreApi.Core.Mappers;
using BookstoreApi.Core.Models;
using BookstoreApi.Core.Repositories;

namespace BookstoreApi.Core.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository customersRepository;
        private readonly IMapper<Entities.Customer, Customer> customerMapper;

        public CustomersService(
            ICustomersRepository customersRepository,
            IMapper<Entities.Customer, Customer> customerMapper)
        {
            this.customersRepository = customersRepository;
            this.customerMapper = customerMapper;
        }

        public async Task<CollectionResponse<Customer>> GetAllAsync()
        {
            var (customers, count) = await this.customersRepository.GetAllAsync().ConfigureAwait(true);

            return new CollectionResponse<Customer>(customers.Select(this.customerMapper.Map), 1, count, 1);
        }

        public async Task<Customer> GetAsync(int id)
        {
            var customer = await this.customersRepository.GetAsync(id).ConfigureAwait(true);

            return customer == null
                ? throw new ResourceNotFoundException($"Customer with ID {id} does not exist.")
                : this.customerMapper.Map(customer);
        }
    }
}
