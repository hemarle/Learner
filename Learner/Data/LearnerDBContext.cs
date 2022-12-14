using Learner.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learner.Data
{
    public class LearnerDBContext:DbContext
    {
        public LearnerDBContext(DbContextOptions <LearnerDBContext> options):base(options)
        {
                
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }

    }
}
