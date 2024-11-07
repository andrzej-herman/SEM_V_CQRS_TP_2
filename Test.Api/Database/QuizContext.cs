using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Test.Entity.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test.Api.Database
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }
        
        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Answer> Answers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Question>().HasMany(q => q.Answers);
        }
    }
}
