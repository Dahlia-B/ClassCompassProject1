namespace ClassCompassWeb.Services
{
    public interface IScheduleService
    {
        Task<string> GetPlaceholderAsync();
    }

    public class ScheduleService : IScheduleService
    {
        private readonly IApiService _apiService;

        public ScheduleService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<string> GetPlaceholderAsync()
        {
            return await Task.FromResult("Schedule feature coming soon!");
        }
    }
}
