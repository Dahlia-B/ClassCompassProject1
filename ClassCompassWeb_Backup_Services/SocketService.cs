using Microsoft.AspNetCore.SignalR.Client;

namespace ClassCompassWeb.Services
{
    public interface ISocketService
    {
        Task StartConnectionAsync();
        Task StopConnectionAsync();
        Task SendMessageAsync(string method, object data);
        void OnReceiveMessage(string method, Action<object> handler);
        bool IsConnected { get; }
        event Action<string>? ConnectionStateChanged;
    }

    public class SocketService : ISocketService, IAsyncDisposable
    {
        private readonly HubConnection _hubConnection;

        public SocketService(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
        }

        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;
        public event Action<string>? ConnectionStateChanged;

        public async Task StartConnectionAsync()
        {
            try
            {
                await _hubConnection.StartAsync();
                ConnectionStateChanged?.Invoke("Connected");
            }
            catch (Exception ex)
            {
                ConnectionStateChanged?.Invoke($"Connection failed: {ex.Message}");
            }
        }

        public async Task StopConnectionAsync()
        {
            try
            {
                await _hubConnection.StopAsync();
                ConnectionStateChanged?.Invoke("Disconnected");
            }
            catch
            {
                // Ignore errors on disconnect
            }
        }

        public async Task SendMessageAsync(string method, object data)
        {
            if (IsConnected)
            {
                await _hubConnection.SendAsync(method, data);
            }
        }

        public void OnReceiveMessage(string method, Action<object> handler)
        {
            _hubConnection.On<object>(method, handler);
        }

        public async ValueTask DisposeAsync()
        {
            await StopConnectionAsync();
            await _hubConnection.DisposeAsync();
        }
    }
}
