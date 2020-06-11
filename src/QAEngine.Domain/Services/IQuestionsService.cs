using System.Collections.Generic;
using System.Threading.Tasks;
using QAEngine.Domain.Resources;

namespace QAEngine.Domain.Services
{
    public interface IQuestionsService
    {
        Task<int> CreateAsync(QuestionCreateRequest question);

        Task<IEnumerable<QuestionResponse>> ListAsync();

        Task<QuestionResponse> GetByIdAsync(int id);
    }
}
