using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClassCompassApi_Simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private static List<StudentData> _students = new List<StudentData>();
        private static int _nextId = 1;

        [HttpPost]
        [HttpPost("register")]
        public IActionResult RegisterStudent([FromBody] StudentRegistrationRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Name))
                {
                    return BadRequest(new { success = false, message = "Student name is required" });
                }

                var student = new StudentData
                {
                    Id = _nextId,
                    StudentId = _nextId++,
                    Name = request.Name,
                    ClassName = request.ClassName ?? "General",
                    TeacherId = request.TeacherId ?? 1,
                    ClassId = request.ClassId ?? 1,
                    PasswordHash = request.PasswordHash ?? "student123",
                    EnrollmentDate = DateTime.UtcNow,
                    IsActive = true,
                    NotificationsEnabled = true
                };

                _students.Add(student);

                return Ok(new { 
                    success = true, 
                    message = "Student registered successfully!", 
                    studentId = student.StudentId,
                    data = student
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _students.FirstOrDefault(s => s.StudentId == id);
            return student == null ? NotFound(new { success = false, message = "Student not found" }) 
                                  : Ok(new { success = true, student = student });
        }
    }

    public class StudentRegistrationRequest
    {
        [Required]
        public string Name { get; set; } = "";
        public string? ClassName { get; set; }
        public int? TeacherId { get; set; }
        public int? ClassId { get; set; }
        public string? PasswordHash { get; set; }
    }

    public class StudentData
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string ClassName { get; set; } = "";
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool NotificationsEnabled { get; set; } = true;
    }
}
