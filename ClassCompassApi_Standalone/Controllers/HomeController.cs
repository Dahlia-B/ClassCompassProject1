// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
namespace ClassCompassApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  // Changes route to /api/home
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ClassCompass API is running.");
        }
    }
}