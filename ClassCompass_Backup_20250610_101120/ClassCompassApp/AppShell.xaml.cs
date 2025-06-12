using ClassCompassApp.Views;
using Microsoft.Maui.Controls;

namespace ClassCompassApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register navigation routes
            Routing.RegisterRoute(nameof(TeacherGradingPage), typeof(TeacherGradingPage));
            Routing.RegisterRoute(nameof(AttendanceTrackingPage), typeof(AttendanceTrackingPage));
            Routing.RegisterRoute(nameof(HomeworkSubmissionPage), typeof(HomeworkSubmissionPage));

            Routing.RegisterRoute(nameof(SchoolSignUpPage), typeof(SchoolSignUpPage));
            Routing.RegisterRoute(nameof(TeacherSignUpPage), typeof(TeacherSignUpPage));
            Routing.RegisterRoute(nameof(StudentSignUpPage), typeof(StudentSignUpPage));

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(TeacherLoginPage), typeof(TeacherLoginPage));
            Routing.RegisterRoute(nameof(StudentLoginPage), typeof(StudentLoginPage));
        }
    }
}
