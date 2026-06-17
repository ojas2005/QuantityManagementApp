using System;

public class QuantityMeasurementApp
{
    public enum LengthUnit
    {
        Feet,Inch,Yards,Centimeters
    }

    public class QuantityLength : IEquatable<QuantityLength>
    {
        private const double InchToFeetConversionFactor = 1.0/12.0;
        private const double YardsToFeetConversionFactor = 3.0;
        private const double CentimetersToFeetConversionFactor = 0.0328084;
        private const double Tolerance = 0.0001;
        private readonly double _value;
        private readonly LengthUnit _unit;

        public QuantityLength(double value,LengthUnit unit)
        {
            if (!ValidateMeasurementValue(value))
                throw new ArgumentException("provided value is not valid for measurement value must be finite and non negative");
            _value = value;
            _unit = unit;
        }
        public double Value => _value;
        public LengthUnit Unit => _unit;
        private static bool ValidateMeasurementValue(double value)
        {
            if((!double.IsNaN(value) && !double.IsInfinity(value) && value >= 0)==true)
            {
                return true;
            }
            return false;
        }
        private double ConvertUnitsToFeet()
        {
            return _unit switch
            {
                LengthUnit.Feet => _value*1.0,
                LengthUnit.Inch => _value*InchToFeetConversionFactor,
                LengthUnit.Yards => _value*YardsToFeetConversionFactor,
                LengthUnit.Centimeters => _value*CentimetersToFeetConversionFactor,
                _ => throw new InvalidOperationException($"cannot convert from the unit:- {_unit}")
            };
        }
        public double ConvertTo(LengthUnit targetUnit)
        {
            double valueInFeet = ConvertUnitsToFeet();
            double convertedValue = targetUnit switch
            {
                LengthUnit.Feet => valueInFeet,
                LengthUnit.Inch => valueInFeet / InchToFeetConversionFactor,
                LengthUnit.Yards => valueInFeet / YardsToFeetConversionFactor,
                LengthUnit.Centimeters => valueInFeet / CentimetersToFeetConversionFactor,
                _ => throw new InvalidOperationException($"cannot convert to the unit:- {targetUnit}")
            };
            return convertedValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is null||this.GetType()!=obj.GetType())
                return false;
            return Equals((QuantityLength)obj);
        }
        public bool Equals(QuantityLength other)
        {
            if (other is null)
                return false;
            double thisValueInFeet = this.ConvertUnitsToFeet();
            double otherValueInFeet = other.ConvertUnitsToFeet();
            return Math.Abs(thisValueInFeet - otherValueInFeet) < Tolerance;
        }
        public override int GetHashCode()
        {
            double valueInFeet = ConvertUnitsToFeet();
            double normalized = Math.Round(valueInFeet, 4);
            return normalized.GetHashCode();
        }
        public override string ToString()
        {
            string unitLabel = _unit switch
            {
                LengthUnit.Feet => "ft",
                LengthUnit.Inch => "in",
                LengthUnit.Yards => "yd",
                LengthUnit.Centimeters => "cm",
                _ => _unit.ToString()
            };
            return $"{_value} {unitLabel}";
        }
        public static bool operator ==(QuantityLength left, QuantityLength right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }
        public static bool operator !=(QuantityLength left, QuantityLength right)
        {
            return !(left == right);
        }
    }
    public static class QuantityMeasurementService
    {
        public static bool CompareFeetEquality(double feetValue1, double feetValue2)
        {
            try
            {
                var feet1 = new QuantityLength(feetValue1, LengthUnit.Feet);
                var feet2 = new QuantityLength(feetValue2, LengthUnit.Feet);
                return feet1.Equals(feet2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool CompareInchesEquality(double inchesValue1, double inchesValue2)
        {
            try
            {
                var inches1 = new QuantityLength(inchesValue1, LengthUnit.Inch);
                var inches2 = new QuantityLength(inchesValue2, LengthUnit.Inch);
                return inches1.Equals(inches2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool CompareYardsEquality(double yardsValue1, double yardsValue2)
        {
            try
            {
                var yards1 = new QuantityLength(yardsValue1, LengthUnit.Yards);
                var yards2 = new QuantityLength(yardsValue2, LengthUnit.Yards);
                return yards1.Equals(yards2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool CompareCentimetersEquality(double centimetersValue1, double centimetersValue2)
        {
            try
            {
                var centimeters1 = new QuantityLength(centimetersValue1, LengthUnit.Centimeters);
                var centimeters2 = new QuantityLength(centimetersValue2, LengthUnit.Centimeters);
                return centimeters1.Equals(centimeters2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool CompareQuantityEquality(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            try
            {
                var quantity1 = new QuantityLength(value1, unit1);
                var quantity2 = new QuantityLength(value2, unit2);
                return quantity1.Equals(quantity2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool ValidateMeasurementValue(double value)
        {
            if((!double.IsNaN(value) && !double.IsInfinity(value) && value >= 0)==true)
            {
                return true;
            }
            return false;
        }
        public static double Convert(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
        {
            if (!ValidateMeasurementValue(value))
                throw new ArgumentException("provided value is not valid for measurement value must be finite and non negative");
            var quantity = new QuantityLength(value, sourceUnit);
            return quantity.ConvertTo(targetUnit);
        }
        public static double ConvertUnits(double feetValue)
        {
            const double FeetToInchesConversionFactor = 12.0;
            if (!ValidateMeasurementValue(feetValue))
                throw new ArgumentException("provided value is not valid for measurement value must be finite and non negative.");
            return feetValue*FeetToInchesConversionFactor;
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Quantity Measurement Application\n");

        //unit equality demo representation
        var feet1 = new QuantityLength(1.0, LengthUnit.Feet);
        var feet2 = new QuantityLength(1.0, LengthUnit.Feet);
        Console.WriteLine($"Feet testing: {feet1} equals {feet2}? {feet1.Equals(feet2)}");

        var inches1 = new QuantityLength(1.0, LengthUnit.Inch);
        var inches2 = new QuantityLength(1.0, LengthUnit.Inch);
        Console.WriteLine($"inches testing: {inches1} equals {inches2}? {inches1.Equals(inches2)}");

        var feet = new QuantityLength(1.0, LengthUnit.Feet);
        var inches = new QuantityLength(12.0, LengthUnit.Inch);
        Console.WriteLine($"cross unit testing: {feet} equals {inches}? {feet.Equals(inches)}");

        var yard1 = new QuantityLength(1.0, LengthUnit.Yards);
        var yard2 = new QuantityLength(1.0, LengthUnit.Yards);
        Console.WriteLine($"yard to yard: {yard1} equals {yard2}? {yard1.Equals(yard2)}");

        var yard3Feet = new QuantityLength(1.0, LengthUnit.Yards);
        var feet3 = new QuantityLength(3.0, LengthUnit.Feet);
        Console.WriteLine($"yard to feet: {yard3Feet} equals {feet3}? {yard3Feet.Equals(feet3)}");

        var yard36Inches = new QuantityLength(1.0, LengthUnit.Yards);
        var inches36 = new QuantityLength(36.0, LengthUnit.Inch);
        Console.WriteLine($"Yard to inches: {yard36Inches} equals {inches36}? {yard36Inches.Equals(inches36)}");

        var cm1 = new QuantityLength(1.0, LengthUnit.Centimeters);
        var cm2 = new QuantityLength(1.0, LengthUnit.Centimeters);
        Console.WriteLine($"cm to cm: {cm1} equals {cm2}? {cm1.Equals(cm2)}");

        var cm1ToInches = new QuantityLength(1.0, LengthUnit.Centimeters);
        var inches0393701 = new QuantityLength(0.393701, LengthUnit.Inch);
        Console.WriteLine($"cm to inches: {cm1ToInches} equals {inches0393701}? {cm1ToInches.Equals(inches0393701)}");

        //unit conversion demo representation

        //basic unit conversions
        double feet2Inches = QuantityMeasurementService.Convert(1.0, LengthUnit.Feet, LengthUnit.Inch);
        Console.WriteLine($"convert 1 foot to inches: {feet2Inches} inches");

        double yards2Feet = QuantityMeasurementService.Convert(3.0, LengthUnit.Yards, LengthUnit.Feet);
        Console.WriteLine($"convert 3 yards to feet: {yards2Feet} feet");

        double inches2Yards = QuantityMeasurementService.Convert(36.0, LengthUnit.Inch, LengthUnit.Yards);
        Console.WriteLine($"convert 36 inches to yards: {inches2Yards} yards");

        //cross unit conversions
        double cm2Inches = QuantityMeasurementService.Convert(1.0, LengthUnit.Centimeters, LengthUnit.Inch);
        Console.WriteLine($"convert 1 centimeter to inches: {cm2Inches:F6} inches");

        double feet2Cm = QuantityMeasurementService.Convert(1.0, LengthUnit.Feet, LengthUnit.Centimeters);
        Console.WriteLine($"convert 1 foot to centimeters: {feet2Cm:F6} centimeters");

        //zero value conversion
        double zero2Inches = QuantityMeasurementService.Convert(0.0, LengthUnit.Feet, LengthUnit.Inch);
        Console.WriteLine($"convert 0 feet to inches: {zero2Inches} inches");

        //same unit conversion
        double feet2Feet = QuantityMeasurementService.Convert(5.0, LengthUnit.Feet, LengthUnit.Feet);
        Console.WriteLine($"convert 5 feet to feet: {feet2Feet} feet");

        //negative value demonstration
        try
        {
            double invalidConversion = QuantityMeasurementService.Convert(-1.0, LengthUnit.Feet, LengthUnit.Inch);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"negative value error: {ex.Message}");
        }

        //service methods demo representation
        Console.WriteLine("\n");

        bool isValidValue = QuantityMeasurementService.ValidateMeasurementValue(10.5);
        Console.WriteLine($"is 10.5 a valid measurement? {isValidValue}");

        bool yardsEqual = QuantityMeasurementService.CompareYardsEquality(2.0, 2.0);
        Console.WriteLine($"are 2.0 yards equal to 2.0 yards? {yardsEqual}");

        bool cmEqual = QuantityMeasurementService.CompareCentimetersEquality(1.0, 1.0);
        Console.WriteLine($"is 1.0 cm equal to 1.0 cm? {cmEqual}");

        bool crossUnitEqual = QuantityMeasurementService.CompareQuantityEquality(1.0, LengthUnit.Yards, 3.0, LengthUnit.Feet);
        Console.WriteLine($"is 1.0 yard equal to 3.0 feet? {crossUnitEqual}");

        //Demonstration of ConvertTo instance method
        Console.WriteLine("\n");
        var quantityLength = new QuantityLength(2.0, LengthUnit.Yards);
        double yards2InchesViaInstance = quantityLength.ConvertTo(LengthUnit.Inch);
        Console.WriteLine($"2 yards converted to inches:-{yards2InchesViaInstance} inches");

        Console.WriteLine("\nApplication Execution Completed\n");
    }
}
