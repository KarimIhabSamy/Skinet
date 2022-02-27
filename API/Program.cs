using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
      
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
                {
                    builder.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }));

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(context, loggerFactory);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
