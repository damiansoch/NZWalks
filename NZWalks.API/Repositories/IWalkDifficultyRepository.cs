using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWaklDifficultiesAsync();
        Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);
    }
}
