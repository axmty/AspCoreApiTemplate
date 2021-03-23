using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreApi.Core.Entities;

namespace BookstoreApi.Core.Repositories
{
    public interface ICustomersRepository
    {
        Task<(IEnumerable<Customer>, int)> GetAllAsync();

        Task<Customer> GetAsync(int id);

        Task<(IEnumerable<Address>, int)> GetAddressesAsync(int customerId);
    }
}
