using System;
using ApplicationLayer.Controllers;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;

namespace ApplicationLayer
{
    /// <summary>
    /// Main entry point for the Quantity Measurement Application
    /// </summary>
    public class QuantityMeasurementApp
    {
        private static QuantityMeasurementApp _instance;
        private readonly QuantityMeasurementController _controller;

        private QuantityMeasurementApp()
        {
            // Initialize dependencies
            IQuantityMeasurementRepository repository = new QuantityMeasurementCacheRepository();
            IQuantityMeasurementService service = new QuantityMeasurementService(repository);
            _controller = new QuantityMeasurementController(service);
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
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void Main(string[] args)
        {
            try
            {
                var app = QuantityMeasurementApp.Instance;
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