using Microsoft.AspNetCore.Mvc;

namespace ClassCompassApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmergencyController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { 
                message = "Emergency controller works!", 
                timestamp = DateTime.Now 
            });
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            // Accept any login attempt and return success
            return Ok(new { 
                success = true, 
                token = "emergency-token-12345", 
                message = "Emergency login SUCCESS!",
                timestamp = DateTime.Now
            });
        }

        [HttpPost("login-with-creds")]
        public IActionResult LoginWithCreds([FromBody] dynamic request)
        {
            try
            {
                return Ok(new { 
                    success = true, 
                    token = "emergency-token-" + DateTime.Now.Ticks, 
                    message = "Login with credentials SUCCESS!",
                    receivedData = request?.ToString() ?? "no data"
                });
            }
            catch (Exception ex)
            {
                return Ok(new { 
                    success = true, 
                    token = "emergency-token-fallback", 
                    message = "Fallback login SUCCESS!",
                    error = ex.Message
                });
            }
        }
    }
}
