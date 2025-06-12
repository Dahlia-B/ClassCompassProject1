using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;

namespace ClassCompassWeb.Services.Socket
{
    public interface ISocketService
    {
        Task ConnectAsync();
        Task DisconnectAsync();
        Task SendNotificationAsync(string userId, string message);
        Task SendBroadcastAsync(string message);
        Task SendToClassAsync(string classId, string message);
        Task SendToTeacherAsync(string teacherId, string message);
        Task SendToStudentAsync(string studentId, string message);
        event EventHandler<string> NotificationReceived;
        event EventHandler<string> BroadcastReceived;
        bool IsConnected { get; }
    }

    public class SocketService : ISocketService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private HubConnection? _hubConnection;

        public event EventHandler<string>? NotificationReceived;
        public event EventHandler<string>? BroadcastReceived;

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public SocketService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5004";
        }

        public async Task ConnectAsync()
        {
            try
            {
                // For now, we'll use HTTP API calls to the socket endpoints
                // In the future, this could be enhanced with SignalR
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Socket connection error: {ex.Message}");
            }
        }

        public async Task DisconnectAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        public async Task SendNotificationAsync(string userId, string message)
        {
            try
            {
                var payload = new { userId = userId, message = message };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/socket/notify", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send notification error: {ex.Message}");
            }
        }

        public async Task SendBroadcastAsync(string message)
        {
            try
            {
                var payload = new { message = message };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/socket/broadcast", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send broadcast error: {ex.Message}");
            }
        }

        public async Task SendToClassAsync(string classId, string message)
        {
            try
            {
                var payload = new { message = message };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/socket/class/{classId}/notify", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send class notification error: {ex.Message}");
            }
        }

        public async Task SendToTeacherAsync(string teacherId, string message)
        {
            try
            {
                var payload = new { message = message };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/socket/teacher/{teacherId}/notify", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send teacher notification error: {ex.Message}");
            }
        }

        public async Task SendToStudentAsync(string studentId, string message)
        {
            try
            {
                var payload = new { message = message };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/api/socket/student/{studentId}/notify", content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send student notification error: {ex.Message}");
            }
        }
    }
}
