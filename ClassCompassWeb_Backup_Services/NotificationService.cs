namespace ClassCompassWeb.Services
{
    public interface INotificationService
    {
        Task<string> GetPlaceholderAsync();
    }

    public class NotificationService : INotificationService
    {
        private readonly IApiService _apiService;
        private readonly ISocketService _socketService;

        public NotificationService(IApiService apiService, ISocketService socketService)
        {
            _apiService = apiService;
            _socketService = socketService;
        }

        public async Task<string> GetPlaceholderAsync()
        {
            return await Task.FromResult("Notifications feature coming soon!");
        }
    }
}
