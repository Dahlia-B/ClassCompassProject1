namespace ClassCompassWeb.Services
{
    public interface IAttendanceService
    {
        Task<string> GetPlaceholderAsync();
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IApiService _apiService;

        public AttendanceService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<string> GetPlaceholderAsync()
        {
            return await Task.FromResult("Attendance feature coming soon!");
        }
    }
}
