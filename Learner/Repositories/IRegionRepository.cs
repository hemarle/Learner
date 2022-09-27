using Learner.Models.Domain;

namespace Learner.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();

        Task<Region> GetRegion(Guid ID);

        Task<Region> AddRegion(Region region);
        Task<Region> DeleteRegion(Guid ID);
        Task<Region> UpdateRegion(Guid ID, Region region);
    }
}
