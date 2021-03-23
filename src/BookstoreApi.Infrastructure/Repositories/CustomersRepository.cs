using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreApi.Core.Entities;
using BookstoreApi.Core.Repositories;
using BookstoreApi.Infrastructure.Extensions;
using Dapper;

namespace BookstoreApi.Infrastructure.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public CustomersRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<(IEnumerable<Customer>, int)> GetAllAsync()
        {
            var sql = CustomersRepositoryQueries.GetAll;

            using var connection = this.dbConnectionFactory.Create();

            return await connection.QueryWithCount<Customer>(sql).ConfigureAwait(true);
        }

        public async Task<Customer> GetAsync(int id)
        {
            var sql = CustomersRepositoryQueries.GetById;

            using var connection = this.dbConnectionFactory.Create();

            var customer = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new
            {
                Id = id
            }).ConfigureAwait(true);

            return customer;
        }

        public async Task<(IEnumerable<Address>, int)> GetAddressesAsync(int customerId)
        {
            var sql = CustomersRepositoryQueries.GetAddresses;

            using var connection = this.dbConnectionFactory.Create();

            return await connection.QueryWithCount<Address>(sql, new
            {
                CustomerId = customerId
            }).ConfigureAwait(true);
        }
    }
}
