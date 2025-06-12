using ClassCompassWeb.Services.Api;
using ClassCompassWeb.Services.Socket;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add HTTP client for API calls
builder.Services.AddHttpClient();

// Add our custom services
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<ISocketService, SocketService>();

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

Console.WriteLine("🚀 ClassCompass Web Application Starting...");
Console.WriteLine("🌐 Web App: https://localhost:7000");
Console.WriteLine("🔌 API Integration: Configured");
Console.WriteLine("📊 Dashboard: Ready");

app.Run();
