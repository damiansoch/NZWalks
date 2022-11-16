using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly AppDbContext context;

        public WalkDifficultyRepository(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<WalkDifficulty>> GetAllWaklDifficultiesAsync()
        {
            return await context.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id)
        {
            return await context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await context.WalkDifficulty.AddAsync(walkDifficulty);
            await context.SaveChangesAsync();
            return walkDifficulty;
        }
    }
}
