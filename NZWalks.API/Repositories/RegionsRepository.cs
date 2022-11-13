using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionsRepository : IRegionsRepository
    {
        private readonly AppDbContext _context;
        public RegionsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task< IEnumerable<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }
    }
}
