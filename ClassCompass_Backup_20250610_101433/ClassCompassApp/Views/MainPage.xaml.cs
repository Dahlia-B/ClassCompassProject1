using Microsoft.Maui.Controls;
using ClassCompass.Shared.Services;
using ClassCompass.Shared.Services.HttpClientServices;
namespace ClassCompassApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Test API connection when page loads
        _ = Task.Run(TestApiConnectionOnStartup);
    }

    private async void OnSchoolSignUpClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SchoolSignUpPage));
    }

    private async void OnTeacherSignUpClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TeacherSignUpPage));
    }

    private async void OnStudentSignUpClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(StudentSignUpPage));
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    // ADD: Test API Connection Button
    private async void OnTestApiClicked(object sender, EventArgs e)
    {
        try
        {
            var apiService = new ApiService();

            // Test basic connection
            var connectionTest = await apiService.TestConnectionAsync();
            await DisplayAlert("API Connection Test", $"Result: {connectionTest}", "OK");

            // Test attendance endpoint
            var attendanceTest = await apiService.GetAttendanceTestAsync();
            if (attendanceTest != null)
            {
                await DisplayAlert("Attendance Test", "Attendance endpoint working!", "OK");
            }
            else
            {
                await DisplayAlert("Attendance Test", "Attendance endpoint failed", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("API Error", $"Connection failed: {ex.Message}", "OK");
        }
    }

    // Test API connection automatically when app starts
    private async Task TestApiConnectionOnStartup()
    {
        try
        {
            await Task.Delay(1000); // Wait 1 second for app to fully load

            var apiService = new ApiService();
            var result = await apiService.TestConnectionAsync();

            // Log to debug output
            System.Diagnostics.Debug.WriteLine($"🚀 API Connection Test: {result}");

            // Show result on main thread
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (result?.Contains("ClassCompass API is running") == true)
                {
                    System.Diagnostics.Debug.WriteLine("✅ API Connection Successful!");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"❌ API Connection Failed: {result}");
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"❌ API Connection Error: {ex.Message}");
        }
    }
}