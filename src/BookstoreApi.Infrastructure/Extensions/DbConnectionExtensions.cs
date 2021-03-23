using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace BookstoreApi.Infrastructure.Extensions
{
    public static class DbConnectionExtensions
    {
        public static async Task<(IEnumerable<TEntity>, int)> QueryWithCount<TEntity>(this IDbConnection connection, string sql, object param = null)
        {
            var resultSets = await connection.QueryMultipleAsync(sql, param).ConfigureAwait(true);

            var count = await resultSets.ReadSingleAsync<int>().ConfigureAwait(true);
            var entities = await resultSets.ReadAsync<TEntity>().ConfigureAwait(true);
            
            return (entities, count);
        }
    }
}
