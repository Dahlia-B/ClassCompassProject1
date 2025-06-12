namespace ClassCompassWeb.Services
{
    public interface IGradeService
    {
        Task<string> GetPlaceholderAsync();
    }

    public class GradeService : IGradeService
    {
        private readonly IApiService _apiService;

        public GradeService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<string> GetPlaceholderAsync()
        {
            return await Task.FromResult("Grades feature coming soon!");
        }
    }
}
