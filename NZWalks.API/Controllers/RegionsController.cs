using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionsRepository regionsRepository;

        public RegionsController(IRegionsRepository regionsRepository)
        {
            this.regionsRepository = regionsRepository;
        }
        [HttpGet]
       public IActionResult GetAllRegions()
        {
            //getting domain regions
            var allRegions= regionsRepository.GetAll();
            //returning DTO regions
            var regionsDTO = new List<Models.DTO.Region>();
            allRegions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population,
                };
                regionsDTO.Add(regionDTO);
            });
            return Ok(regionsDTO);
        }
    }
}
