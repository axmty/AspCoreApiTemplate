using System.Collections.Generic;
using System.Threading.Tasks;
using QAEngine.Core.Models;

namespace QAEngine.Core.Services
{
    public interface IQuestionsService
    {
        Task<IEnumerable<Question>> GetAsync();

        Task<Question> GetByIdAsync(int id);
    }
}
