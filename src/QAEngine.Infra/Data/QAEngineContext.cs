using Microsoft.EntityFrameworkCore;
using QAEngine.Core.Data;

namespace QAEngine.Infra.Data
{
    public class QAEngineContext : DbContext
    {
        public QAEngineContext(DbContextOptions<QAEngineContext> options)
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Answer>().ToTable("Answer");
        }
    }
}
