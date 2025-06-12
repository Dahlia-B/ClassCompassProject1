using ClassCompass.Shared.Models;

namespace ClassCompassWeb.Services
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachersAsync();
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<Teacher?> CreateTeacherAsync(Teacher teacher);
        Task<Teacher?> UpdateTeacherAsync(Teacher teacher);
        Task<bool> DeleteTeacherAsync(int id);
        Task<List<Teacher>> SearchTeachersAsync(string searchTerm);
        Task<List<ClassRoom>> GetTeacherClassesAsync(int teacherId);
    }

    public class TeacherService : ITeacherService
    {
        private readonly IApiService _apiService;

        public TeacherService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            var result = await _apiService.GetAsync<List<Teacher>>("api/teachers");
            return result ?? GetMockTeachers();
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            return await _apiService.GetAsync<Teacher>($"api/teachers/{id}");
        }

        public async Task<Teacher?> CreateTeacherAsync(Teacher teacher)
        {
            return await _apiService.PostAsync<Teacher>("api/teachers", teacher);
        }

        public async Task<Teacher?> UpdateTeacherAsync(Teacher teacher)
        {
            return await _apiService.PutAsync<Teacher>($"api/teachers/{teacher.Id}", teacher);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            return await _apiService.DeleteAsync($"api/teachers/{id}");
        }

        public async Task<List<Teacher>> SearchTeachersAsync(string searchTerm)
        {
            var result = await _apiService.GetAsync<List<Teacher>>($"api/teachers/search?term={searchTerm}");
            return result ?? new List<Teacher>();
        }

        public async Task<List<ClassRoom>> GetTeacherClassesAsync(int teacherId)
        {
            var result = await _apiService.GetAsync<List<ClassRoom>>($"api/teachers/{teacherId}/classes");
            return result ?? new List<ClassRoom>();
        }

        private List<Teacher> GetMockTeachers()
        {
            return new List<Teacher>
            {
                new() { Id = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@school.edu", TeacherId = "TCH001", Subject = "Mathematics" },
                new() { Id = 2, FirstName = "Robert", LastName = "Smith", Email = "robert.smith@school.edu", TeacherId = "TCH002", Subject = "Science" },
                new() { Id = 3, FirstName = "Maria", LastName = "Garcia", Email = "maria.garcia@school.edu", TeacherId = "TCH003", Subject = "English" },
                new() { Id = 4, FirstName = "David", LastName = "Brown", Email = "david.brown@school.edu", TeacherId = "TCH004", Subject = "History" },
                new() { Id = 5, FirstName = "Sarah", LastName = "Wilson", Email = "sarah.wilson@school.edu", TeacherId = "TCH005", Subject = "Art" }
            };
        }
    }
}
