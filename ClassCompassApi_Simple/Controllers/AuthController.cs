using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClassCompassApi_Simple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static List<UserData> _users = new List<UserData>
        {
            new UserData { UserId = 1, Username = "admin", Role = "Admin", TeacherId = null, StudentId = null },
            new UserData { UserId = 2, Username = "teacher1", Role = "Teacher", TeacherId = 1, StudentId = null },
            new UserData { UserId = 3, Username = "student1", Role = "Student", TeacherId = null, StudentId = 1 }
        };
        private static int _nextId = 4;

        [HttpPost("login")]
        [HttpPost("Login")] // Alternative casing
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { success = false, message = "Login request is required" });
                }

                if (string.IsNullOrWhiteSpace(request.Username))
                {
                    return BadRequest(new { success = false, message = "Username is required" });
                }

                // For demo purposes, accept any password - find or create user
                var user = _users.FirstOrDefault(u => u.Username.ToLower() == request.Username.ToLower());
                
                if (user == null)
                {
                    // Create a new user if not found (for demo purposes)
                    user = new UserData
                    {
                        UserId = _nextId++,
                        Username = request.Username,
                        Role = "Student", // Default role
                        TeacherId = null,
                        StudentId = null
                    };
                    _users.Add(user);
                }

                // Simple token generation (in real app, use JWT)
                var token = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.Username}:{DateTime.UtcNow:yyyyMMddHHmmss}"));

                return Ok(new { 
                    success = true, 
                    message = "Login successful!", 
                    token = token,
                    user = new {
                        userId = user.UserId,
                        username = user.Username,
                        role = user.Role,
                        teacherId = user.TeacherId,
                        studentId = user.StudentId
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("login")] // Handle GET requests too (for testing)
        public IActionResult GetLogin()
        {
            return Ok(new { 
                message = "Please use POST method for login", 
                endpoint = "POST /api/Auth/login",
                example = new {
                    username = "admin",
                    password = "password"
                }
            });
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Username))
                {
                    return BadRequest(new { success = false, message = "Username is required" });
                }

                if (_users.Any(u => u.Username.ToLower() == request.Username.ToLower()))
                {
                    return BadRequest(new { success = false, message = "Username already exists" });
                }

                var user = new UserData
                {
                    UserId = _nextId++,
                    Username = request.Username,
                    Role = request.Role ?? "Student",
                    TeacherId = request.TeacherId,
                    StudentId = request.StudentId
                };

                _users.Add(user);

                return Ok(new { 
                    success = true, 
                    message = "User registered successfully!", 
                    userId = user.UserId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(new { success = true, users = _users, count = _users.Count });
        }

        // Test endpoint
        [HttpGet("test")]
        public IActionResult TestAuth()
        {
            return Ok(new { 
                message = "Auth controller is working!",
                endpoints = new[] {
                    "POST /api/Auth/login",
                    "POST /api/Auth/register",
                    "GET /api/Auth/users"
                },
                timestamp = DateTime.UtcNow
            });
        }
    }

    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class RegisterUserRequest
    {
        [Required]
        public string Username { get; set; } = "";
        public string Role { get; set; } = "Student";
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
    }

    public class UserData
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }
    }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string? Token { get; set; }
        public UserData? User { get; set; }
    }
}
