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

        public async Task<Region> AddRegion(Region region)
        {
            region.ID = Guid.NewGuid();
            await learnerDBContext.Regions.AddAsync(region);
           await  learnerDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegion(Guid ID)
        {
           var region= await learnerDBContext.Regions.FirstOrDefaultAsync(r => r.ID == ID);
            if (region == null){
                return null;
            }
            learnerDBContext.Regions.Remove(region);
            learnerDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await learnerDBContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegion(Guid ID)
        {
           return await learnerDBContext.Regions.FirstOrDefaultAsync(r => r.ID == ID);
            
        }

        public async Task<Region> UpdateRegion(Guid id, Region region)
        {
            //get the region to be updated
            var exisitingRegion = await learnerDBContext.Regions.FirstOrDefaultAsync(x => x.ID == id);
            if (region == null)
            {
                return null;
            };
            //Update the props
            exisitingRegion.Area=region.Area;
            exisitingRegion.Code=region.Code;
            exisitingRegion.Lat = region.Lat;
            exisitingRegion.Long = region.Long;
            exisitingRegion.Population = region.Population;

            //Save the changes
            await learnerDBContext.SaveChangesAsync();
            return exisitingRegion;
        }
    }
}
