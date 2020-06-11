using System.Data.SqlClient;

namespace QAEngine.Infra.Persistence
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connection;

        public SqlConnectionFactory(string connection)
        {
            _connection = connection;
        }

        public SqlConnection Create()
        {
            return new SqlConnection(_connection);
        }
    }
}
