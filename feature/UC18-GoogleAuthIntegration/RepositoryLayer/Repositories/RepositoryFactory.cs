using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Repositories;

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
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public IQuantityMeasurementRepository CreateRepository()
    {
        var useDatabase = _configuration.GetValue<bool>("RepositorySettings:UseDatabaseRepository");
        var repositoryType = _configuration.GetValue<string>("RepositorySettings:RepositoryType");
        
        _logger.LogInformation("�� RepositoryFactory config: UseDatabase={UseDatabase}, Type={RepositoryType}", 
            useDatabase, repositoryType);

        if (useDatabase || "Database".Equals(repositoryType, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation("✅ Creating DATABASE repository");
            
            try
            {
                // Get services from the current scope
                var context = _serviceProvider.GetRequiredService<QuantityDbContext>();
                var repoLogger = _serviceProvider.GetRequiredService<ILogger<QuantityMeasurementDatabaseRepository>>();
                
                return new QuantityMeasurementDatabaseRepository(context, repoLogger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Database repository failed, falling back to CACHE");
                return CreateCacheRepository();
            }
        }
        
        _logger.LogInformation("📦 Creating CACHE repository");
        return CreateCacheRepository();
    }

    private IQuantityMeasurementRepository CreateCacheRepository()
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<QuantityMeasurementCacheRepository>>();
        return new QuantityMeasurementCacheRepository(logger);
    }
}
