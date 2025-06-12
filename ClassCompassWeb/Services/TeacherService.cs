using ClassCompassWeb.Models;
using System.Net.Http.Json;

namespace ClassCompassWeb.Services
{
    public class TeacherService
    {
        private readonly HttpClient _httpClient;
        private readonly List<Teacher> _mockTeachers;

        public TeacherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _mockTeachers = new List<Teacher>
            {
                new Teacher 
                { 
                    Id = 1, 
                    FirstName = "Sarah", 
                    LastName = "Wilson", 
                    Email = "sarah.wilson@school.edu", 
                    Subject = "Mathematics",
                    UserId = 4,
                    User = new User { Id = 4, Username = "sarah.wilson", Email = "sarah.wilson@school.edu", Role = "Teacher" }
                },
                new Teacher 
                { 
                    Id = 2, 
                    FirstName = "Mike", 
                    LastName = "Brown", 
                    Email = "mike.brown@school.edu", 
                    Subject = "Science",
                    UserId = 5,
                    User = new User { Id = 5, Username = "mike.brown", Email = "mike.brown@school.edu", Role = "Teacher" }
                },
                new Teacher 
                { 
                    Id = 3, 
                    FirstName = "Lisa", 
                    LastName = "Davis", 
                    Email = "lisa.davis@school.edu", 
                    Subject = "English",
                    UserId = 6,
                    User = new User { Id = 6, Username = "lisa.davis", Email = "lisa.davis@school.edu", Role = "Teacher" }
                }
            };
        }

        public async Task<List<Teacher>> GetTeachersAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Teacher>>("http://localhost:5004/api/teachers");
                return response ?? _mockTeachers;
            }
            catch
            {
                return _mockTeachers;
            }
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Teacher>($"http://localhost:5004/api/teachers/{id}");
                return response;
            }
            catch
            {
                return _mockTeachers.FirstOrDefault(t => t.Id == id);
            }
        }

        public async Task<Teacher> CreateTeacherAsync(Teacher teacher)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5004/api/teachers", teacher);
                return await response.Content.ReadFromJsonAsync<Teacher>() ?? teacher;
            }
            catch
            {
                teacher.Id = _mockTeachers.Count + 1;
                _mockTeachers.Add(teacher);
                return teacher;
            }
        }

        public async Task<Teacher> UpdateTeacherAsync(Teacher teacher)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"http://localhost:5004/api/teachers/{teacher.Id}", teacher);
                return await response.Content.ReadFromJsonAsync<Teacher>() ?? teacher;
            }
            catch
            {
                var existing = _mockTeachers.FirstOrDefault(t => t.Id == teacher.Id);
                if (existing != null)
                {
                    var index = _mockTeachers.IndexOf(existing);
                    _mockTeachers[index] = teacher;
                }
                return teacher;
            }
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"http://localhost:5004/api/teachers/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                var teacher = _mockTeachers.FirstOrDefault(t => t.Id == id);
                if (teacher != null)
                {
                    _mockTeachers.Remove(teacher);
                    return true;
                }
                return false;
            }
        }
    }
}
