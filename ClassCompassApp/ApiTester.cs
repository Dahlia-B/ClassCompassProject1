using System;
using System.Threading.Tasks;

namespace ClassCompass
{
    public static class ApiTester
    {
        public static async Task<string> RunDiagnosticsAsync()
        {
            var apiService = new ApiService();
            var results = new System.Text.StringBuilder();
            
            results.AppendLine("=== ClassCompass API Diagnostics ===");
            results.AppendLine($"Base URL: {ApiConfig.BaseUrl}");
            results.AppendLine();
            
            // Test health endpoint
            try
            {
                bool healthOk = await apiService.TestConnectionAsync();
                results.AppendLine($"Health Check: {(healthOk ? "? PASS" : "? FAIL")}");
            }
            catch (Exception ex)
            {
                results.AppendLine($"Health Check: ? ERROR - {ex.Message}");
            }
            
            // Test students endpoint
            try
            {
                var students = await apiService.GetStudentsAsync();
                results.AppendLine($"Students API: ? PASS - {students.Count} students found");
            }
            catch (Exception ex)
            {
                results.AppendLine($"Students API: ? ERROR - {ex.Message}");
            }
            
            // Test teachers endpoint
            try
            {
                var teachers = await apiService.GetTeachersAsync();
                results.AppendLine($"Teachers API: ? PASS - {teachers.Count} teachers found");
            }
            catch (Exception ex)
            {
                results.AppendLine($"Teachers API: ? ERROR - {ex.Message}");
            }
            
            results.AppendLine();
            results.AppendLine("=== Troubleshooting Tips ===");
            results.AppendLine("If tests fail:");
            results.AppendLine("1. Check API server is running on PC");
            results.AppendLine("2. Verify same WiFi network");
            results.AppendLine("3. Test in mobile browser: " + ApiConfig.BaseUrl + "/health");
            results.AppendLine("4. Check Windows Firewall settings");
            
            return results.ToString();
        }
    }
}

