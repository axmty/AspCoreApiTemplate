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

        public async Task<IEnumerable<Question>> GetAsync()
        {
            using var connection = _sqlConnectionFactory.Create();

            var questions = await connection.QueryAsync<Question>(Queries.Get);

            return questions;
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.Create();

            var question = await connection.QueryFirstOrDefaultAsync<Question>(
                Queries.GetById,
                new
                {
                    QuestionID = id
                });

            return question;
        }

        private static class Queries
        {
            public const string Get = @"SELECT * FROM Question";
            
            public const string GetById = @"SELECT * FROM Question WHERE QuestionID = @QuestionID";
        }
    }
}
