using System.Threading.Tasks;
using BookstoreApi.Core.Models;

namespace BookstoreApi.Core.Services
{
    public interface ICustomersService
    {
        Task<CollectionResponse<Customer>> GetAllAsync();

        Task<Customer> GetAsync(int id);

        Task<CollectionResponse<Address>> GetAddressesAsync(int customerId);
    }
}
