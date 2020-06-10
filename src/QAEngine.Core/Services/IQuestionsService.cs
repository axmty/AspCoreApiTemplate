using System.Collections.Generic;
using System.Threading.Tasks;
using QAEngine.Core.Models;

namespace QAEngine.Core.Services
{
    public interface IQuestionsService
    {
        Task<Question> CreateAsync(QuestionCreate question);

        Task<IEnumerable<Question>> GetAsync();

        Task<Question> GetByIdAsync(int id);
    }
}
