using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QAEngine.Core.Data;
using QAEngine.Core.Repositories;
using QAEngine.Infra.Data;

namespace QAEngine.Infra.Repositories
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

        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }
    }
}
