using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassCompass
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        
        public ApiService()
        {
            // Create handler that ignores SSL errors for development
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            
            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri(ApiConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }
        
        // Test API Connection
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiConfig.Endpoints.Health);
                var content = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode && content.Contains("Healthy");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"API Connection Error: {ex.Message}");
                return false;
            }
        }
        
        // Get Students
        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiConfig.Endpoints.Students);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<Student>>(json, options) ?? new List<Student>();
                }
                return new List<Student>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get Students Error: {ex.Message}");
                return new List<Student>();
            }
        }
        
        // Get Teachers
        public async Task<List<Teacher>> GetTeachersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiConfig.Endpoints.Teachers);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    return JsonSerializer.Deserialize<List<Teacher>>(json, options) ?? new List<Teacher>();
                }
                return new List<Teacher>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Get Teachers Error: {ex.Message}");
                return new List<Teacher>();
            }
        }
        
        // Register School
        public async Task<bool> RegisterSchoolAsync(SchoolRegistration registration)
        {
            try
            {
                var json = JsonSerializer.Serialize(registration);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                // Use absolute URL to fix "invalid request URL" error
                var response = await _httpClient.PostAsync(ApiConfig.Endpoints.Schools, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"School Registration Error: {ex.Message}");
                return false;
            }
        }
        
        // Login (placeholder)
        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var loginData = new { Username = username, Password = password };
                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(ApiConfig.Endpoints.Login, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login Error: {ex.Message}");
                return false;
            }
        }
    }
}

