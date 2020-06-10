using System;
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

            var questions = await connection.QueryAsync<Question>("SELECT * FROM Question");

            return questions;
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
