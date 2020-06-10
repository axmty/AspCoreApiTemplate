using System.Data.SqlClient;

namespace QAEngine.Core.Data
{
    public interface ISqlConnectionFactory
    {
        SqlConnection Create();
    }
}
