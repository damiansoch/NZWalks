using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task< IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            //validate incoming request
            //is user authenticated
            var user =  await userRepository.AuthenticateAsync(loginRequest.Username,loginRequest.Password);

            if (user != null)
            {
                //generateJWTToken
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("Username or password is incorrect");
        }
    }
}
