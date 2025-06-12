using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassCompassApi_Simple.Data;
using ClassCompassApi_Simple.Models;
using System.ComponentModel.DataAnnotations;

namespace ClassCompassApi_Simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ClassCompassDbContext _context;

        public SchoolController(ClassCompassDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterSchool([FromBody] SchoolRegistrationRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Name))
                {
                    return BadRequest(new { success = false, message = "School name is required" });
                }

                var school = new School
                {
                    Name = request.Name.Trim(),
                    NumberOfClasses = request.NumberOfClasses ?? 0,
                    Description = request.Description?.Trim(),
                    CreatedDate = DateTime.UtcNow
                };

                _context.Schools.Add(school);
                await _context.SaveChangesAsync();

                return Ok(new { 
                    success = true, 
                    message = "School registered successfully in MySQL database!", 
                    schoolId = school.SchoolId,
                    data = new {
                        schoolId = school.SchoolId,
                        name = school.Name,
                        numberOfClasses = school.NumberOfClasses,
                        description = school.Description,
                        createdDate = school.CreatedDate
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Database error", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            try
            {
                var schools = await _context.Schools
                    .Include(s => s.Teachers)
                    .Include(s => s.Students)
                    .Select(s => new {
                        schoolId = s.SchoolId,
                        name = s.Name,
                        numberOfClasses = s.NumberOfClasses,
                        description = s.Description,
                        createdDate = s.CreatedDate,
                        teacherCount = s.Teachers.Count,
                        studentCount = s.Students.Count
                    })
                    .ToListAsync();

                return Ok(new { success = true, schools = schools, count = schools.Count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Database error", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchool(int id)
        {
            try
            {
                var school = await _context.Schools
                    .Include(s => s.Teachers)
                    .Include(s => s.Students)
                    .FirstOrDefaultAsync(s => s.SchoolId == id);
                
                if (school == null)
                    return NotFound(new { success = false, message = "School not found" });

                return Ok(new { success = true, school = school });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Database error", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            try
            {
                var school = await _context.Schools.FindAsync(id);
                if (school == null) 
                    return NotFound(new { success = false, message = "School not found" });
                
                _context.Schools.Remove(school);
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "School deleted successfully from MySQL database" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Database error", error = ex.Message });
            }
        }
    }

    public class SchoolRegistrationRequest
    {
        [Required]
        public string Name { get; set; } = "";
        public int? NumberOfClasses { get; set; }
        public string? Description { get; set; }
    }
}
