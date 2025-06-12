using Microsoft.AspNetCore.Mvc;
using ClassCompassApi.Shared.Models;
using ClassCompassApi.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace ClassCompassApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassRoomController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassRoomController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassRoom>>> GetClassRooms()
        {
            try
            {
                return await _context.ClassRooms
                    .Include(c => c.Teacher)
                    .Include(c => c.School)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassRoom>> GetClassRoom(int id)
        {
            try
            {
                var classRoom = await _context.ClassRooms
                    .Include(c => c.Teacher)
                    .Include(c => c.School)
                    .FirstOrDefaultAsync(c => c.Id == id);
                    
                if (classRoom == null)
                {
                    return NotFound();
                }
                return classRoom;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

