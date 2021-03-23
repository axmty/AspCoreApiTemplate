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
        private readonly IMapper<Entities.Address, Address> addressMapper;

        public CustomersService(
            ICustomersRepository customersRepository,
            IMapper<Entities.Customer, Customer> customerMapper,
            IMapper<Entities.Address, Address> addressMapper)
        {
            this.customersRepository = customersRepository;
            this.customerMapper = customerMapper;
            this.addressMapper = addressMapper;
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

        public async Task<CollectionResponse<Address>> GetAddressesAsync(int customerId)
        {
            var (addresses, count) = await this.customersRepository.GetAddressesAsync(customerId).ConfigureAwait(true);

            return new CollectionResponse<Address>(addresses.Select(this.addressMapper.Map), 1, count, 1);
        }
    }
}
