using System;

public class QuantityMeasurementApp
{
    public class QuantityLength : IEquatable<QuantityLength>
    {
        private const double Tolerance = 0.0001;
        
        public static double InchToFeetConversionFactorValue => LengthUnit.Inch.GetConversionFactor();
        public static double YardsToFeetConversionFactorValue => LengthUnit.Yards.GetConversionFactor();
        public static double CentimetersToFeetConversionFactorValue => LengthUnit.Centimeters.GetConversionFactor();
        
        private readonly double _value;
        private readonly LengthUnit _unit;

        public QuantityLength(double value, LengthUnit unit)
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
            return !double.IsNaN(value) && !double.IsInfinity(value) && value >= 0;
        }

        private static double ConvertToBase(double value, LengthUnit unit)
        {
            return unit.ConvertToBaseUnit(value);
        }

        private static double ConvertFromBase(double baseValue, LengthUnit unit)
        {
            return unit.ConvertFromBaseUnit(baseValue);
        }

        private double ConvertUnitsToFeet()
        {
            return _unit.ConvertToBaseUnit(_value);
        }

        public double ConvertTo(LengthUnit targetUnit)
        {
            double valueInFeet = ConvertUnitsToFeet();
            return targetUnit.ConvertFromBaseUnit(valueInFeet);
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
            return $"{_value} {_unit.GetLabel()}";
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

        public static QuantityLength Add(QuantityLength first, QuantityLength second)
        {
            if (first is null || second is null)
                throw new ArgumentNullException(first is null ? nameof(first) : nameof(second), "operands cannot be null");
            
            return AddWithTargetUnit(first, second, first._unit);
        }

        public static QuantityLength Add(QuantityLength first, QuantityLength second, LengthUnit? targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentNullException(first is null ? nameof(first) : nameof(second), "operands cannot be null");
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit), "target unit cannot be null");
            
