using AutoMapper;
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
        private readonly IMapper mapper;

        public RegionsController(IRegionsRepository regionsRepository,IMapper mapper)
        {
            this.regionsRepository = regionsRepository;
            this.mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpGet]
       public async Task<IActionResult> GetAllRegions()
        {
            //getting domain regions
            var allRegions= await regionsRepository.GetAllAsync();
            //returning DTO regions
            //----------------------------manual
            //var regionsDTO = new List<Models.DTO.Region>();
            //allRegions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            //---------------------------------

            //using automapper
            var regionsDTO =  mapper.Map<List<Models.DTO.Region>>(allRegions);

            return Ok(regionsDTO);
        }
    }
}
