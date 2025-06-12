using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace ClassCompass
{
    public partial class SchoolRegistrationPage : ContentPage
    {
        private readonly ApiService _apiService;
        
        public SchoolRegistrationPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }
        
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                // Show loading state
                RegisterButton.Text = "Registering...";
                RegisterButton.IsEnabled = false;
                
                // Validate input
                if (string.IsNullOrWhiteSpace(SchoolNameEntry.Text))
                {
                    await DisplayAlert("Validation Error", "Please enter school name", "OK");
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(ClassCountEntry.Text) || !int.TryParse(ClassCountEntry.Text, out int classCount))
                {
                    await DisplayAlert("Validation Error", "Please enter a valid number of classes", "OK");
                    return;
                }
                
                var registration = new SchoolRegistration
                {
                    Name = SchoolNameEntry.Text,
                    NumberOfClasses = classCount,
                    Description = DescriptionEntry.Text ?? ""
                };
                
                bool success = await _apiService.RegisterSchoolAsync(registration);
                
                if (success)
                {
                    await DisplayAlert("Success", 
                        "? School registered successfully!", "OK");
                    await Navigation.PopAsync(); // Go back
                }
                else
                {
                    await DisplayAlert("Registration Failed", 
                        "? Could not register school.\n\n" +
                        "Please check:\n" +
                        "• API server is running\n" +
                        "• Network connection\n" +
                        "• All required fields", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Registration error: {ex.Message}", "OK");
            }
            finally
            {
                RegisterButton.Text = "?? Register School";
                RegisterButton.IsEnabled = true;
            }
        }
        
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

