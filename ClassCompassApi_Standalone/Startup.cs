using Microsoft.EntityFrameworkCore;
using ClassCompassApi.Shared.Data;
using ClassCompassApi.Data;

namespace ClassCompassApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Entity Framework with InMemory database
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("ClassCompassDb"));

            // Add controllers
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Configure JSON serialization to handle circular references
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });

            // Add API documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
                { 
                    Title = "ClassCompass API", 
                    Version = "v1",
                    Description = "API for ClassCompass school management system"
                });
            });

            // Add CORS for mobile app access
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMobileApp", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Note: Removed TcpSocketServer to avoid import errors
            // Can be added back later when Infrastructure is properly configured
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClassCompass API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            // Essential middleware in correct order
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowMobileApp");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                // Add a simple health check endpoint
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("ClassCompass API is running! Visit /swagger for documentation.");
                });
                
                endpoints.MapGet("/health", async context =>
                {
                    await context.Response.WriteAsync("Healthy");
                });
            });

            // Seed the database with sample data
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    DataSeeder.SeedData(context);
                    Console.WriteLine("? Database seeded successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"? Database seeding failed: {ex.Message}");
                }
            }
        }
    }
}

