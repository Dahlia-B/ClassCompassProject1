using Microsoft.AspNetCore.Mvc;

namespace ClassCompassApi_Simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult TestAttendance()
        {
            return Ok(new { 
                success = true, 
                message = "Attendance endpoint working!", 
                timestamp = DateTime.UtcNow,
                data = new { studentCount = 25, presentCount = 23, absentCount = 2 }
            });
        }

        [HttpGet]
        public IActionResult GetAttendance()
        {
            return Ok(new { 
                success = true, 
                attendance = new List<object>
                {
                    new { studentId = 1, studentName = "John Doe", status = "Present", date = DateTime.Today },
                    new { studentId = 2, studentName = "Jane Smith", status = "Absent", date = DateTime.Today }
                }
            });
        }
    }
}
