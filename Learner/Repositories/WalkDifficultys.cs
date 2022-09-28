using Learner.Data;
using Learner.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Learner.Repositories
{
    public class WalkDifficultys : IWalkDifficultys
    {
        private readonly LearnerDBContext learnerDBContext;

        public WalkDifficultys(LearnerDBContext learnerDBContext)
        {
            this.learnerDBContext = learnerDBContext;
        }
        public async Task <IEnumerable<WalkDifficulty>> GetAll()
        {
            var request = await learnerDBContext.WalkDifficulties.ToListAsync();
            return request;
        }

        public async Task<WalkDifficulty> GetByID(Guid id)
        {
            var request = await learnerDBContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            return request;
        }

        public async Task<WalkDifficulty> CreateNew(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
           await learnerDBContext.AddAsync(walkDifficulty);
            await learnerDBContext.SaveChangesAsync();

            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdatePost(Guid id, WalkDifficulty walkDifficulty)
        {
            var requestPost=await learnerDBContext.WalkDifficulties.FirstOrDefaultAsync(x=>x.Id==id);

            if (requestPost == null)
            {
                return null;
            }
            requestPost.Code = walkDifficulty.Code;
            await learnerDBContext.SaveChangesAsync();
            return requestPost;
        }

        public async Task<WalkDifficulty> DeletePost(Guid id)
        {
            var request = await learnerDBContext.WalkDifficulties.FirstOrDefaultAsync(x=>x.Id==id);
        if (request == null)
            {
                return null;
            }

         learnerDBContext.Remove(request);
            await learnerDBContext.SaveChangesAsync();
            return request;
        }
    }
}
