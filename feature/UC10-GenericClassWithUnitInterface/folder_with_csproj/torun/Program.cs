using System;
using static System.Console;

public class QuantityMeasurementApp
{
    static void Main(string[] args)
    {
        WriteLine("Quantity Measurement Application (Generic)\n");

        // Length equality demonstration
        var feet1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
        var feet2 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
        WriteLine($"feet testing: {feet1} equals {feet2}? {feet1.Equals(feet2)}");

        var inches1 = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
        var inches2 = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
        WriteLine($"inches testing: {inches1} equals {inches2}? {inches1.Equals(inches2)}");

        var feet = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
        var inches = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
        WriteLine($"cross unit testing: {feet} equals {inches}? {feet.Equals(inches)}");

        var yard1 = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
        var yard2 = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
        WriteLine($"yard to yard: {yard1} equals {yard2}? {yard1.Equals(yard2)}");

        var yard3Feet = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
        var feet3 = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
        WriteLine($"yard to feet: {yard3Feet} equals {feet3}? {yard3Feet.Equals(feet3)}");

        var yard36Inches = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
        var inches36 = new Quantity<LengthUnit>(36.0, LengthUnit.Inch);
        WriteLine($"yard to inches: {yard36Inches} equals {inches36}? {yard36Inches.Equals(inches36)}");

        var cm1 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeters);
        var cm2 = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeters);
        WriteLine($"cm to cm: {cm1} equals {cm2}? {cm1.Equals(cm2)}");

        var cm1ToInches = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeters);
        var inches0393701 = new Quantity<LengthUnit>(0.393701, LengthUnit.Inch);
        WriteLine($"cm to inches: {cm1ToInches} equals {inches0393701}? {cm1ToInches.Equals(inches0393701)}");

        // Unit conversions
        double feet2Inches = new Quantity<LengthUnit>(1.0, LengthUnit.Feet).ConvertTo(LengthUnit.Inch);
        WriteLine($"convert 1 foot to inches: {feet2Inches} inches");

        double yards2Feet = new Quantity<LengthUnit>(3.0, LengthUnit.Yards).ConvertTo(LengthUnit.Feet);
        WriteLine($"convert 3 yards to feet: {yards2Feet} feet");

        double inches2Yards = new Quantity<LengthUnit>(36.0, LengthUnit.Inch).ConvertTo(LengthUnit.Yards);
        WriteLine($"convert 36 inches to yards: {inches2Yards} yards");

        double cm2Inches = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeters).ConvertTo(LengthUnit.Inch);
        WriteLine($"convert 1 centimeter to inches: {cm2Inches:F6} inches");

        double feet2Cm = new Quantity<LengthUnit>(1.0, LengthUnit.Feet).ConvertTo(LengthUnit.Centimeters);
        WriteLine($"convert 1 foot to centimeters: {feet2Cm:F6} centimeters");

        double zero2Inches = new Quantity<LengthUnit>(0.0, LengthUnit.Feet).ConvertTo(LengthUnit.Inch);
        WriteLine($"convert 0 feet to inches: {zero2Inches} inches");

        double feet2Feet = new Quantity<LengthUnit>(5.0, LengthUnit.Feet).ConvertTo(LengthUnit.Feet);
        WriteLine($"convert 5 feet to feet: {feet2Feet} feet");

        try
        {
            var invalid = new Quantity<LengthUnit>(-1.0, LengthUnit.Feet);
        }
        catch (ArgumentException ex)
        {
            WriteLine($"negative value error: {ex.Message}");
        }

        WriteLine();

        // Instance conversion
        var quantityLength = new Quantity<LengthUnit>(2.0, LengthUnit.Yards);
        double yards2InchesViaInstance = quantityLength.ConvertTo(LengthUnit.Inch);
        WriteLine($"2 yards converted to inches: {yards2InchesViaInstance} inches");

        // Addition
        WriteLine("\nAddition of Length Units:");
        var add1Feet = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
        var add2Feet = new Quantity<LengthUnit>(2.0, LengthUnit.Feet);
        var addResult1 = add1Feet.Add(add2Feet);
        WriteLine($"add({add1Feet},{add2Feet}) = {addResult1}");

        var add1FeetAgain = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
        var add12Inches = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
        var addResult2 = add1FeetAgain.Add(add12Inches);
        WriteLine($"add({add1FeetAgain},{add12Inches}) = {addResult2}");

        var add12InchesAgain = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
        var add1FeetForInches = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
        var addResult3 = add12InchesAgain.Add(add1FeetForInches);
        WriteLine($"add({add12InchesAgain},{add1FeetForInches}) = {addResult3}");

        var add1Yard = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
        var add3Feet = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);
        var addResult4 = add1Yard.Add(add3Feet);
        WriteLine($"add({add1Yard},{add3Feet}) = {addResult4}");

        var add36Inches = new Quantity<LengthUnit>(36.0, LengthUnit.Inch);
        var add1YardForInches = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
        var addResult5 = add36Inches.Add(add1YardForInches);
        WriteLine($"add({add36Inches},{add1YardForInches}) = {addResult5}");

        var add2_54Cm = new Quantity<LengthUnit>(2.54, LengthUnit.Centimeters);
        var add1InchForCm = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
        var addResult6 = add2_54Cm.Add(add1InchForCm);
        WriteLine($"add({add2_54Cm},{add1InchForCm}) = {addResult6:F2}");

        var add5Feet = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
        var add0Inches = new Quantity<LengthUnit>(0.0, LengthUnit.Inch);
        var addResult7 = add5Feet.Add(add0Inches);
        WriteLine($"add({add5Feet},{add0Inches}) = {addResult7}");

        WriteLine();

        // Addition with explicit target unit
        WriteLine("Addition with Explicit Target Unit:");
        var uc7Feet = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
        var uc7Inches = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);

        var uc7ResultFeet = uc7Feet.Add(uc7Inches, LengthUnit.Feet);
        WriteLine($"add({uc7Feet},{uc7Inches},target=feet) = {uc7ResultFeet}");

        var uc7ResultInches = uc7Feet.Add(uc7Inches, LengthUnit.Inch);
        WriteLine($"add({uc7Feet},{uc7Inches},target=inches) = {uc7ResultInches}");

        var uc7ResultYards = uc7Feet.Add(uc7Inches, LengthUnit.Yards);
        WriteLine($"add({uc7Feet},{uc7Inches},target=yards) = {uc7ResultYards:F2}");

        var uc7Yard = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
        var uc7FeetAgain = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);

        var uc7ResultYardsFromYards = uc7Yard.Add(uc7FeetAgain, LengthUnit.Yards);
        WriteLine($"add({uc7Yard},{uc7FeetAgain},target=yards) = {uc7ResultYardsFromYards}");

        var uc7ResultFeetFromYards = uc7Yard.Add(uc7FeetAgain, LengthUnit.Feet);
        WriteLine($"add({uc7Yard},{uc7FeetAgain},target=feet) = {uc7ResultFeetFromYards}");

        var uc7Cm = new Quantity<LengthUnit>(2.54, LengthUnit.Centimeters);
        var uc7InchForCm = new Quantity<LengthUnit>(1.0, LengthUnit.Inch);
        var uc7ResultCm = uc7Cm.Add(uc7InchForCm, LengthUnit.Centimeters);
        WriteLine($"add({uc7Cm},{uc7InchForCm},target=cm) = {uc7ResultCm:F2}");

        var uc7Large1 = new Quantity<LengthUnit>(5.0, LengthUnit.Feet);
        var uc7Large2 = new Quantity<LengthUnit>(0.0, LengthUnit.Inch);
        var uc7ResultLarge = uc7Large1.Add(uc7Large2, LengthUnit.Yards);
        WriteLine($"add({uc7Large1},{uc7Large2},target=yards) = {uc7ResultLarge:F2}");

        WriteLine("\nWeight Equality & Conversion:\n");

        var kg1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
        var kg2 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
        WriteLine($"kg equality: {kg1} equals {kg2}? {kg1.Equals(kg2)}");

        var kg = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
        var grams1000 = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
        WriteLine($"kg to gram equality: {kg} equals {grams1000}? {kg.Equals(grams1000)}");

        var pound = new Quantity<WeightUnit>(2.20462, WeightUnit.Pound);
        WriteLine($"kg to pound equality: {kg} equals {pound}? {kg.Equals(pound)}");

        double kgToGram = kg.ConvertTo(WeightUnit.Gram);
        WriteLine($"convert 1 kg to grams: {kgToGram} g");

        double gramToKg = grams1000.ConvertTo(WeightUnit.Kilogram);
        WriteLine($"convert 1000 g to kg: {gramToKg} kg");

        double kgToPound = kg.ConvertTo(WeightUnit.Pound);
        WriteLine($"convert 1 kg to pounds: {kgToPound:F5} lb");

        WriteLine("\nWeight Addition:");

        var addKg = new Quantity<WeightUnit>(2.0, WeightUnit.Kilogram);
        var addGram = new Quantity<WeightUnit>(500.0, WeightUnit.Gram);

        var addResultDefault = addKg.Add(addGram);
        WriteLine($"add({addKg},{addGram}) = {addResultDefault}");

        var addResultGram = addKg.Add(addGram, WeightUnit.Gram);
        WriteLine($"add({addKg},{addGram},target=gram) = {addResultGram}");

        var addResultPound = addKg.Add(addGram, WeightUnit.Pound);
        WriteLine($"add({addKg},{addGram},target=pound) = {addResultPound:F5}");

        WriteLine("\nNegative Weight Demonstration:");

        var negKg = new Quantity<WeightUnit>(-1.0, WeightUnit.Kilogram);
        var negGram = new Quantity<WeightUnit>(-1000.0, WeightUnit.Gram);
        WriteLine($"negative equality: {negKg} equals {negGram}? {negKg.Equals(negGram)}");

        var negAddResult = kg.Add(negGram);
        WriteLine($"add({kg},{negGram}) = {negAddResult}");

        WriteLine("\nApplication Execution Completed\n");
    }
}