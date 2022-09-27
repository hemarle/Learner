using Learner.Models.Domain;

namespace Learner.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalks();

        Task<Walk> GetWalkById(Guid id);

        Task<Walk> CreateWalk(Walk walk);

        Task<Walk> UpdateWalk(Guid id, Walk walk);

        Task<Walk> DeleteWalk(Guid id);

    }
}
