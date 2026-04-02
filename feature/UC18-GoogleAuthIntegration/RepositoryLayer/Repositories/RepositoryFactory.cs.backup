using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Repositories
{
    /// <summary>
    /// Factory for creating repository instances based on configuration
    /// </summary>
    public class RepositoryFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RepositoryFactory> _logger;

        public RepositoryFactory(
            IConfiguration configuration,
            IServiceProvider serviceProvider,
            ILogger<RepositoryFactory> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IQuantityMeasurementRepository CreateRepository()
        {
            // Determine which repository to use from configuration
            var useDatabase = _configuration.GetValue<bool>("RepositorySettings:UseDatabaseRepository");
            var repositoryType = _configuration.GetValue<string>("RepositorySettings:RepositoryType");

            _logger.LogInformation($"Creating repository with UseDatabase={useDatabase}, Type={repositoryType}");

            if (useDatabase || repositoryType?.Equals("Database", StringComparison.OrdinalIgnoreCase) == true)
            {
                try
                {
                    var context = _serviceProvider.GetRequiredService<QuantityDbContext>();
                    var repoLogger = _serviceProvider.GetRequiredService<ILogger<QuantityMeasurementDatabaseRepository>>();
                    
                    // Ensure database is created
                    context.Database.EnsureCreated();
                    
                    _logger.LogInformation("Database repository created successfully");
                    return new QuantityMeasurementDatabaseRepository(context, repoLogger);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create database repository, falling back to cache repository");
                    return CreateCacheRepository();
                }
            }
            else
            {
                return CreateCacheRepository();
            }
        }

        private IQuantityMeasurementRepository CreateCacheRepository()
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<QuantityMeasurementCacheRepository>>();
            _logger.LogInformation("Cache repository created");
            return new QuantityMeasurementCacheRepository(logger);
        }
    }
}
