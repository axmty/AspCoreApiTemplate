using System.Collections.Generic;
using System.Threading.Tasks;

namespace QAEngine.Domain.Persistence
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> ListAsync();

        Task<Question> GetByIdAsync(int id);

        Task<int> CreateAsync(QuestionCreate question);
    }
}
