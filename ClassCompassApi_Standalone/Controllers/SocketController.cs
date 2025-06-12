using Microsoft.AspNetCore.Mvc;
using ClassCompass.Infrastructure.Sockets;

namespace ClassCompassApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocketController : ControllerBase
    {
        private readonly ISocketService _socketService;
        private readonly ILogger<SocketController> _logger;

        public SocketController(ISocketService socketService, ILogger<SocketController> logger)
        {
            _socketService = socketService;
            _logger = logger;
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
                    status = "running",
                    message = "Socket server is operational"
                });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Failed to get socket status");
                return StatusCode(500, new { success = false, error = "Failed to get socket status" });
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
    }

    public class BroadcastRequest
    {
        public string Message { get; set; } = string.Empty;
    }

    public class NotificationRequest
    {
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
