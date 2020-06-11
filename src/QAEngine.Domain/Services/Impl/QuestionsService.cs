using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QAEngine.Domain.Exceptions;
using QAEngine.Domain.Persistence;
using QAEngine.Domain.Resources;

namespace QAEngine.Domain.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsService(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        public async Task<IEnumerable<QuestionResponse>> ListAsync()
        {
            return (await _questionsRepository.ListAsync()).Select(data => new QuestionResponse
            {
                Content = data.Content,
                CreateDate = data.CreateDate,
                ID = data.ID,
                IsClosed = data.IsClosed
            });
        }

        public async Task<QuestionResponse> GetByIdAsync(int id)
        {
            var data = await _questionsRepository.GetByIdAsync(id);

            if (data is null)
            {
                throw new NotFoundException($"Question [{id}] does not exist.");
            }

            return new QuestionResponse
            {
                Content = data.Content,
                CreateDate = data.CreateDate,
                ID = data.ID,
                IsClosed = data.IsClosed
            };
        }

        public async Task<QuestionResponse> CreateAsync(QuestionCreateRequest question)
        {
            var id = await _questionsRepository.CreateAsync(new Persistence.QuestionCreate
            {
                Content = question.Content,
                CreateDate = DateTimeOffset.Now
            });

            return await this.GetByIdAsync(id);
        }
    }
}
