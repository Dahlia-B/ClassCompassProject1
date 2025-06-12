using ClassCompass.Shared.Models;
using Microsoft.Maui.Controls;
namespace ClassCompassApp;

public partial class App : Application
{
    public class UserModel
    {
        public string UserId { get; set; } = string.Empty;
    }

    public static UserModel? CurrentUser { get; set; }

    public App()
    {
        InitializeComponent();
        MainPage = new Views.MainPage();
    }
}
