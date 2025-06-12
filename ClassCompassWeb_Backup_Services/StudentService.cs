using ClassCompass.Shared.Models;

namespace ClassCompassWeb.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> CreateStudentAsync(Student student);
        Task<Student?> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
        Task<List<Student>> SearchStudentsAsync(string searchTerm);
    }

    public class StudentService : IStudentService
    {
        private readonly IApiService _apiService;

        public StudentService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var result = await _apiService.GetAsync<List<Student>>("api/students");
            return result ?? GetMockStudents();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _apiService.GetAsync<Student>($"api/students/{id}");
        }

        public async Task<Student?> CreateStudentAsync(Student student)
        {
            return await _apiService.PostAsync<Student>("api/students", student);
        }

        public async Task<Student?> UpdateStudentAsync(Student student)
        {
            return await _apiService.PutAsync<Student>($"api/students/{student.Id}", student);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            return await _apiService.DeleteAsync($"api/students/{id}");
        }

        public async Task<List<Student>> SearchStudentsAsync(string searchTerm)
        {
            var result = await _apiService.GetAsync<List<Student>>($"api/students/search?term={searchTerm}");
            return result ?? new List<Student>();
        }

        private List<Student> GetMockStudents()
        {
            return new List<Student>
            {
                new() { Id = 1, FirstName = "John", LastName = "Smith", Email = "john.smith@school.edu", StudentId = "STU001", Grade = 10 },
                new() { Id = 2, FirstName = "Emma", LastName = "Johnson", Email = "emma.johnson@school.edu", StudentId = "STU002", Grade = 11 },
                new() { Id = 3, FirstName = "Michael", LastName = "Brown", Email = "michael.brown@school.edu", StudentId = "STU003", Grade = 9 },
                new() { Id = 4, FirstName = "Sarah", LastName = "Davis", Email = "sarah.davis@school.edu", StudentId = "STU004", Grade = 12 },
                new() { Id = 5, FirstName = "David", LastName = "Wilson", Email = "david.wilson@school.edu", StudentId = "STU005", Grade = 10 }
            };
        }
    }
}
