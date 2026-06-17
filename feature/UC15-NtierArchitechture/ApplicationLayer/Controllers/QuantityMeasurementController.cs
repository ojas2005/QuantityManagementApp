using System;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entities;

namespace ApplicationLayer.Controllers
{
    /// <summary>
    /// Controller for handling quantity measurement operations
    /// </summary>
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void DemonstrateEquality()
        {
            Console.WriteLine("\n EQUALITY DEMONSTRATION \n");

            // Length equality
            var feet1 = new QuantityDTO(1.0, "ft", "Length");
            var feet2 = new QuantityDTO(1.0, "ft", "Length");
            DisplayResult(_service.CompareQuantities(feet1, feet2), "1 ft == 1 ft");

            var inches = new QuantityDTO(12.0, "in", "Length");
            DisplayResult(_service.CompareQuantities(feet1, inches), "1 ft == 12 in");

            var yards = new QuantityDTO(1.0, "yd", "Length");
            var feet3 = new QuantityDTO(3.0, "ft", "Length");
            DisplayResult(_service.CompareQuantities(yards, feet3), "1 yd == 3 ft");

            var cm = new QuantityDTO(1.0, "cm", "Length");
            var inch = new QuantityDTO(0.393701, "in", "Length");
            DisplayResult(_service.CompareQuantities(cm, inch), "1 cm == 0.393701 in");

            // Weight equality
            var kg = new QuantityDTO(1.0, "kg", "Weight");
            var gram = new QuantityDTO(1000.0, "g", "Weight");
            DisplayResult(_service.CompareQuantities(kg, gram), "1 kg == 1000 g");

            // Volume equality
            var litre = new QuantityDTO(1.0, "L", "Volume");
            var ml = new QuantityDTO(1000.0, "mL", "Volume");
            DisplayResult(_service.CompareQuantities(litre, ml), "1 L == 1000 mL");

            // Temperature equality
            var celsius = new QuantityDTO(0.0, "°C", "Temperature");
            var fahrenheit = new QuantityDTO(32.0, "°F", "Temperature");
            DisplayResult(_service.CompareQuantities(celsius, fahrenheit), "0°C == 32°F");
        }

