using Microsoft.Extensions.Logging;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entities;
using RepositoryLayer.Interfaces;
using System.Text;

namespace ApplicationLayer.Controllers
{
    /// <summary>
    /// Controller for handling quantity measurement operations with user input
    /// </summary>
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;
        private readonly IQuantityMeasurementRepository _repository;
        private readonly ILogger<QuantityMeasurementController> _logger;

        public QuantityMeasurementController(
            IQuantityMeasurementService service,
            IQuantityMeasurementRepository repository,
            ILogger<QuantityMeasurementController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("  QUANTITY MEASUREMENT APPLICATION");
            Console.WriteLine("  (Interactive Mode - UC16)");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Compare Two Quantities");
            Console.WriteLine("2. Convert a Quantity");
            Console.WriteLine("3. Add Two Quantities");
            Console.WriteLine("4. Subtract Two Quantities");
            Console.WriteLine("5. Divide Two Quantities");
            Console.WriteLine("6. Temperature Operations (Unsupported Demo)");
            Console.WriteLine("7. Cross-Category Prevention Demo");
            Console.WriteLine("8. Show Database Statistics");
            Console.WriteLine("9. View Recent Operations");
            Console.WriteLine("0. Exit");
            Console.WriteLine("========================================");
            Console.Write("Enter your choice: ");
        }

