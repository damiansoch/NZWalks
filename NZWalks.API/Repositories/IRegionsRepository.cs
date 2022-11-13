using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionsRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
