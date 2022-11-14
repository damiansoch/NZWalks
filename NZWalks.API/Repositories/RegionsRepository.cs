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

        public async Task<Region> GetAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            
        }
        public async Task<Region> AddAsync(Region region)
        {
            region.Id = new Guid();
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x=>x.Id==id);
            if (region == null)
            {
                return null;
            }
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var regionForUpdate = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(regionForUpdate == null)
            {
                return null;
            }
            regionForUpdate.Code = region.Code;
            regionForUpdate.Name = region.Name;
            regionForUpdate.Area = region.Area;
            regionForUpdate.Lat = region.Lat;
            regionForUpdate.Long = region.Long;
            regionForUpdate.Population = region.Population;

            _context.SaveChanges();

            return regionForUpdate;
        }
    }
}
