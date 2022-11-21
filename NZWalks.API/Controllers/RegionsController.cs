using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    

    public class RegionsController : Controller
    {
        private readonly IRegionsRepository regionsRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionsRepository regionsRepository, IMapper mapper)
        {
            this.regionsRepository = regionsRepository;
            this.mapper = mapper;
        }

        public IMapper Mapper { get; } = default!;

        #region All Regions
        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            //getting domain regions
            var allRegions = await regionsRepository.GetAllAsync();

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
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(allRegions);

            return Ok(regionsDTO);
        }
        #endregion

        #region Single Region
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionByIdAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRegionByIdAsync([FromRoute] Guid id)
        {
            var region = await regionsRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDto);
        }
        #endregion

        #region Adding Region
        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegionAsync([FromBody] AddRegionRequest addRegionRequest)
        {
            //validate the request
            if (!ValidateAddRegionRequest(addRegionRequest))
            {
                return BadRequest(ModelState);
            };


            //request(DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Area = addRegionRequest.Area,
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };
            //Pass details to Repository
            var responce = await regionsRepository.AddAsync(region);

            //Convert back to DTO

            var regionDTO = new Models.DTO.Region
            {
                Id = responce.Id,
                Area = responce.Area,
                Code = responce.Code,
                Name = responce.Name,
                Lat = responce.Lat,
                Long = responce.Long,
                Population = responce.Population,
            };

            return CreatedAtAction(nameof(GetRegionByIdAsync), new { id = regionDTO.Id }, responce);
        }
        #endregion

        #region Delete Region
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteRegionAsync([FromRoute] Guid id)
        {
            //get region from the database
            var response = await regionsRepository.DeleteAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            //convert response back to dto
            var regionDTO = mapper.Map<Models.DTO.Region>(response);

            //return ok response
            return Ok(regionDTO);
        }
        #endregion

        #region Update Region
        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody]UpdateRegionRequest updateRegionRequest)
        {
            //validations
            if (!ValidateUpdateRegionRequest(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }


            //convert DTO to domain model
            var region = new Models.Domain.Region()
            {
                Area = updateRegionRequest.Area,
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population,
            };

            //update region using repository
            var response = await regionsRepository.UpdateAsync(id, region);

            if(response == null)
            {
                return NotFound();
            }

            //convert domain back to dto

            var regionDTO = mapper.Map<Models.DTO.Region>(response);

            //return ok

            return Ok(regionDTO);
        }
        #endregion

        #region Private validation methods

        private bool ValidateAddRegionRequest(AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), $"Add Region data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),$"{nameof(addRegionRequest.Code)} can't be empty, null or have a whitespace");
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), $"{nameof(addRegionRequest.Name)} can't be empty, null or have a whitespace");
            }
            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} has to be greater than 0");
            }
            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{nameof(addRegionRequest.Population)} has to be greater or equal to 0");
            }

            //----
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateRegionRequest(UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest), $"Add Region data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code), $"{nameof(updateRegionRequest.Code)} can't be empty, null or have a whitespace");
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name), $"{nameof(updateRegionRequest.Name)} can't be empty, null or have a whitespace");
            }
            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area), $"{nameof(updateRegionRequest.Area)} has to be greater than 0");
            }
            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population), $"{nameof(updateRegionRequest.Population)} has to be greater or equal to 0");
            }

            //----
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
