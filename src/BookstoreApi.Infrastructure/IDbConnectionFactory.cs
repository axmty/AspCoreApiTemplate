using System.Data;

namespace BookstoreApi.Infrastructure
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
