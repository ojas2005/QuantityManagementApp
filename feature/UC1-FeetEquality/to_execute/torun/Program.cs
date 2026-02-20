using System;

public class QuantityMeasurementApp
{
    public class Feet : IEquatable<Feet>
    {
        private readonly double _value;
        private const double Tolerance = 0.0001; //added tolerance so that if there is very minor difference then we can treat as equal.
        public Feet(double value)
        {
            _value = value;
        }
        public double Value => _value;
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (this.GetType()!=obj.GetType())
                return false;
            return Equals((Feet)obj);
        }
        public bool Equals(Feet other)
        {
            if (other is null)
                return false;
            return Math.Abs(_value-other._value)<Tolerance;
        }
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
        public override string ToString()
        {
            return $"{_value} ft";
        }
        public static bool operator ==(Feet left, Feet right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }
        public static bool operator !=(Feet left, Feet right)
        {
            return !(left == right);
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Quantity Measurement App UC-1 Feet Equality");

        //testing for same values
        Feet feet1 = new Feet(1.0);
        Feet feet2 = new Feet(1.0);
        Console.WriteLine($"");
        Console.WriteLine($"  {feet1} equals {feet2}? answer:- {feet1.Equals(feet2)}");
        Console.WriteLine();
        
        //testing for different values
        Feet feet3 = new Feet(1.0);
        Feet feet4 = new Feet(2.0);
        Console.WriteLine($"");
        Console.WriteLine($"  {feet3} equals {feet4}? answer:- {feet3.Equals(feet4)}");
        Console.WriteLine();

        //trying to compare with null values
        Feet feet5 = new Feet(1.0);
        Console.WriteLine($"");
        Console.WriteLine($"  {feet5} equals null? answer:- {feet5.Equals(null)}");
        Console.WriteLine();

        //testing for same reference
        Feet feet6 = new Feet(3.5);
        Console.WriteLine($"");
        Console.WriteLine($"  {feet6} equals with its own reference? answer:- {feet6.Equals(feet6)}");
        Console.WriteLine();

        //toleration for small difference
        Feet feet7 = new Feet(5.0);
        Feet feet8 = new Feet(5.00001);
        Console.WriteLine($"");
        Console.WriteLine($"  {feet7} equals {feet8}? answer:- {feet7.Equals(feet8)}");
    }
}