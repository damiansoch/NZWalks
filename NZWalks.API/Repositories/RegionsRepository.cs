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
        public IEnumerable<Region> GetAll()
        {
            return _context.Regions.ToList();
        }
    }
}
