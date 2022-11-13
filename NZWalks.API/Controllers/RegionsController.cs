using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {

        public RegionsController(IRegionsRepository)
        {

        }
        [HttpGet]
       public IActionResult GetAllRegions()
        {

        }
    }
}
