using ClassCompass.Infrastructure.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ClassCompass.TestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create logger
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<TcpSocketClient>();

            // Create client
            var client = new TcpSocketClient(logger, "localhost", 8080);

            // Subscribe to events
            client.MessageReceived += (sender, message) =>
            {
                Console.WriteLine($"Received: [{message.Type}] {message.Content}");
            };

            client.Connected += (sender, e) =>
            {
                Console.WriteLine("Connected to server!");
            };

            client.Disconnected += (sender, e) =>
            {
                Console.WriteLine("Disconnected from server!");
            };

            try
            {
                // Connect to server
                Console.WriteLine("Connecting to socket server...");
                var connected = await client.ConnectAsync();

                if (connected)
                {
                    // Register as a student
                    await client.RegisterAsync("student_123");
                    
                    // Send a ping
                    await client.SendPingAsync();
                    
                    // Send a test notification
                    await client.SendNotificationAsync("teacher_456", "Hello from student!");
                    
                    // Keep running
                    Console.WriteLine("Press any key to disconnect...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Failed to connect to server");
                }
            }
            finally
            {
                await client.DisconnectAsync();
                client.Dispose();
            }
        }
    }
}
