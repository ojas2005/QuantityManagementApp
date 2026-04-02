using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Configuration;
using ApplicationLayer.Controllers;
using RepositoryLayer.Interfaces;

namespace ApplicationLayer
{
    /// <summary>
    /// Main entry point for the Quantity Measurement Application
    /// </summary>
    public class QuantityMeasurementApp
    {
        private static QuantityMeasurementApp? _instance;
        private readonly QuantityMeasurementController _controller;
        private readonly ILogger<QuantityMeasurementApp> _logger;
        private readonly IQuantityMeasurementRepository _repository;
        private readonly IServiceProvider _serviceProvider;

        private QuantityMeasurementApp()
        {
            // Configure services
            _serviceProvider = AppConfiguration.ConfigureServices();
            
            // Get required services
            _logger = _serviceProvider.GetRequiredService<ILogger<QuantityMeasurementApp>>();
            _controller = _serviceProvider.GetRequiredService<QuantityMeasurementController>();
            _repository = _serviceProvider.GetRequiredService<IQuantityMeasurementRepository>();
            
            _logger.LogInformation("QuantityMeasurementApp initialized with {RepositoryType}", 
                _repository.GetType().Name);
        }

        public static QuantityMeasurementApp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QuantityMeasurementApp();
                return _instance;
            }
        }

        public void Run()
        {
            _logger.LogInformation("Application started");
            
            bool exit = false;
            while (!exit)
            {
                _controller.DisplayMainMenu();
                
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                        exit = true;
                    else
                        _controller.RunSelectedDemonstration(choice);
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a number.");
                }
            }
            
            // Clean up resources
            ReleaseResources();
        }

        public void DeleteAllMeasurements()
        {
            _logger.LogWarning("Deleting all measurements from repository");
            _repository.Clear();
        }

        public void ShowRepositoryStatistics()
        {
            _logger.LogInformation("Repository Statistics:\n{Stats}", _repository.GetPoolStatistics());
            Console.WriteLine(_repository.GetPoolStatistics());
        }

        private void ReleaseResources()
        {
            _logger.LogInformation("Releasing resources...");
            _repository.ReleaseResources();
            
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
            
            _logger.LogInformation("Resources released, application shutting down");
        }

        // This is the entry point
        public static void Main(string[] args)
        {
            try
            {
                var app = QuantityMeasurementApp.Instance;
                
                // Optional: Clear all measurements on startup (comment out if you want to keep data)
                // app.DeleteAllMeasurements();
                
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}