        public void DemonstrateConversion()
        {
            Console.WriteLine("\n CONVERSION DEMONSTRATION \n");

            // Length conversions
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(1.0, "ft", "Length"), "in"), "1 ft to inches");
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(1.0, "yd", "Length"), "ft"), "1 yd to feet");
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(1.0, "cm", "Length"), "in"), "1 cm to inches");

            // Weight conversions
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(1.0, "kg", "Weight"), "g"), "1 kg to grams");
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(1.0, "kg", "Weight"), "lb"), "1 kg to pounds");

            // Volume conversions
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(1.0, "L", "Volume"), "mL"), "1 L to mL");
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(1.0, "gal", "Volume"), "L"), "1 gal to L");

            // Temperature conversions
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(100.0, "°C", "Temperature"), "°F"), "100°C to °F");
            DisplayResult(_service.ConvertQuantity(new QuantityDTO(32.0, "°F", "Temperature"), "°C"), "32°F to °C");
        }

        public void DemonstrateAddition()
        {
            Console.WriteLine("\n ADDITION DEMONSTRATION \n");

            // Length addition
            var feet1 = new QuantityDTO(1.0, "ft", "Length");
            var feet2 = new QuantityDTO(2.0, "ft", "Length");
            DisplayResult(_service.AddQuantities(feet1, feet2), "1 ft + 2 ft");

            var feet = new QuantityDTO(1.0, "ft", "Length");
            var inches = new QuantityDTO(12.0, "in", "Length");
            DisplayResult(_service.AddQuantities(feet, inches), "1 ft + 12 in");
            DisplayResult(_service.AddQuantities(feet, inches, "in"), "1 ft + 12 in (result in inches)");

            var yard = new QuantityDTO(1.0, "yd", "Length");
            var feet3 = new QuantityDTO(3.0, "ft", "Length");
            DisplayResult(_service.AddQuantities(yard, feet3), "1 yd + 3 ft");

            // Weight addition
            var kg = new QuantityDTO(1.0, "kg", "Weight");
            var gram = new QuantityDTO(500.0, "g", "Weight");
            DisplayResult(_service.AddQuantities(kg, gram), "1 kg + 500 g");

            // Volume addition
            var litre = new QuantityDTO(1.0, "L", "Volume");
            var ml = new QuantityDTO(500.0, "mL", "Volume");
            DisplayResult(_service.AddQuantities(litre, ml), "1 L + 500 mL");
        }

        public void DemonstrateSubtraction()
        {
            Console.WriteLine("\n SUBTRACTION DEMONSTRATION \n");

            var feet10 = new QuantityDTO(10.0, "ft", "Length");
            var inches6 = new QuantityDTO(6.0, "in", "Length");
            DisplayResult(_service.SubtractQuantities(feet10, inches6), "10 ft - 6 in");

            var kg10 = new QuantityDTO(10.0, "kg", "Weight");
            var g5000 = new QuantityDTO(5000.0, "g", "Weight");
            DisplayResult(_service.SubtractQuantities(kg10, g5000), "10 kg - 5000 g");

            var litre5 = new QuantityDTO(5.0, "L", "Volume");
            var ml500 = new QuantityDTO(500.0, "mL", "Volume");
            DisplayResult(_service.SubtractQuantities(litre5, ml500, "mL"), "5 L - 500 mL (result in mL)");
        }

        public void DemonstrateDivision()
        {
            Console.WriteLine("\n DIVISION DEMONSTRATION \n");

            var feet10 = new QuantityDTO(10.0, "ft", "Length");
            var feet2 = new QuantityDTO(2.0, "ft", "Length");
            DisplayResult(_service.DivideQuantities(feet10, feet2), "10 ft ÷ 2 ft");

            var inches24 = new QuantityDTO(24.0, "in", "Length");
            var feet2again = new QuantityDTO(2.0, "ft", "Length");
            DisplayResult(_service.DivideQuantities(inches24, feet2again), "24 in ÷ 2 ft");

            var g2000 = new QuantityDTO(2000.0, "g", "Weight");
            var kg1 = new QuantityDTO(1.0, "kg", "Weight");
            DisplayResult(_service.DivideQuantities(g2000, kg1), "2000 g ÷ 1 kg");
        }

        public void DemonstrateTemperatureUnsupportedOperations()
        {
            Console.WriteLine("\n TEMPERATURE UNSUPPORTED OPERATIONS \n");

            var celsius100 = new QuantityDTO(100.0, "°C", "Temperature");
            var celsius50 = new QuantityDTO(50.0, "°C", "Temperature");

            DisplayResult(_service.AddQuantities(celsius100, celsius50), "100°C + 50°C");
            DisplayResult(_service.SubtractQuantities(celsius100, celsius50), "100°C - 50°C");
            DisplayResult(_service.DivideQuantities(celsius100, celsius50), "100°C ÷ 50°C");
        }

        public void DemonstrateCrossCategoryPrevention()
        {
            Console.WriteLine("\n CROSS-CATEGORY PREVENTION \n");

            var length = new QuantityDTO(10.0, "ft", "Length");
            var weight = new QuantityDTO(5.0, "kg", "Weight");
            var volume = new QuantityDTO(2.0, "L", "Volume");
            var temp = new QuantityDTO(100.0, "°C", "Temperature");

            DisplayResult(_service.CompareQuantities(length, weight), "10 ft == 5 kg?");
            DisplayResult(_service.CompareQuantities(length, volume), "10 ft == 2 L?");
            DisplayResult(_service.CompareQuantities(temp, weight), "100°C == 5 kg?");
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("  QUANTITY MEASUREMENT APPLICATION");
            Console.WriteLine("");
            Console.WriteLine("Press 1 to Equality Demonstrations");
            Console.WriteLine("Press 2 to Conversion Demonstrations");
            Console.WriteLine("Press 3 to Addition Demonstrations");
            Console.WriteLine("Press 4 to Subtraction Demonstrations");
            Console.WriteLine("Press 5 to Division Demonstrations");
            Console.WriteLine("Press 6 to Temperature Unsupported Operations");
            Console.WriteLine("Press 7 to Cross-Category Prevention");
            Console.WriteLine("Press 8 to Run All Demonstrations");
            Console.WriteLine("Press 0 to Exit");
            Console.WriteLine("");
            Console.Write("Enter your choice: ");
        }

        public void RunSelectedDemonstration(int choice)
        {
            switch (choice)
            {
                case 1:
                    DemonstrateEquality();
                    break;
                case 2:
                    DemonstrateConversion();
                    break;
                case 3:
                    DemonstrateAddition();
                    break;
                case 4:
                    DemonstrateSubtraction();
                    break;
                case 5:
                    DemonstrateDivision();
                    break;
                case 6:
                    DemonstrateTemperatureUnsupportedOperations();
                    break;
                case 7:
                    DemonstrateCrossCategoryPrevention();
                    break;
                case 8:
                    RunAllDemonstrations();
                    break;
                case 0:
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

        private void RunAllDemonstrations()
        {
            DemonstrateEquality();
            DemonstrateConversion();
            DemonstrateAddition();
            DemonstrateSubtraction();
            DemonstrateDivision();
            DemonstrateTemperatureUnsupportedOperations();
            DemonstrateCrossCategoryPrevention();
        }

        private void DisplayResult(QuantityMeasurementEntity entity, string description)
        {
            Console.WriteLine($"{description}:");
            Console.WriteLine($"  {entity}");
            Console.WriteLine();
        }
    }
}