namespace ClassCompassWeb.Services
{
    public interface IClassService
    {
        Task<string> GetPlaceholderAsync();
    }

    public class ClassService : IClassService
    {
        private readonly IApiService _apiService;

        public ClassService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<string> GetPlaceholderAsync()
        {
            return await Task.FromResult("Class management feature coming soon!");
        }
    }
}
