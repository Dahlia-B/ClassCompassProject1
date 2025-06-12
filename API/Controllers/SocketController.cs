using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ClassCompass.Infrastructure.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ClassCompass.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SocketController : ControllerBase
    {
        private readonly ISocketService _socketService;
        private readonly ILogger<SocketController> _logger;

        public SocketController(ISocketService socketService, ILogger<SocketController> logger)
        {
            _socketService = socketService;
            _logger = logger;
        }

        [HttpPost("notify")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            try
            {
                await _socketService.SendNotificationAsync(request.UserId, request.Message);
                return Ok(new { success = true, message = "Notification sent successfully" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Failed to send notification");
                return StatusCode(500, new { success = false, error = "Failed to send notification" });
            }
        }

        [HttpPost("broadcast")]
        public async Task<IActionResult> BroadcastMessage([FromBody] BroadcastRequest request)
        {
            try
            {
                await _socketService.BroadcastMessageAsync(request.Message);
                return Ok(new { success = true, message = "Message broadcasted successfully" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Failed to broadcast message");
                return StatusCode(500, new { success = false, error = "Failed to broadcast message" });
            }
        }

        [HttpPost("class/{classId}/notify")]
        public async Task<IActionResult> NotifyClass(string classId, [FromBody] ClassNotificationRequest request)
        {
            try
            {
                await _socketService.SendToClassAsync(classId, request.Message);
                return Ok(new { success = true, message = $"Notification sent to class {classId}" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Failed to notify class {classId}");
                return StatusCode(500, new { success = false, error = "Failed to notify class" });
            }
        }

        [HttpPost("teacher/{teacherId}/notify")]
        public async Task<IActionResult> NotifyTeacher(string teacherId, [FromBody] TeacherNotificationRequest request)
        {
            try
            {
                await _socketService.SendToTeacherAsync(teacherId, request.Message);
                return Ok(new { success = true, message = $"Notification sent to teacher {teacherId}" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Failed to notify teacher {teacherId}");
                return StatusCode(500, new { success = false, error = "Failed to notify teacher" });
            }
        }

        [HttpPost("student/{studentId}/notify")]
        public async Task<IActionResult> NotifyStudent(string studentId, [FromBody] StudentNotificationRequest request)
        {
            try
            {
                await _socketService.SendToStudentAsync(studentId, request.Message);
                return Ok(new { success = true, message = $"Notification sent to student {studentId}" });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Failed to notify student {studentId}");
                return StatusCode(500, new { success = false, error = "Failed to notify student" });
            }
        }

        [HttpGet("status")]
        public IActionResult GetSocketStatus()
        {
            try
            {
                var connectedClients = _socketService.GetConnectedClientsCount();
                return Ok(new 
                { 
                    success = true, 
                    connectedClients = connectedClients,
                    status = "running"
                });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Failed to get socket status");
                return StatusCode(500, new { success = false, error = "Failed to get socket status" });
            }
        }
    }

    // Request DTOs
    public class NotificationRequest
    {
        public string UserId { get; set; }
        public string Message { get; set; }
    }

    public class BroadcastRequest
    {
        public string Message { get; set; }
    }

    public class ClassNotificationRequest
    {
        public string Message { get; set; }
    }

    public class TeacherNotificationRequest
    {
        public string Message { get; set; }
    }

    public class StudentNotificationRequest
    {
        public string Message { get; set; }
    }
}
