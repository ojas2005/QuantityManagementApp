using System;

public class QuantityMeasurementApp
{
    public enum LengthUnit
    {
        Feet,Inch
    }
    public class QuantityLength : IEquatable<QuantityLength>
    {
        private const double InchToFeetConversionFactor = 1.0/12.0;
        private const double Tolerance = 0.0001;
        private readonly double _value;
        private readonly LengthUnit _unit;
        public QuantityLength(double value, LengthUnit unit)
        {
            if (!ValidateMeasurementValue(value))
                throw new ArgumentException("provided value is not valid for measurement");
            _value = value;
            _unit = unit;
        }
        public double Value => _value;
        public LengthUnit Unit => _unit;
        private static bool ValidateMeasurementValue(double totalUnits)
        {
            if(!double.IsNaN(totalUnits) && !double.IsInfinity(totalUnits) && totalUnits >= 0)
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
                _ => throw new InvalidOperationException($"Cannont convert from the unit-{_unit}")
            };
        }
        public override bool Equals(object obj)
        {
            if (obj is null || this.GetType() != obj.GetType())
                return false;

            return Equals((QuantityLength)obj);
        }
        public bool Equals(QuantityLength other)
        {
            if (other is null)
                return false;

            double thisValueInFeet = this.ConvertUnitsToFeet();
            double otherValueInFeet = other.ConvertUnitsToFeet();

            return Math.Abs(thisValueInFeet-otherValueInFeet)<Tolerance;
        }
        public override int GetHashCode()
        {
            return ConvertUnitsToFeet().GetHashCode();
        }
        public override string ToString()
        {
            string unitLabel = _unit switch
            {
                LengthUnit.Feet => "ft",
                LengthUnit.Inch => "in",
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
            catch
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
            catch
            {
                return false;
            }
        }

        public static bool ValidateMeasurementValue(double totalUnits)
        {
            return !double.IsNaN(totalUnits) && !double.IsInfinity(totalUnits) && totalUnits >= 0;
        }

        public static double ConvertUnits(double feetValue)
        {
            const double FeetToInchesConversionFactor = 12.0;

            if (!ValidateMeasurementValue(feetValue))
                throw new ArgumentException("provided value is not valid for measurement");

            return feetValue*FeetToInchesConversionFactor;
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Quantity Measurement App\n");
        var feet1 = new QuantityLength(1.0,LengthUnit.Feet);
        var feet2 = new QuantityLength(1.0,LengthUnit.Feet);
        Console.WriteLine($"feet testing: {feet1} equals {feet2}? {feet1.Equals(feet2)}");
        var inches1 = new QuantityLength(1.0,LengthUnit.Inch);
        var inches2 = new QuantityLength(1.0,LengthUnit.Inch);
        Console.WriteLine($"inches testing: {inches1} equals {inches2}? {inches1.Equals(inches2)}");
        var feet = new QuantityLength(1.0,LengthUnit.Feet);
        var inches = new QuantityLength(12.0,LengthUnit.Inch);
        Console.WriteLine($"testing on different units: {feet} equals {inches}? {feet.Equals(inches)}");
        double convertedInches = QuantityMeasurementService.ConvertUnits(1.0);
        Console.WriteLine($"converting 1 ft to inches = {convertedInches}inches");
        bool isValidValue = QuantityMeasurementService.ValidateMeasurementValue(10.5);
        Console.WriteLine($"Is 10.5 valid? {isValidValue}\n");
        Console.WriteLine("Application Execution Completed");
    }
}
