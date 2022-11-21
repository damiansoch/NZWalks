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
    public class WalksController : Controller
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }
        #region Get All Walks
        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //domain walks
            var allWalks = await walksRepository.GetAllAsync();
            //dto walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(allWalks);
            //returning dto walks
            return Ok(walksDTO);
        }
        #endregion

        #region Get Single Walk
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetSingleWalkAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetSingleWalkAsync(Guid id)
        {
            //get domain
            var walk = await walksRepository.GetWalkByIdAsync(id);
            //if null
            if (walk == null)
            {
                return NotFound();
            }
            //map to dto
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            //return
            return Ok(walkDTO);
        }
        #endregion

        #region Add Walk
        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {



            //converting to domain
            var walk = new Models.Domain.Walk
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };
            //passing to repo
            var responce = await walksRepository.AddWalkAsync(walk);

            //changing back to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = responce.Id,
                Name = responce.Name,
                Length = responce.Length,
                RegionId = responce.RegionId,
                WalkDifficultyId = responce.WalkDifficultyId,
            };
            return CreatedAtAction(nameof(GetSingleWalkAsync), new { id = responce.Id }, responce);
        }
        #endregion

        #region Update Walk
        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            //convert to domain
            var walk = mapper.Map<Models.Domain.Walk>(updateWalkRequest);

            //send to repository
            var response = await walksRepository.UpdateWalkAsync(id, walk);
            //convert back to DTO
            if (response == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(response);
            //return
            return Ok(walkDTO);
        }
        #endregion

        #region Delete Walk
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkAsync([FromRoute]Guid id)
        {
            var response = await walksRepository.DeleteWalkAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            var responceDTO = mapper.Map<Models.DTO.Walk>(response);
            return Ok(responceDTO);
        }
        #endregion
    }
}
