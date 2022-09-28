using Learner.Models.Domain;

namespace Learner.Repositories
{
    public interface IWalkDifficultys
    {
        Task<IEnumerable<WalkDifficulty>> GetAll();
        Task<WalkDifficulty> GetByID(Guid id);
       Task<WalkDifficulty> CreateNew( WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdatePost(Guid id, WalkDifficulty walkDifficulty);
           Task <WalkDifficulty> DeletePost(Guid id); 
    }
}
