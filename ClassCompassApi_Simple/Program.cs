using ClassCompass.Shared.Data;
using ClassCompassApi_Simple.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Entity Framework with MySQL using your existing AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ClassCompassConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ClassCompassConnection"))
    ));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Create database and tables if they don't exist
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // This will create the database and all tables based on your models
        context.Database.EnsureCreated();
        Console.WriteLine("✅ ClassCompass Database and tables created successfully!");
        Console.WriteLine("📊 Tables created: Students, Teachers, Schools, ClassRooms, Attendances, Grades, BehaviorRemarks, Assignments, HomeworkSubmissions");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database creation failed: {ex.Message}");
        Console.WriteLine("💡 Make sure MySQL is running and the connection string is correct!");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

Console.WriteLine("🚀 ClassCompass API with MySQL is starting...");
Console.WriteLine("📊 Database: MySQL (classcompass_db)");
Console.WriteLine("🌐 API URL: http://localhost:5004");
Console.WriteLine("🔍 View data in MySQL Workbench!");

app.Run();