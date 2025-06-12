using Microsoft.AspNetCore.Mvc;
using ClassCompassApi.Shared.Models;
using ClassCompassApi.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace ClassCompassApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            try
            {
                return await _context.Teachers
                    .Include(t => t.School)
                    .Include(t => t.Students)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            try
            {
                var teacher = await _context.Teachers
                    .Include(t => t.School)
                    .Include(t => t.Students)
                    .FirstOrDefaultAsync(t => t.Id == id);
                    
                if (teacher == null)
                {
                    return NotFound();
                }
                return teacher;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            try
            {
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

