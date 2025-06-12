using ClassCompass.Shared.Services.HttpClientServices;
using ClassCompass.Shared.Models;
using Microsoft.Maui.Controls;

namespace ClassCompassApp.Views;

public partial class TeacherSignUpPage : ContentPage
{
    private readonly ITeacherHttpService _teacherHttpService;
    private readonly ISchoolHttpService _schoolHttpService;

    public TeacherSignUpPage(ITeacherHttpService teacherHttpService, ISchoolHttpService schoolHttpService)
    {
        InitializeComponent();
        _teacherHttpService = teacherHttpService;
        _schoolHttpService = schoolHttpService;
    }

    private async void OnRegisterTeacherClicked(object sender, EventArgs e)
    {
        RegisterTeacherButton.IsEnabled = false;
        try
        {
            if (string.IsNullOrWhiteSpace(TeacherNameEntry.Text) ||
                string.IsNullOrWhiteSpace(TeacherIdEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(SubjectEntry.Text) ||
                string.IsNullOrWhiteSpace(SchoolIdEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields", "OK");
                return;
            }

            if (!int.TryParse(TeacherIdEntry.Text, out int teacherId) ||
                !int.TryParse(SchoolIdEntry.Text, out int schoolId))
            {
                await DisplayAlert("Error", "IDs must be numeric", "OK");
                return;
            }

            // Verify school exists (optional - you might want to check if school is valid)
            var school = await _schoolHttpService.GetSchoolByIdAsync(schoolId);
            if (school == null)
            {
                await DisplayAlert("Error", "Invalid School ID. Please verify the school exists.", "OK");
                return;
            }

            var hashedPassword = await Task.Run(() => BCrypt.Net.BCrypt.HashPassword(PasswordEntry.Text));

            var teacher = new Teacher
            {
                Name = TeacherNameEntry.Text.Trim(),
                TeacherId = teacherId,
                PasswordHash = hashedPassword,
                Subject = SubjectEntry.Text.Trim(),
                SchoolId = schoolId
            };

            // Create teacher using HTTP service
            var createdTeacher = await _teacherHttpService.CreateTeacherAsync(teacher);

            if (createdTeacher != null)
            {
                await DisplayAlert("Success", "Teacher registered successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "Failed to register teacher. Please try again.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Registration failed: {ex.Message}", "OK");
        }
        finally
        {
            RegisterTeacherButton.IsEnabled = true;
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}