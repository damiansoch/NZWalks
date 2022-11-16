using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly AppDbContext context;

        public WalksRepository(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await 
                context.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            return await
                context.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walkFromDatabase = await context.Walks.FindAsync(id);
            if(walkFromDatabase == null)
            {
                return null;
            }
            walkFromDatabase.Name = walk.Name;
            walkFromDatabase.Length = walk.Length;
            walkFromDatabase.RegionId = walk.RegionId;
            walkFromDatabase.WalkDifficultyId = walk.WalkDifficultyId;

            await context.SaveChangesAsync();
            return walkFromDatabase;

        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await context.Walks.FindAsync(id);
            if (walk == null)
            {
                return null;
            }
            context.Walks.Remove(walk);
            await context.SaveChangesAsync();
            return walk;
        }
    }
}
