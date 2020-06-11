using System.Data.SqlClient;

namespace QAEngine.Infra.Persistence
{
    public interface ISqlConnectionFactory
    {
        SqlConnection Create();
    }
}
