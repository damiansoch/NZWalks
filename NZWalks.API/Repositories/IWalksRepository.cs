using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {
         Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk>AddWalkAsync(Walk walk);
        Task<Walk>UpdateWalkAsync(Guid id,Walk walk);
        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
