namespace ClassCompassWeb.Services
{
    public interface IHomeworkService
    {
        Task<string> GetPlaceholderAsync();
    }

    public class HomeworkService : IHomeworkService
    {
        private readonly IApiService _apiService;

        public HomeworkService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<string> GetPlaceholderAsync()
        {
            return await Task.FromResult("Homework feature coming soon!");
        }
    }
}
