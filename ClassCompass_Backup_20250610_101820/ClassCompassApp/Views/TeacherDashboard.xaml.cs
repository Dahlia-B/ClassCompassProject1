using ClassCompassApp.Views;
using Microsoft.Maui.Controls;
using System;
using ClassCompass.Shared.Services.HttpClientServices;
namespace ClassCompassApp.Views
{
    public partial class TeacherDashboard : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;

        public TeacherDashboard(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private async void OnScheduleClicked(object sender, EventArgs e)
        {
            var page = _serviceProvider.GetService<TeacherSchedulePage>();
            await Navigation.PushAsync(page);
        }

        private async void OnHomeworkClicked(object sender, EventArgs e)
        {
            var page = _serviceProvider.GetService<HomeworkAssignmentPage>();
            await Navigation.PushAsync(page);
        }

        private async void OnAttendanceClicked(object sender, EventArgs e)
        {
            var page = _serviceProvider.GetService<AttendanceTrackingPage>();
            await Navigation.PushAsync(page);
        }
    }
}