using Microsoft.AspNetCore.Mvc;

namespace ClassCompassApi_Fresh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { 
                message = "Fresh ClassCompass API is working!", 
                timestamp = DateTime.UtcNow,
                status = "success",
                version = "1.0.0-fresh"
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { 
                status = "healthy", 
                service = "ClassCompass Fresh API",
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            });
        }

        [HttpGet("socket-ready")]
        public IActionResult SocketReady()
        {
            return Ok(new { 
                message = "Ready for socket integration!",
                timestamp = DateTime.UtcNow,
                nextStep = "Add socket services"
            });
        }
    }
}
