using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using QAEngine.Core.Data;
using QAEngine.Core.Repositories;

namespace QAEngine.Infra.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public QuestionsRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<IEnumerable<Question>> GetAsync()
        {
            using var connection = _sqlConnectionFactory.Create();

            return connection.QueryAsync<Question>(Queries.Get);
        }

        public Task<Question> GetByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.Create();

            return connection.QueryFirstOrDefaultAsync<Question>(Queries.GetById, new
            {
                QuestionID = id
            });
        }

        public Task<int> CreateAsync(QuestionCreate question)
        {
            using var connection = _sqlConnectionFactory.Create();

            return connection.ExecuteScalarAsync<int>(Queries.Create, question);
        }

        private static class Queries
        {
            public const string Get = @"SELECT * FROM [Question]";
            
            public const string GetById = @"SELECT * FROM [Question] WHERE [QuestionID] = @QuestionID";

            public const string Create = @"
                INSERT INTO [dbo].[Question] ([Content], [CreateDate]) VALUES (@Content, @CreateDate);
                SELECT SCOPE_IDENTITY()";
        }
    }
}
