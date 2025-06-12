using Microsoft.AspNetCore.Mvc;
using ClassCompassApi.Shared.Models;
using ClassCompassApi.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace ClassCompassApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttendanceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance()
        {
            try
            {
                return await _context.Attendances.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("test")]
        public async Task<ActionResult<object>> GetAttendanceTest()
        {
            try
            {
                var count = await _context.Attendances.CountAsync();
                return Ok(new { Message = "Attendance endpoint working", Count = count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance)
        {
            try
            {
                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetAttendance", new { id = attendance.AttendanceId }, attendance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}


