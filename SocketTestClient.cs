using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocketTest
{
    public class SocketMessage
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public string TargetUserId { get; set; }
        public DateTime Timestamp { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("🧪 Testing Socket Connection to localhost:8080");
            Console.WriteLine("=" + new string('=', 45));
            
            try
            {
                using var client = new TcpClient();
                
                // Test connection
                Console.WriteLine("📡 Attempting to connect...");
                await client.ConnectAsync("localhost", 8080);
                Console.WriteLine("✅ Connected to socket server!");
                
                var stream = client.GetStream();
                
                // Test 1: Send ping
                Console.WriteLine("\n1. 🏓 Testing Ping...");
                var pingMessage = new SocketMessage
                {
                    Type = "ping",
                    Content = "ping",
                    Timestamp = DateTime.UtcNow
                };
                
                var pingJson = JsonSerializer.Serialize(pingMessage);
                var pingData = Encoding.UTF8.GetBytes(pingJson);
                await stream.WriteAsync(pingData, 0, pingData.Length);
                Console.WriteLine("   Sent: ping");
                
                // Read response
                var buffer = new byte[1024];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                var response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"   Received: {response}");
                
                // Test 2: Register user
                Console.WriteLine("\n2. 📝 Testing User Registration...");
                var registerMessage = new SocketMessage
                {
                    Type = "register",
                    Content = "test_user_123",
                    Timestamp = DateTime.UtcNow
                };
                
                var registerJson = JsonSerializer.Serialize(registerMessage);
                var registerData = Encoding.UTF8.GetBytes(registerJson);
                await stream.WriteAsync(registerData, 0, registerData.Length);
                Console.WriteLine("   Sent: register as test_user_123");
                
                // Read response
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"   Received: {response}");
                
                // Test 3: Send notification
                Console.WriteLine("\n3. 📢 Testing Notification...");
                var notificationMessage = new SocketMessage
                {
                    Type = "notification",
                    Content = "Test notification from PowerShell!",
                    TargetUserId = "test_user_456",
                    Timestamp = DateTime.UtcNow
                };
                
                var notificationJson = JsonSerializer.Serialize(notificationMessage);
                var notificationData = Encoding.UTF8.GetBytes(notificationJson);
                await stream.WriteAsync(notificationData, 0, notificationData.Length);
                Console.WriteLine("   Sent: notification");
                
                Console.WriteLine("\n✅ All socket tests completed successfully!");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Socket test failed: {ex.Message}");
                Console.WriteLine("\n💡 Make sure:");
                Console.WriteLine("   1. Your API server is running");
                Console.WriteLine("   2. Socket server is started (should be automatic)");
                Console.WriteLine("   3. Port 8080 is not blocked by firewall");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
