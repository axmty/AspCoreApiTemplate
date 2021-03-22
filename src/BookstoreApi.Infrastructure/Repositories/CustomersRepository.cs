using System.Collections.Generic;
using System.Threading.Tasks;
using BookstoreApi.Core.Entities;
using BookstoreApi.Core.Repositories;
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
            SqlMapper.GridReader resultSets;

            using var connection = this.dbConnectionFactory.Create();

            resultSets = await connection.QueryMultipleAsync(sql).ConfigureAwait(true);

            var count = await resultSets.ReadSingleAsync<int>().ConfigureAwait(true);
            var customers = await resultSets.ReadAsync<Customer>().ConfigureAwait(true);

            return (customers, count);
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
    }
}
