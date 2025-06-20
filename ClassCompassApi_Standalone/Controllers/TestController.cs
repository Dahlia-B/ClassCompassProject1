﻿using Microsoft.AspNetCore.Mvc;

namespace ClassCompassApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { 
                message = "ClassCompass API is working!", 
                timestamp = DateTime.UtcNow,
                status = "success"
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { 
                status = "healthy", 
                service = "ClassCompass API",
                version = "1.0.0"
            });
        }
    }
}
