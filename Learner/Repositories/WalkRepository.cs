using Learner.Data;
using Learner.Models.Domain;
using Learner.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Learner.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly LearnerDBContext learnerDBContext;
      

        public WalkRepository(LearnerDBContext learnerDBContext)
        {
            this.learnerDBContext = learnerDBContext;
          
        }

        public async Task<Walk> CreateWalk(Walk walk)
        {
            //Assign new id
            walk.Id = Guid.NewGuid();
            await learnerDBContext.Walks.AddAsync(walk);
            await learnerDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalk(Guid id)
        {
            var request = await learnerDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return null;
            }
            learnerDBContext.Remove(request);
            await learnerDBContext.SaveChangesAsync();
            return request;
        }

        public async Task<IEnumerable<Walk>> GetAllWalks()
        {
            var response = await learnerDBContext.Walks.ToListAsync();
            return response;
        }

        public async Task<Walk> GetWalkById(Guid id)
        {
            var response = await learnerDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
           
            return response;
        }

        public async Task<Walk> UpdateWalk(Guid id, Walk walk)
        {
            var response = await learnerDBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (response == null)
            {
                return null;
            }
            response.Name = walk.Name;
            response.Length=   walk.Length;
            response.WalkDifficultyID=walk.WalkDifficultyID;
            response.regionID = walk.regionID;

            await learnerDBContext.SaveChangesAsync();
            return response;
        }
    }
}
