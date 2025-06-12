using ClassCompass.Shared.Services.HttpClientServices;
using ClassCompass.Shared.Models;
using Microsoft.Maui.Controls;

namespace ClassCompassApp.Views
{
    public partial class TeacherLoginPage : ContentPage
    {
        private readonly IAuthHttpService _authHttpService;
        private readonly ITeacherHttpService _teacherHttpService;

        public TeacherLoginPage(IAuthHttpService authHttpService, ITeacherHttpService teacherHttpService)
        {
            InitializeComponent();
            _authHttpService = authHttpService;
            _teacherHttpService = teacherHttpService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                // Defensive null check if controls are not assigned properly
                if (TeacherIdEntry == null || PasswordEntry == null)
                {
                    await DisplayAlert("Error", "Login controls are not initialized", "OK");
                    return;
                }

                var teacherIdText = TeacherIdEntry.Text;
                var passwordText = PasswordEntry.Text;

                if (string.IsNullOrWhiteSpace(teacherIdText) || string.IsNullOrWhiteSpace(passwordText))
                {
                    await DisplayAlert("Error", "Please fill in all fields", "OK");
                    return;
                }

                if (!int.TryParse(teacherIdText, out int teacherId))
                {
                    await DisplayAlert("Error", "Teacher ID must be a number", "OK");
                    return;
                }

                // Use HTTP service for authentication
                var token = await _authHttpService.LoginAsync(teacherIdText, passwordText);

                if (!string.IsNullOrEmpty(token))
                {
                    // Store token for future API calls
                    // SecureStorage.SetAsync("auth_token", token);

                    // Navigate to TeacherDashboard (make sure route is registered)
                    await Shell.Current.GoToAsync($"//{nameof(TeacherDashboard)}");
                }
                else
                {
                    await DisplayAlert("Login Failed", "Invalid teacher ID or password", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}