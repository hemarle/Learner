using Learner.Data;
using Learner.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learner.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly LearnerDBContext learnerDBContext;

        public RegionRepository(LearnerDBContext learnerDBContext)
        {
            this.learnerDBContext = learnerDBContext;
        }
        public async Task<IEnumerable<Region>> GetAll()
        {
            return await learnerDBContext.Regions.ToListAsync();
        }
    }
}
