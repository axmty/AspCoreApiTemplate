using QAEngine.Core.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QAEngine.Core.Repositories
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetAsync();
    }
}