        public void RunSelectedDemonstration(int choice)
        {
            switch (choice)
            {
                case 1:
                    InteractiveCompare();
                    break;
                case 2:
                    InteractiveConvert();
                    break;
                case 3:
                    InteractiveAdd();
                    break;
                case 4:
                    InteractiveSubtract();
                    break;
                case 5:
                    InteractiveDivide();
                    break;
                case 6:
                    DemonstrateTemperatureUnsupportedOperations();
                    break;
                case 7:
                    DemonstrateCrossCategoryPrevention();
                    break;
                case 8:
                    ShowDatabaseStatistics();
                    break;
                case 9:
                    ViewRecentOperations();
                    break;
                case 0:
                    _logger.LogInformation("Exiting application...");
                    Console.WriteLine("\nExiting application...");
                    return;
                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    break;
            }

            if (choice != 0)
            {
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
        }

        #region Interactive Methods

        private void InteractiveCompare()
        {
            _logger.LogInformation("=== INTERACTIVE COMPARISON ===");
            Console.WriteLine("\n=== COMPARE TWO QUANTITIES ===\n");

            try
            {
                // Get first quantity
                Console.WriteLine("Enter FIRST quantity:");
                var first = GetQuantityFromUser();
                
                // Get second quantity
                Console.WriteLine("\nEnter SECOND quantity:");
                var second = GetQuantityFromUser();

                // Perform comparison
                Console.WriteLine($"\nComparing {first} and {second}...");
                var result = _service.CompareQuantities(first, second);
                
                DisplayResult(result, $"{first} == {second}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during comparison");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void InteractiveConvert()
        {
            _logger.LogInformation("=== INTERACTIVE CONVERSION ===");
            Console.WriteLine("\n=== CONVERT A QUANTITY ===\n");

            try
            {
                // Get source quantity
                Console.WriteLine("Enter the quantity to convert:");
                var source = GetQuantityFromUser();
                
                // Get target unit
                Console.WriteLine("\nEnter target unit (e.g., ft, in, cm, kg, g, L, mL, °C, °F):");
                Console.Write("Target unit: ");
                var targetUnit = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(targetUnit))
                {
                    Console.WriteLine("Target unit cannot be empty.");
                    return;
                }

                // Perform conversion
                Console.WriteLine($"\nConverting {source} to {targetUnit}...");
                var result = _service.ConvertQuantity(source, targetUnit);
                
                DisplayResult(result, $"{source} to {targetUnit}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during conversion");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void InteractiveAdd()
        {
            _logger.LogInformation("=== INTERACTIVE ADDITION ===");
            Console.WriteLine("\n=== ADD TWO QUANTITIES ===\n");

            try
            {
                // Get first quantity
                Console.WriteLine("Enter FIRST quantity:");
                var first = GetQuantityFromUser();
                
                // Get second quantity
                Console.WriteLine("\nEnter SECOND quantity:");
                var second = GetQuantityFromUser();

                // Ask for target unit (optional)
                Console.Write("\nEnter target unit (press Enter for same as first unit): ");
                var targetUnit = Console.ReadLine()?.Trim();
                
                QuantityMeasurementEntity result;
                if (string.IsNullOrWhiteSpace(targetUnit))
                {
                    result = _service.AddQuantities(first, second);
                }
                else
                {
                    result = _service.AddQuantities(first, second, targetUnit);
                }
                
                DisplayResult(result, $"{first} + {second}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during addition");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void InteractiveSubtract()
        {
            _logger.LogInformation("=== INTERACTIVE SUBTRACTION ===");
            Console.WriteLine("\n=== SUBTRACT TWO QUANTITIES ===\n");

            try
            {
                // Get first quantity
                Console.WriteLine("Enter FIRST quantity (minuend):");
                var first = GetQuantityFromUser();
                
                // Get second quantity
                Console.WriteLine("\nEnter SECOND quantity (subtrahend):");
                var second = GetQuantityFromUser();

                // Ask for target unit (optional)
                Console.Write("\nEnter target unit (press Enter for same as first unit): ");
                var targetUnit = Console.ReadLine()?.Trim();
                
                QuantityMeasurementEntity result;
                if (string.IsNullOrWhiteSpace(targetUnit))
                {
                    result = _service.SubtractQuantities(first, second);
                }
                else
                {
                    result = _service.SubtractQuantities(first, second, targetUnit);
                }
                
                DisplayResult(result, $"{first} - {second}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during subtraction");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void InteractiveDivide()
        {
            _logger.LogInformation("=== INTERACTIVE DIVISION ===");
            Console.WriteLine("\n=== DIVIDE TWO QUANTITIES ===\n");

            try
            {
                // Get first quantity
                Console.WriteLine("Enter FIRST quantity (numerator):");
                var first = GetQuantityFromUser();
                
                // Get second quantity
                Console.WriteLine("\nEnter SECOND quantity (denominator):");
                var second = GetQuantityFromUser();

                // Perform division
                Console.WriteLine($"\nDividing {first} by {second}...");
                var result = _service.DivideQuantities(first, second);
                
                DisplayResult(result, $"{first} ÷ {second}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during division");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Helper method to get quantity input from user
        /// </summary>
        private QuantityDTO GetQuantityFromUser()
        {
            // Get value
            Console.Write("Enter value: ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                throw new ArgumentException("Invalid number format");
            }

            // Get unit
            Console.Write("Enter unit (ft/in/cm/yd/kg/g/L/mL/gal/°C/°F): ");
            var unit = Console.ReadLine()?.Trim() ?? "";
            
            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException("Unit cannot be empty");
            }

            // Auto-detect measurement type from unit
            string measurementType = DetectMeasurementType(unit);
            
            return new QuantityDTO(value, unit, measurementType);
        }

        /// <summary>
        /// Detect measurement type from unit string
        /// </summary>
        private string DetectMeasurementType(string unit)
        {
            return unit.ToLower() switch
            {
                "ft" or "in" or "cm" or "yd" => "Length",
                "kg" or "g" or "lb" => "Weight",
                "l" or "ml" or "gal" => "Volume",
                "°c" or "°f" or "c" or "f" => "Temperature",
                _ => throw new ArgumentException($"Unknown unit: {unit}")
            };
        }

        #endregion

        #region Demonstration Methods (Keep for reference)

        public void DemonstrateTemperatureUnsupportedOperations()
        {
            _logger.LogInformation("=== TEMPERATURE UNSUPPORTED OPERATIONS ===");
            Console.WriteLine("\n=== TEMPERATURE UNSUPPORTED OPERATIONS ===\n");
            Console.WriteLine("Temperature units do not support arithmetic operations.\n");

            var celsius100 = new QuantityDTO(100.0, "°C", "Temperature");
            var celsius50 = new QuantityDTO(50.0, "°C", "Temperature");

            DisplayResult(_service.AddQuantities(celsius100, celsius50), "100°C + 50°C (should fail)");
            DisplayResult(_service.SubtractQuantities(celsius100, celsius50), "100°C - 50°C (should fail)");
            DisplayResult(_service.DivideQuantities(celsius100, celsius50), "100°C ÷ 50°C (should fail)");
        }

        public void DemonstrateCrossCategoryPrevention()
        {
            _logger.LogInformation("=== CROSS-CATEGORY PREVENTION ===");
            Console.WriteLine("\n=== CROSS-CATEGORY PREVENTION ===\n");
            Console.WriteLine("Operations between different measurement types should fail.\n");

            var length = new QuantityDTO(10.0, "ft", "Length");
            var weight = new QuantityDTO(5.0, "kg", "Weight");
            var volume = new QuantityDTO(2.0, "L", "Volume");

            DisplayResult(_service.CompareQuantities(length, weight), "10 ft == 5 kg?");
            DisplayResult(_service.AddQuantities(length, volume), "10 ft + 2 L?");
        }

        #endregion

        #region Database Statistics

        private void ShowDatabaseStatistics()
        {
            _logger.LogInformation("=== DATABASE STATISTICS ===");
            Console.WriteLine("\n=== DATABASE STATISTICS ===\n");
            
            try
            {
                var stats = _repository.GetPoolStatistics();
                Console.WriteLine(stats);
                
                var allMeasurements = _repository.GetAll();
                
                if (!allMeasurements.Any())
                {
                    Console.WriteLine("No measurements found in database.");
                    return;
                }

                Console.WriteLine("\n=== RECENT ACTIVITY ===\n");
                
                var recent = allMeasurements.OrderByDescending(m => m.Timestamp).Take(5);
                foreach (var m in recent)
                {
                    Console.WriteLine($"  {m.Timestamp:HH:mm:ss} - {m}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting database statistics");
                Console.WriteLine($"Error getting statistics: {ex.Message}");
            }
        }

        private void ViewRecentOperations()
        {
            _logger.LogInformation("=== RECENT OPERATIONS ===");
            Console.WriteLine("\n=== RECENT OPERATIONS ===\n");
            
            try
            {
                var allMeasurements = _repository.GetAll();
                
                if (!allMeasurements.Any())
                {
                    Console.WriteLine("No operations found in database.");
                    return;
                }

                Console.WriteLine($"Total operations: {allMeasurements.Count}\n");
                Console.WriteLine("Last 10 operations:");
                Console.WriteLine("-------------------");
                
                var recent = allMeasurements.OrderByDescending(m => m.Timestamp).Take(10);
                foreach (var m in recent)
                {
                    Console.WriteLine($"[{m.Timestamp:yyyy-MM-dd HH:mm:ss}] {m}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing recent operations");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion

        private void DisplayResult(QuantityMeasurementEntity entity, string description)
        {
            if (entity.HasError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n{description}:");
                Console.WriteLine($"  [ERROR] {entity.ErrorMessage}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{description}:");
                Console.WriteLine($"  {entity}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
