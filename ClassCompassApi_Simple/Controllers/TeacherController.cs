using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClassCompassApi_Simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private static List<TeacherData> _teachers = new List<TeacherData>();
        private static int _nextId = 1;

        [HttpPost]
        [HttpPost("register")]
        public IActionResult RegisterTeacher([FromBody] TeacherRegistrationRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Name))
                {
                    return BadRequest(new { success = false, message = "Teacher name is required" });
                }

                var teacher = new TeacherData
                {
                    Id = _nextId,
                    TeacherId = _nextId++,
                    Name = request.Name,
                    Subject = request.Subject ?? "",
                    SchoolId = request.SchoolId ?? 1,
                    PasswordHash = request.PasswordHash ?? "default123",
                    IsActive = true
                };

                _teachers.Add(teacher);

                return Ok(new { 
                    success = true, 
                    message = "Teacher registered successfully!", 
                    teacherId = teacher.TeacherId,
                    data = teacher
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetTeachers()
        {
            return Ok(new { success = true, teachers = _teachers, count = _teachers.Count });
        }

        [HttpGet("{id}")]
        public IActionResult GetTeacher(int id)
        {
            var teacher = _teachers.FirstOrDefault(t => t.TeacherId == id);
            return teacher == null ? NotFound(new { success = false, message = "Teacher not found" }) 
                                  : Ok(new { success = true, teacher = teacher });
        }
    }

    public class TeacherRegistrationRequest
    {
        [Required]
        public string Name { get; set; } = "";
        public string? Subject { get; set; }
        public int? SchoolId { get; set; }
        public string? PasswordHash { get; set; }
    }

    public class TeacherData
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string Subject { get; set; } = "";
        public int SchoolId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
