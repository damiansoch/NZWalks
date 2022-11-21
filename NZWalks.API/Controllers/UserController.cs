using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userReository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userReository , IMapper mapper)
        {
            this.userReository = userReository;
            this.mapper = mapper;
        }
        #region Get All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userReository.GetAllUsers();
            var usersDTO = mapper.Map<List<Models.DTO.User>>(users);
            return Ok(usersDTO);
        }
        #endregion

        #region Single User
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute]Guid id)
        {
            var user = await userReository.GetUserByIDAsync(id);
            if(user == null)
            {
                return BadRequest("user doesn't exist");
            }
            var userDTO = mapper.Map<Models.DTO.User>(user);
            return Ok(userDTO);
        }
        #endregion
    }
}
