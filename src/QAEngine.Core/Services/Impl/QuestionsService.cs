using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QAEngine.Core.Exceptions;
using QAEngine.Core.Repositories;

namespace QAEngine.Core.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsService(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public async Task<IEnumerable<Models.Question>> GetAsync()
        {
            return (await _questionsRepository.GetAsync()).Select(q => new Models.Question
            {
                Content = q.Content,
                CreateDate = q.CreateDate,
                ID = q.ID
            });
        }

        public async Task<Models.Question> GetByIdAsync(int id)
        {
            var data = await _questionsRepository.GetByIdAsync(id);

            if (data is null)
            {
                throw new NotFoundException($"Question [{id}] does not exist.");
            }

            return new Models.Question
            {
                Content = data.Content,
                CreateDate = data.CreateDate,
                ID = data.ID
            };
        }

        public async Task<Models.Question> CreateAsync(Models.QuestionCreate question)
        {
            var id = await _questionsRepository.CreateAsync(new Data.QuestionCreate
            {
                Content = question.Content,
                CreateDate = DateTimeOffset.Now
            });

            return await this.GetByIdAsync(id);
        }
    }
}