            return AddWithTargetUnit(first, second, targetUnit.Value);
        }

        private static QuantityLength AddWithTargetUnit(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
        {
            double firstInBase = ConvertToBase(first.Value, first.Unit);
            double secondInBase = ConvertToBase(second.Value, second.Unit);
            double sumInBase = firstInBase + secondInBase;
            double resultInTarget = ConvertFromBase(sumInBase, targetUnit);

            return new QuantityLength(resultInTarget, targetUnit);
        }

    }
    public static class QuantityMeasurementService
    {
        public static bool CompareFeetEquality(double feetValue1,double feetValue2)
        {
            try
            {
                var feet1 = new QuantityLength(feetValue1,LengthUnit.Feet);
                var feet2 = new QuantityLength(feetValue2,LengthUnit.Feet);
                return feet1.Equals(feet2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool CompareInchesEquality(double inchesValue1,double inchesValue2)
        {
            try
            {
                var inches1 = new QuantityLength(inchesValue1,LengthUnit.Inch);
                var inches2 = new QuantityLength(inchesValue2,LengthUnit.Inch);
                return inches1.Equals(inches2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool CompareYardsEquality(double yardsValue1,double yardsValue2)
        {
            try
            {
                var yards1 = new QuantityLength(yardsValue1,LengthUnit.Yards);
                var yards2 = new QuantityLength(yardsValue2,LengthUnit.Yards);
                return yards1.Equals(yards2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool CompareCentimetersEquality(double centimetersValue1,double centimetersValue2)
        {
            try
            {
                var centimeters1 = new QuantityLength(centimetersValue1,LengthUnit.Centimeters);
                var centimeters2 = new QuantityLength(centimetersValue2,LengthUnit.Centimeters);
                return centimeters1.Equals(centimeters2);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        public static bool CompareQuantityEquality(double value1,LengthUnit unit1,double value2,LengthUnit unit2)
        {
            try
            {
                var quantity1 = new QuantityLength(value1,unit1);
                var quantity2 = new QuantityLength(value2,unit2);
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
        public static double Convert(double value,LengthUnit sourceUnit,LengthUnit targetUnit)
        {
            if (!ValidateMeasurementValue(value))
                throw new ArgumentException("provided value is not valid for measurement value must be finite and non negative");
            var quantity = new QuantityLength(value,sourceUnit);
            return quantity.ConvertTo(targetUnit);
        }
        public static double ConvertUnits(double feetValue)
        {
            const double FeetToInchesConversionFactor = 12.0;
            if (!ValidateMeasurementValue(feetValue))
                throw new ArgumentException("provided value is not valid for measurement value must be finite and non negative.");
            return feetValue*FeetToInchesConversionFactor;
        }
        public static QuantityLength Add(QuantityLength first,QuantityLength second)
        {
            if (first is null || second is null)
                throw new ArgumentNullException(first is null ? nameof(first) : nameof(second),"operands cannot be null");
            
            return QuantityLength.Add(first,second);
        }
        public static QuantityLength Add(QuantityLength first,QuantityLength second,LengthUnit? targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentNullException(first is null ? nameof(first) : nameof(second),"operands cannot be null");
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit),"target unit cannot be null");
            
            return QuantityLength.Add(first,second,targetUnit.Value);
        }
        public static QuantityLength Add(double firstValue,LengthUnit firstUnit,double secondValue,LengthUnit? secondUnit,LengthUnit? targetUnit)
        {
            if (!ValidateMeasurementValue(firstValue) || 
                !ValidateMeasurementValue(secondValue))
                throw new ArgumentException(
                    "provided values are not valid for measurement values must be finite and non negative");

            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit),
                    "target unit cannot be null");

            var firstQuantity = new QuantityLength(firstValue,firstUnit);
            var secondQuantity = new QuantityLength(secondValue,secondUnit.Value);

            return QuantityLength.Add(firstQuantity,secondQuantity,targetUnit.Value);
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Quantity Measurement Application\n");

        //unit equality demo representation
        var feet1 = new QuantityLength(1.0,LengthUnit.Feet);
        var feet2 = new QuantityLength(1.0,LengthUnit.Feet);
        Console.WriteLine($"feet testing: {feet1} equals {feet2}? {feet1.Equals(feet2)}");

        var inches1 = new QuantityLength(1.0,LengthUnit.Inch);
        var inches2 = new QuantityLength(1.0,LengthUnit.Inch);
        Console.WriteLine($"inches testing: {inches1} equals {inches2}? {inches1.Equals(inches2)}");

        var feet = new QuantityLength(1.0,LengthUnit.Feet);
        var inches = new QuantityLength(12.0,LengthUnit.Inch);
        Console.WriteLine($"cross unit testing: {feet} equals {inches}? {feet.Equals(inches)}");

        var yard1 = new QuantityLength(1.0,LengthUnit.Yards);
        var yard2 = new QuantityLength(1.0,LengthUnit.Yards);
        Console.WriteLine($"yard to yard: {yard1} equals {yard2}? {yard1.Equals(yard2)}");

        var yard3Feet = new QuantityLength(1.0,LengthUnit.Yards);
        var feet3 = new QuantityLength(3.0,LengthUnit.Feet);
        Console.WriteLine($"yard to feet: {yard3Feet} equals {feet3}? {yard3Feet.Equals(feet3)}");

        var yard36Inches = new QuantityLength(1.0,LengthUnit.Yards);
        var inches36 = new QuantityLength(36.0,LengthUnit.Inch);
        Console.WriteLine($"yard to inches: {yard36Inches} equals {inches36}? {yard36Inches.Equals(inches36)}");

        var cm1 = new QuantityLength(1.0,LengthUnit.Centimeters);
        var cm2 = new QuantityLength(1.0,LengthUnit.Centimeters);
        Console.WriteLine($"cm to cm: {cm1} equals {cm2}? {cm1.Equals(cm2)}");

        var cm1ToInches = new QuantityLength(1.0,LengthUnit.Centimeters);
        var inches0393701 = new QuantityLength(0.393701,LengthUnit.Inch);
        Console.WriteLine($"cm to inches: {cm1ToInches} equals {inches0393701}? {cm1ToInches.Equals(inches0393701)}");

        //unit conversion demo representation

        //basic unit conversions
        double feet2Inches = QuantityMeasurementService.Convert(1.0,LengthUnit.Feet,LengthUnit.Inch);
        Console.WriteLine($"convert 1 foot to inches: {feet2Inches} inches");

        double yards2Feet = QuantityMeasurementService.Convert(3.0,LengthUnit.Yards,LengthUnit.Feet);
        Console.WriteLine($"convert 3 yards to feet: {yards2Feet} feet");

        double inches2Yards = QuantityMeasurementService.Convert(36.0,LengthUnit.Inch,LengthUnit.Yards);
        Console.WriteLine($"convert 36 inches to yards: {inches2Yards} yards");

        //cross unit conversions
        double cm2Inches = QuantityMeasurementService.Convert(1.0,LengthUnit.Centimeters,LengthUnit.Inch);
        Console.WriteLine($"convert 1 centimeter to inches: {cm2Inches:F6} inches");

        double feet2Cm = QuantityMeasurementService.Convert(1.0,LengthUnit.Feet,LengthUnit.Centimeters);
        Console.WriteLine($"convert 1 foot to centimeters: {feet2Cm:F6} centimeters");

        //zero value conversion
        double zero2Inches = QuantityMeasurementService.Convert(0.0,LengthUnit.Feet,LengthUnit.Inch);
        Console.WriteLine($"convert 0 feet to inches: {zero2Inches} inches");

        //same unit conversion
        double feet2Feet = QuantityMeasurementService.Convert(5.0,LengthUnit.Feet,LengthUnit.Feet);
        Console.WriteLine($"convert 5 feet to feet: {feet2Feet} feet");

        //negative value demonstration
        try
        {
            double invalidConversion = QuantityMeasurementService.Convert(-1.0,LengthUnit.Feet,LengthUnit.Inch);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"negative value error: {ex.Message}");
        }

        //service methods demo representation
        Console.WriteLine("\n");

        bool isValidValue = QuantityMeasurementService.ValidateMeasurementValue(10.5);
        Console.WriteLine($"is 10.5 a valid measurement? {isValidValue}");

        bool yardsEqual = QuantityMeasurementService.CompareYardsEquality(2.0,2.0);
        Console.WriteLine($"are 2.0 yards equal to 2.0 yards? {yardsEqual}");

        bool cmEqual = QuantityMeasurementService.CompareCentimetersEquality(1.0,1.0);
        Console.WriteLine($"is 1.0 cm equal to 1.0 cm? {cmEqual}");

        bool crossUnitEqual = QuantityMeasurementService.CompareQuantityEquality(1.0,LengthUnit.Yards,3.0,LengthUnit.Feet);
        Console.WriteLine($"is 1.0 yard equal to 3.0 feet? {crossUnitEqual}");

        //Demonstration of ConvertTo instance method
        Console.WriteLine("\n");
        var quantityLength = new QuantityLength(2.0,LengthUnit.Yards);
        double yards2InchesViaInstance = quantityLength.ConvertTo(LengthUnit.Inch);
        Console.WriteLine($"2 yards converted to inches:-{yards2InchesViaInstance} inches");

        //addition functionality demonstration
        Console.WriteLine("\nAddition of Length Units:");
        
        var add1Feet = new QuantityLength(1.0,LengthUnit.Feet);
        var add2Feet = new QuantityLength(2.0,LengthUnit.Feet);
        var addResult1 = QuantityLength.Add(add1Feet,add2Feet);
        Console.WriteLine($"add({add1Feet},{add2Feet}) = {addResult1}");

        var add1FeetAgain = new QuantityLength(1.0,LengthUnit.Feet);
        var add12Inches = new QuantityLength(12.0,LengthUnit.Inch);
        var addResult2 = QuantityLength.Add(add1FeetAgain,add12Inches);
        Console.WriteLine($"add({add1FeetAgain},{add12Inches}) = {addResult2}");

        var add12InchesAgain = new QuantityLength(12.0,LengthUnit.Inch);
        var add1FeetForInches = new QuantityLength(1.0,LengthUnit.Feet);
        var addResult3 = QuantityLength.Add(add12InchesAgain,add1FeetForInches);
        Console.WriteLine($"add({add12InchesAgain},{add1FeetForInches}) = {addResult3}");

        var add1Yard = new QuantityLength(1.0,LengthUnit.Yards);
        var add3Feet = new QuantityLength(3.0,LengthUnit.Feet);
        var addResult4 = QuantityLength.Add(add1Yard,add3Feet);
        Console.WriteLine($"add({add1Yard},{add3Feet}) = {addResult4}");

        var add36Inches = new QuantityLength(36.0,LengthUnit.Inch);
        var add1YardForInches = new QuantityLength(1.0,LengthUnit.Yards);
        var addResult5 = QuantityLength.Add(add36Inches,add1YardForInches);
        Console.WriteLine($"add({add36Inches},{add1YardForInches}) = {addResult5}");

        var add2_54Cm = new QuantityLength(2.54,LengthUnit.Centimeters);
        var add1InchForCm = new QuantityLength(1.0,LengthUnit.Inch);
        var addResult6 = QuantityLength.Add(add2_54Cm,add1InchForCm);
        Console.WriteLine($"add({add2_54Cm},{add1InchForCm}) = {addResult6:F2}");

        var add5Feet = new QuantityLength(5.0,LengthUnit.Feet);
        var add0Inches = new QuantityLength(0.0,LengthUnit.Inch);
        var addResult7 = QuantityLength.Add(add5Feet,add0Inches);
        Console.WriteLine($"add({add5Feet},{add0Inches}) = {addResult7}");

        try
        {
            var serviceAdd1 = new QuantityLength(1.0,LengthUnit.Feet);
            var serviceAdd2 = new QuantityLength(12.0,LengthUnit.Inch);
            var serviceResult = QuantityMeasurementService.Add(serviceAdd1,serviceAdd2);
            Console.WriteLine($"service add({serviceAdd1},{serviceAdd2}) = {serviceResult}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"service addition error: {ex.Message}");
        }

        var customTargetResult = QuantityMeasurementService.Add(1.0,LengthUnit.Feet,12.0,LengthUnit.Inch,LengthUnit.Inch);
        Console.WriteLine($"service add(1 ft,12 in,target=in) = {customTargetResult}");

        //uc7 addition with explicit target unit specification demonstration
        Console.WriteLine("\nAddition with Explicit Target Unit Specification:");
        
        var uc7Feet = new QuantityLength(1.0,LengthUnit.Feet);
        var uc7Inches = new QuantityLength(12.0,LengthUnit.Inch);
        
        var uc7ResultFeet = QuantityLength.Add(uc7Feet,uc7Inches,LengthUnit.Feet);
        Console.WriteLine($"add({uc7Feet},{uc7Inches},target=feet) = {uc7ResultFeet}");
        
        var uc7ResultInches = QuantityLength.Add(uc7Feet,uc7Inches,LengthUnit.Inch);
        Console.WriteLine($"add({uc7Feet},{uc7Inches},target=inches) = {uc7ResultInches}");
        
        var uc7ResultYards = QuantityLength.Add(uc7Feet,uc7Inches,LengthUnit.Yards);
        Console.WriteLine($"add({uc7Feet},{uc7Inches},target=yards) = {uc7ResultYards:F2}");
        
        var uc7Yard = new QuantityLength(1.0,LengthUnit.Yards);
        var uc7FeetAgain = new QuantityLength(3.0,LengthUnit.Feet);
        
        var uc7ResultYardsFromYards = QuantityLength.Add(uc7Yard,uc7FeetAgain,LengthUnit.Yards);
        Console.WriteLine($"add({uc7Yard},{uc7FeetAgain},target=yards) = {uc7ResultYardsFromYards}");
        
        var uc7ResultFeetFromYards = QuantityLength.Add(uc7Yard,uc7FeetAgain,LengthUnit.Feet);
        Console.WriteLine($"add({uc7Yard},{uc7FeetAgain},target=feet) = {uc7ResultFeetFromYards}");
        
        var uc7Cm = new QuantityLength(2.54,LengthUnit.Centimeters);
        var uc7InchForCm = new QuantityLength(1.0,LengthUnit.Inch);
        
        var uc7ResultCm = QuantityLength.Add(uc7Cm,uc7InchForCm,LengthUnit.Centimeters);
        Console.WriteLine($"add({uc7Cm},{uc7InchForCm},target=cm) = {uc7ResultCm:F2}");
        
        var uc7Large1 = new QuantityLength(5.0,LengthUnit.Feet);
        var uc7Large2 = new QuantityLength(0.0,LengthUnit.Inch);
        
        var uc7ResultLarge = QuantityLength.Add(uc7Large1,uc7Large2,LengthUnit.Yards);
        Console.WriteLine($"add({uc7Large1},{uc7Large2},target=yards) = {uc7ResultLarge:F2}");
        
        //service method with explicit target unit
        try
        {
            var uc7Service1 = new QuantityLength(1.0,LengthUnit.Feet);
            var uc7Service2 = new QuantityLength(12.0,LengthUnit.Inch);
            var uc7ServiceResultFeet = QuantityMeasurementService.Add(uc7Service1,uc7Service2,LengthUnit.Feet);
            Console.WriteLine($"service add({uc7Service1},{uc7Service2},target=feet) = {uc7ServiceResultFeet}");
            
            var uc7ServiceResultYards = QuantityMeasurementService.Add(uc7Service1,uc7Service2,LengthUnit.Yards);
            Console.WriteLine($"service add({uc7Service1},{uc7Service2},target=yards) = {uc7ServiceResultYards:F2}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"service addition error: {ex.Message}");
        }
        
        //raw values with explicit target unit
        var uc7RawResult = QuantityMeasurementService.Add(1.0,LengthUnit.Feet,12.0,LengthUnit.Inch,LengthUnit.Yards);
        Console.WriteLine($"service add(1 ft,12 in,target=yards) = {uc7RawResult:F2}");
        
        var uc7RawResult2 = QuantityMeasurementService.Add(1.0,LengthUnit.Feet,12.0,LengthUnit.Inch,LengthUnit.Centimeters);
        Console.WriteLine($"service add(1 ft,12 in,target=cm) = {uc7RawResult2:F2}");

        Console.WriteLine("\nApplication Execution Completed\n");
    }
}
