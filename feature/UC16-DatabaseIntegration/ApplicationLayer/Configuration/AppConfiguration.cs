using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using ApplicationLayer.Controllers;

namespace ApplicationLayer.Configuration
{
    /// <summary>
    /// Application configuration and service registration
    /// </summary>
    public static class AppConfiguration
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Configure logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            // Configure DbContext
            services.AddDbContext<QuantityDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging(false);
                options.EnableDetailedErrors(false);
            });

            // Register repository factory
            services.AddSingleton<RepositoryFactory>();

            // Register repository
            services.AddScoped<IQuantityMeasurementRepository>(sp =>
            {
                var factory = sp.GetRequiredService<RepositoryFactory>();
                return factory.CreateRepository();
            });

            // Register service
            services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();

            // Register controller
            services.AddScoped<QuantityMeasurementController>();

            return services.BuildServiceProvider();
        }
    }
}
