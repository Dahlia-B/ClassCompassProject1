using Microsoft.Maui.Controls;
using ClassCompass.Shared.Services.HttpClientServices;

namespace ClassCompassApp.Views;

public partial class StudentLoginPage : ContentPage
{
    private readonly IAuthHttpService _authHttpService;
    private readonly IStudentHttpService _studentHttpService;

    public StudentLoginPage(IAuthHttpService authHttpService, IStudentHttpService studentHttpService)
    {
        InitializeComponent();
        _authHttpService = authHttpService;
        _studentHttpService = studentHttpService;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(StudentIdEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        try
        {
            // Use HTTP service for authentication
            var token = await _authHttpService.LoginAsync(StudentIdEntry.Text, PasswordEntry.Text);

            if (!string.IsNullOrEmpty(token))
            {
                // Store token for future API calls (you might want to use secure storage)
                // SecureStorage.SetAsync("auth_token", token);

                int studentId = int.Parse(StudentIdEntry.Text);
                await Shell.Current.GoToAsync($"//StudentDashboardPage?studentId={studentId}");
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid student ID or password", "OK");
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