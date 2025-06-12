using System;
using Microsoft.Extensions.Logging;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using ClassCompass.Shared.Services.HttpClientServices;
using ClassCompassApp.Views;

namespace ClassCompassApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Configure your Azure API base URL
        // In ClassCompassApp\MauiProgram.cs, change line ~18 back to:
       var apiBaseUrl = "http://192.168.68.83:5004/";
        builder.Services.AddHttpClient<IStudentHttpService, StudentHttpService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "ClassCompass-Mobile");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddHttpClient<IAuthHttpService, AuthHttpService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "ClassCompass-Mobile");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddHttpClient<IAttendanceHttpService, AttendanceHttpService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "ClassCompass-Mobile");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddHttpClient<IGradesHttpService, GradesHttpService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "ClassCompass-Mobile");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddHttpClient<ITeacherHttpService, TeacherHttpService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "ClassCompass-Mobile");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddHttpClient<IHomeworkHttpService, HomeworkHttpService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "ClassCompass-Mobile");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        builder.Services.AddHttpClient<ISchoolHttpService, SchoolHttpService>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "ClassCompass-Mobile");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        // Register your page views
        builder.Services.AddTransient<TeacherGradingPage>();
        builder.Services.AddTransient<AttendanceTrackingPage>();
        builder.Services.AddTransient<HomeworkSubmissionPage>();
        builder.Services.AddTransient<SchoolSignUpPage>();
        builder.Services.AddTransient<TeacherSignUpPage>();
        builder.Services.AddTransient<StudentSignUpPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<TeacherLoginPage>();
        builder.Services.AddTransient<StudentLoginPage>();
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Test API connection
        TestApiConnection(app, apiBaseUrl);

        return app;
    }

    private static async void TestApiConnection(MauiApp app, string apiBaseUrl)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();

            // Test basic API connectivity - try multiple endpoints
            var endpoints = new[] { "", "api/students", "swagger" };
            bool connected = false;

            foreach (var endpoint in endpoints)
            {
                try
                {
                    var response = await httpClient.GetAsync($"{apiBaseUrl}{endpoint}");
                    if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine($"✅ Connected to ClassCompass API successfully! (tested {endpoint})");
                        connected = true;
                        break;
                    }
                }
                catch { /* Try next endpoint */ }
            }

            if (!connected)
                Console.WriteLine("❌ Could not connect to any API endpoints");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ API connection failed: {ex.Message}");
            Console.WriteLine("🔄 Mobile app will work offline or when API is available");
        }
    }
}