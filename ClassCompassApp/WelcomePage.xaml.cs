using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace ClassCompass
{
    public partial class WelcomePage : ContentPage
    {
        private readonly ApiService _apiService;
        
        public WelcomePage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }
        
        private async void OnTestApiConnectionClicked(object sender, EventArgs e)
        {
            try
            {
                // Show loading state
                TestApiButton.Text = "Testing...";
                TestApiButton.IsEnabled = false;
                ApiStatusLabel.Text = "Connecting to API...";
                
                bool isConnected = await _apiService.TestConnectionAsync();
                
                if (isConnected)
                {
                    await DisplayAlert("Success", 
                        "? API Connection successful!\n" +
                        $"Connected to: {ApiConfig.BaseUrl}", "OK");
                    TestApiButton.Text = "? API Connected";
                    TestApiButton.BackgroundColor = Colors.Green;
                    ApiStatusLabel.Text = "? API is working correctly";
                }
                else
                {
                    await DisplayAlert("Connection Failed", 
                        "? Cannot connect to API server.\n\n" +
                        "Please check:\n" +
                        "• API server is running\n" +
                        "• Same WiFi network\n" +
                        "• Firewall settings\n\n" +
                        $"Trying to connect to: {ApiConfig.BaseUrl}", "OK");
                    TestApiButton.Text = "? Connection Failed";
                    TestApiButton.BackgroundColor = Colors.Red;
                    ApiStatusLabel.Text = "? API connection failed";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Connection test failed: {ex.Message}", "OK");
                TestApiButton.Text = "? Test Failed";
                TestApiButton.BackgroundColor = Colors.Red;
                ApiStatusLabel.Text = "? Test error occurred";
            }
            finally
            {
                TestApiButton.IsEnabled = true;
            }
        }
        
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Login", "Login functionality - implement as needed", "OK");
        }
        
        private async void OnRegisterSchoolClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SchoolRegistrationPage());
        }
    }
}

