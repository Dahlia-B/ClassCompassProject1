using ClassCompass.Shared.Models;
using ClassCompass.Shared.Services.HttpClientServices;
using Microsoft.Maui.Controls;

namespace ClassCompassApp.Views
{
    public partial class SchoolSignUpPage : ContentPage
    {
        private readonly ISchoolHttpService _schoolHttpService;

        public SchoolSignUpPage(ISchoolHttpService schoolHttpService)
        {
            InitializeComponent();
            _schoolHttpService = schoolHttpService;
        }

        private async void OnRegisterSchoolClicked(object sender, EventArgs e)
        {
            try
            {
                // Validate input fields
                if (string.IsNullOrWhiteSpace(SchoolNameEntry.Text) ||
                    string.IsNullOrWhiteSpace(SchoolIdEntry.Text) ||
                    string.IsNullOrWhiteSpace(ClassCountEntry.Text))
                {
                    await DisplayAlert("Error", "Please fill in all fields", "OK");
                    return;
                }

                if (!int.TryParse(SchoolIdEntry.Text, out int schoolId))
                {
                    await DisplayAlert("Error", "School ID must be numeric.", "OK");
                    return;
                }

                if (!int.TryParse(ClassCountEntry.Text, out int classCount))
                {
                    await DisplayAlert("Error", "Number of classes must be numeric.", "OK");
                    return;
                }

                // Disable the button during processing
                if (sender is Button button)
                {
                    button.IsEnabled = false;
                    button.Text = "Registering...";
                }

                // Create the school object
                var school = new School
                {
                    SchoolId = schoolId,
                    Name = SchoolNameEntry.Text,
                    NumberOfClasses = classCount,
                    Description = "No description provided", // Remove SchoolDescriptionEntry reference
                    CreatedDate = DateTime.Now,
                    Classes = new List<ClassRoom>(), // Use ClassRoom instead of Class
                    Teachers = new List<Teacher>()
                };

                // Call the actual API
                var createdSchool = await _schoolHttpService.CreateSchoolAsync(school);

                if (createdSchool != null)
                {
                    await DisplayAlert("Success",
                        $"School registered successfully!\n\n" +
                        $"📚 School Name: {createdSchool.Name}\n" +
                        $"🆔 School ID: {createdSchool.SchoolId}\n" +
                        $"🏫 Number of Classes: {createdSchool.NumberOfClasses}\n\n" +
                        $"✅ Data saved to database successfully!",
                        "OK");

                    // Clear the form
                    SchoolNameEntry.Text = string.Empty;
                    SchoolIdEntry.Text = string.Empty;
                    ClassCountEntry.Text = string.Empty;
                    // Remove SchoolDescriptionEntry clearing since it doesn't exist

                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to create school. Please try again.", "OK");
                }
            }
            catch (HttpRequestException httpEx)
            {
                await DisplayAlert("Network Error",
                    $"Could not connect to server. Please check your internet connection.\n\nDetails: {httpEx.Message}",
                    "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",
                    $"Registration failed: {ex.Message}",
                    "OK");
            }
            finally
            {
                // Reset button
                if (sender is Button button)
                {
                    button.IsEnabled = true;
                    button.Text = "Register School";
                }
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}