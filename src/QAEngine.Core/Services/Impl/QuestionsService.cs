using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QAEngine.Core.Models;
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

        public async Task<IEnumerable<QuestionRead>> GetAsync()
        {
            return (await _questionsRepository.GetAsync()).Select(q => new QuestionRead
            {
                Content = q.Content,
                CreateDate = q.CreateDate,
                ID = q.ID
            });
        }
    }
}
