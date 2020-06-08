using QAEngine.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QAEngine.Core.Services
{
    public interface IQuestionsService
    {
        Task<IEnumerable<QuestionRead>> GetAsync();
    }
}
