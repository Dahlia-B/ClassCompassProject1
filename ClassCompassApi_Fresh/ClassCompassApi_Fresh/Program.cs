using ClassCompassApi_Fresh.Infrastructure.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Socket Services for real-time communication
builder.Services.AddSocketServices(8080);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Console.WriteLine("🚀 ClassCompass API with Socket Support starting...");
Console.WriteLine("📱 API: http://localhost:5004");
Console.WriteLine("📋 Swagger: http://localhost:5004/swagger");
Console.WriteLine("🧪 Test: http://localhost:5004/api/test");
Console.WriteLine("🔌 Socket Server: localhost:8080");
Console.WriteLine("📊 Socket Status: http://localhost:5004/api/socket/status");

app.Run();
