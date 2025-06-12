using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClassCompass.Shared.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClassCompass.Shared.Models;

namespace ClassCompass.Shared.Services
{
    public class ApiService
    {
        private readonly HttpClient _client = new HttpClient();
        
        // LOCAL DEVELOPMENT: Use your PC's IP address with consistent port
        private readonly string _baseUrl = "http://192.168.68.83:5004";
        private readonly string _baseApiUrl = "http://192.168.68.83:5004";

        public async Task<List<Student>?> GetStudentsAsync()
        {
            var url = $"{_baseUrl}Student";
            return await _client.GetFromJsonAsync<List<Student>>(url);
        }

        // Test API connection with local health endpoint - FIXED PORT
        public async Task<string?> TestConnectionAsync()
        {
            try
            {
                var url = $"{_baseApiUrl}/health";
                return await _client.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        // Test attendance endpoint
        public async Task<object?> GetAttendanceTestAsync()
        {
            try
            {
                var url = $"{_baseUrl}Attendance/test";
                return await _client.GetFromJsonAsync<object>(url);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Additional method to test API info - FIXED PORT
        public async Task<string?> GetApiInfoAsync()
        {
            try
            {
                var url = $"{_baseUrl}info";
                return await _client.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}