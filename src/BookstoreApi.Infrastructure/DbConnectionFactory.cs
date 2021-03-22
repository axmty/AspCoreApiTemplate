using System.Data;
using System.Data.SqlClient;

namespace BookstoreApi.Infrastructure
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionString;

        public DbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(this.connectionString);
        }
    }
}
