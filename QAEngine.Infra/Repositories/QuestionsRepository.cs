using Microsoft.EntityFrameworkCore;
using QAEngine.Core.Data;
using QAEngine.Infra.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QAEngine.Core.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly QAEngineContext _context;

        public QuestionsRepository(QAEngineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetAsync()
        {
            return await _context.Questions.ToListAsync();
        }
    }
}
