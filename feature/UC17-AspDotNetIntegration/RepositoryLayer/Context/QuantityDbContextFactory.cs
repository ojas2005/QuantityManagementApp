using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Context
{
    /// <summary>
    /// Factory for creating QuantityDbContext at design time (for migrations)
    /// </summary>
    public class QuantityDbContextFactory : IDesignTimeDbContextFactory<QuantityDbContext>
    {
        public QuantityDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // Get connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                // Fallback for when running from RepositoryLayer folder
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ApplicationLayer");
                configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            Console.WriteLine($"Using connection string: {connectionString}");

            // Create options builder
            var optionsBuilder = new DbContextOptionsBuilder<QuantityDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new QuantityDbContext(optionsBuilder.Options);
        }
    }
}
