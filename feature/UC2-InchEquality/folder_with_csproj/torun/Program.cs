using System;

public class QuantityMeasurementApp
{
    public class Feet : IEquatable<Feet>
    {
        private readonly double _value;
        private const double Tolerance = 0.0001;
        public Feet(double value)
        {
            _value = value;
        }
        public double Value => _value;
        public override bool Equals(object obj)
        {
            if (obj is null || this.GetType() != obj.GetType())
                return false;

            return Equals((Feet)obj);
        }
        public bool Equals(Feet other)
        {
            if (other is null) {
                return false;
            }
            return Math.Abs(_value - other._value) < Tolerance;
        }
        public override int GetHashCode(){
           return _value.GetHashCode();
        }
        public override string ToString() 
        {
            return $"{_value} ft";
        } 
        public static bool operator ==(Feet left, Feet right)
        {
            if (left is null)
            {
                return right is null;
            }
            return left.Equals(right);
        }
        public static bool operator !=(Feet left, Feet right) {
            return !(left == right);
        }
    }
    public class Inches : IEquatable<Inches>
    {
        private readonly double _value;
        private const double Tolerance = 0.0001;

        public Inches(double value)
        {
            _value = value;
        }

        public double Value {
            get{ return _value;}
        } 

        public override bool Equals(object obj)
        {
            if (obj is null || this.GetType() != obj.GetType())
                return false;

            return Equals((Inches)obj);
        }
        public bool Equals(Inches other)
        {
            if (other is null) return false;
            return Math.Abs(_value - other._value) < Tolerance;
        }
        public override int GetHashCode(){
            return  _value.GetHashCode();
        }
        public override string ToString() {
            return $"{_value} in";
        } 
        public static bool operator ==(Inches left, Inches right)
        {
            if (left is null)
            {
                return right is null;
            }
            return left.Equals(right);
        }
        public static bool operator !=(Inches left,Inches right) //this method ensures that != compares the measurement values,not the object references.
        {
            return !(left==right);
        }
    }
    public static class QuantityMeasurementService
    {
        public static bool CompareFeetEquality(double feetValue1, double feetValue2)
        {
            try
            {
                return new Feet(feetValue1).Equals(new Feet(feetValue2));
            }
            catch { return false; }
        }
        public static bool CompareInchesEquality(double inchesValue1, double inchesValue2)
        {
            try
            {
                return new Inches(inchesValue1).Equals(new Inches(inchesValue2));
            }
            catch { return false; }
        }
        public static bool ValidateMeasurementValue(double totalUnits)
        {
            return !double.IsNaN(totalUnits) && !double.IsInfinity(totalUnits) && totalUnits >= 0;
        }
        public static double ConvertUnits(double feetValue)
        {
            const double FeetToInchesConversionFactor = 12.0;
            if (!ValidateMeasurementValue(feetValue))
                throw new ArgumentException("invalid measurement value provided");

            return feetValue*FeetToInchesConversionFactor;
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Quantity Measurement App\n");
        var feet1 = new Feet(1.0);
        var feet2 = new Feet(1.0);
        Console.WriteLine($"Feet Test: {feet1} equals {feet2}? {feet1.Equals(feet2)}");
        var inches1 = new Inches(1.0);
        var inches2 = new Inches(1.0);
        Console.WriteLine($"Inches Test: {inches1} equals {inches2}? {inches1.Equals(inches2)}");
        double convertedInches = QuantityMeasurementService.ConvertUnits(1.0);
        Console.WriteLine($"1 ft to inches: {convertedInches} in");
        bool isValidValue = QuantityMeasurementService.ValidateMeasurementValue(10.5);
        Console.WriteLine($"Is 10.5 valid? {isValidValue}\n");
        Console.WriteLine("Application Execution Completed");
    }
}