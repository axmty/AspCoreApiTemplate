﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using QAEngine.Domain.Persistence;

namespace QAEngine.Infra.Persistence
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public QuestionsRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<Question>> ListAsync()
        {
            using var connection = _sqlConnectionFactory.Create();

            return await connection.QueryAsync<Question>(Queries.Get);
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            using var connection = _sqlConnectionFactory.Create();

            return await connection.QueryFirstOrDefaultAsync<Question>(Queries.GetById, new
            {
                QuestionID = id
            });
        }

        public async Task<int> CreateAsync(QuestionCreate question)
        {
            using var connection = _sqlConnectionFactory.Create();

            return await connection.ExecuteScalarAsync<int>(Queries.Create, question);
        }

        private static class Queries
        {
            public const string Get = @"
                SELECT 
                    [QuestionID] AS ID
                    ,[Content]
                    ,[CreateDate]
                    ,[IsClosed]
                FROM [dbo].[Question]";
            
            public const string GetById = @"
                SELECT 
                    [QuestionID] AS ID
                    ,[Content]
                    ,[CreateDate]
                    ,[IsClosed]
                FROM [dbo].[Question]
                WHERE [QuestionID] = @QuestionID";

            public const string Create = @"
                INSERT INTO [dbo].[Question] ([Content], [CreateDate], [IsClosed])
                VALUES (@Content, @CreateDate, 0);
                SELECT SCOPE_IDENTITY()";
        }
    }
}