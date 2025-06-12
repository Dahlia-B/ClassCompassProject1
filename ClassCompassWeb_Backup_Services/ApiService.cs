using System.Net.Http.Json;
using System.Text.Json;

namespace ClassCompassWeb.Services
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<T?> PostAsync<T>(string endpoint, object data);
        Task<T?> PutAsync<T>(string endpoint, object data);
        Task<bool> DeleteAsync(string endpoint);
    }

    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<T>(endpoint, _jsonOptions);
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, data, _jsonOptions);
                return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(endpoint, data, _jsonOptions);
                return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
            }
            catch
            {
                return default;
            }
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
