using System.Collections.Generic;
using System.Threading.Tasks;
using QAEngine.Core.Data;

namespace QAEngine.Core.Repositories
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetAsync();

        Task<Question> GetAsync(int id);
    }
}
