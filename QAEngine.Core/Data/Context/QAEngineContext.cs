using Microsoft.EntityFrameworkCore;

namespace QAEngine.Core.Data
{
    public class QAEngineContext : DbContext
    {
        public QAEngineContext(DbContextOptions<QAEngineContext> options)
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
    }
}
