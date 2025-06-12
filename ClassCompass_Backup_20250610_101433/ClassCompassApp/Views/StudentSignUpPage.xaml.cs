using ClassCompass.Shared.Services.HttpClientServices;
using ClassCompass.Shared.Models;
using Microsoft.Maui.Controls;

namespace ClassCompassApp.Views;

public partial class StudentSignUpPage : ContentPage
{
    private readonly IStudentHttpService _studentHttpService;
    private readonly ITeacherHttpService _teacherHttpService;
    private readonly IAuthHttpService _authHttpService;

    public StudentSignUpPage(IStudentHttpService studentHttpService, ITeacherHttpService teacherHttpService, IAuthHttpService authHttpService)
    {
        InitializeComponent();
        _studentHttpService = studentHttpService;
        _teacherHttpService = teacherHttpService;
        _authHttpService = authHttpService;
    }

    private async void OnRegisterStudentClicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(StudentNameEntry.Text) ||
                string.IsNullOrWhiteSpace(StudentIdEntry.Text) ||
                string.IsNullOrWhiteSpace(StudentPasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(ClassIdEntry.Text) ||
                string.IsNullOrWhiteSpace(TeacherIdEntry.Text) ||
                string.IsNullOrWhiteSpace(TeacherPasswordEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            if (!int.TryParse(TeacherIdEntry.Text, out int teacherId))
            {
                await DisplayAlert("Error", "Teacher ID must be numeric", "OK");
                return;
            }

            // Verify teacher credentials using HTTP service
            var teacherToken = await _authHttpService.LoginAsync(TeacherIdEntry.Text, TeacherPasswordEntry.Text);
            if (string.IsNullOrEmpty(teacherToken))
            {
                await DisplayAlert("Error", "Invalid teacher credentials", "OK");
                return;
            }

            if (!int.TryParse(StudentIdEntry.Text, out int studentId) ||
                !int.TryParse(ClassIdEntry.Text, out int classId))
            {
                await DisplayAlert("Error", "Student and Class ID must be numeric", "OK");
                return;
            }

            var student = new Student
            {
                Name = StudentNameEntry.Text.Trim(),
                StudentId = studentId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(StudentPasswordEntry.Text),
                Id = classId,
                TeacherId = teacherId
            };

            // Create student using HTTP service
            var createdStudent = await _studentHttpService.CreateStudentAsync(student);

            if (createdStudent != null)
            {
                await DisplayAlert("Success", "Student registered successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "Failed to register student. Please try again.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}