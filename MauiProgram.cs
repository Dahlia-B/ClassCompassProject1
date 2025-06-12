using Microsoft.AspNetCore.Components.WebView.Maui;

namespace YourAppName;

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

        builder.Services.AddMauiBlazorWebView();
        
        // Add HTTP client for API calls
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5004/") });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        return builder.Build();
    }
}
