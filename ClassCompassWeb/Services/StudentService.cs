using ClassCompassWeb.Models;
using System.Net.Http.Json;

namespace ClassCompassWeb.Services
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;
        private readonly List<Student> _mockStudents;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _mockStudents = new List<Student>
            {
                new Student 
                { 
                    Id = 1, 
                    FirstName = "John", 
                    LastName = "Doe", 
                    Email = "john.doe@school.edu", 
                    Grade = "10th",
                    UserId = 1,
                    User = new User { Id = 1, Username = "john.doe", Email = "john.doe@school.edu", Role = "Student" }
                },
                new Student 
                { 
                    Id = 2, 
                    FirstName = "Jane", 
                    LastName = "Smith", 
                    Email = "jane.smith@school.edu", 
                    Grade = "11th",
                    UserId = 2,
                    User = new User { Id = 2, Username = "jane.smith", Email = "jane.smith@school.edu", Role = "Student" }
                },
                new Student 
                { 
                    Id = 3, 
                    FirstName = "Bob", 
                    LastName = "Johnson", 
                    Email = "bob.johnson@school.edu", 
                    Grade = "9th",
                    UserId = 3,
                    User = new User { Id = 3, Username = "bob.johnson", Email = "bob.johnson@school.edu", Role = "Student" }
                }
            };
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                // Try to get from API first, fallback to mock data
                var response = await _httpClient.GetFromJsonAsync<List<Student>>("http://localhost:5004/api/students");
                return response ?? _mockStudents;
            }
            catch
            {
                // If API is not available, return mock data
                return _mockStudents;
            }
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Student>($"http://localhost:5004/api/students/{id}");
                return response;
            }
            catch
            {
                return _mockStudents.FirstOrDefault(s => s.Id == id);
            }
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5004/api/students", student);
                return await response.Content.ReadFromJsonAsync<Student>() ?? student;
            }
            catch
            {
                // Add to mock data for demo
                student.Id = _mockStudents.Count + 1;
                _mockStudents.Add(student);
                return student;
            }
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"http://localhost:5004/api/students/{student.Id}", student);
                return await response.Content.ReadFromJsonAsync<Student>() ?? student;
            }
            catch
            {
                // Update mock data for demo
                var existing = _mockStudents.FirstOrDefault(s => s.Id == student.Id);
                if (existing != null)
                {
                    var index = _mockStudents.IndexOf(existing);
                    _mockStudents[index] = student;
                }
                return student;
            }
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"http://localhost:5004/api/students/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                // Remove from mock data for demo
                var student = _mockStudents.FirstOrDefault(s => s.Id == id);
                if (student != null)
                {
                    _mockStudents.Remove(student);
                    return true;
                }
                return false;
            }
        }
    }
}
