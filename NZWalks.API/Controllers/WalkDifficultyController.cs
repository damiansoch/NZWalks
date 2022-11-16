using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository,IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        #region Get All
        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficutlies()
        {
            var walkDifficulties = await walkDifficultyRepository.GetAllWaklDifficultiesAsync();

            var walkDiffiecultiesDTO =  mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulties);
            return Ok(walkDiffiecultiesDTO);
        }
        #endregion

        #region Get WalkDifficulty
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyByIdAsync")]
        public async Task<IActionResult> GetWalkDifficultyByIdAsync([FromRoute] Guid id)
        {
            //get domain
            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyByIdAsync(id);
            if (walkDifficulty == null)
            {
                return NotFound();
            }
            //convert to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            //return
            return Ok(walkDifficultyDTO);
        }
        #endregion

        #region Add WalkDifficulty
        [HttpPost]
        
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody]Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //convert to domain
            var walkDificulty = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };
            //send to repository
            var response = await walkDifficultyRepository.AddWalkDifficultyAsync(walkDificulty);
            //convert back to dto
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(response);
            //return
            return CreatedAtAction(nameof(GetWalkDifficultyByIdAsync), new {id = response.Id}, response);
        }
        #endregion
    }
}
