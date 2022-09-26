using Learner.Models.Domain;

namespace Learner.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAll();
    }
}
