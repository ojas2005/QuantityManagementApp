using System;

public enum WeightUnit
{
    Kilogram,Gram,Pound
}

public static class WeightUnitExtensions
{
    private const double GramToKilogramConversionFactor = 0.001;
    private const double PoundToKilogramConversionFactor = 0.453592;

    public static double ConvertToBaseUnit(this WeightUnit unit, double value)
    {
        return unit switch
        {
            WeightUnit.Kilogram => value,
            WeightUnit.Gram => value * GramToKilogramConversionFactor,
            WeightUnit.Pound => value * PoundToKilogramConversionFactor,
            _ => throw new ArgumentException($"Invalid weight unit: {unit}")
        };
    }

    public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
    {
        return unit switch
        {
            WeightUnit.Kilogram => baseValue,
            WeightUnit.Gram => baseValue / GramToKilogramConversionFactor,
            WeightUnit.Pound => baseValue / PoundToKilogramConversionFactor,
            _ => throw new ArgumentException($"Invalid weight unit: {unit}")
        };
    }

    public static double GetConversionFactor(this WeightUnit unit)
    {
        return unit switch
        {
            WeightUnit.Kilogram => 1.0,
            WeightUnit.Gram => GramToKilogramConversionFactor,
            WeightUnit.Pound => PoundToKilogramConversionFactor,
            _ => throw new ArgumentException($"Invalid weight unit: {unit}")
        };
    }

    public static string GetLabel(this WeightUnit unit)
    {
        return unit switch
        {
            WeightUnit.Kilogram => "kg",
            WeightUnit.Gram => "g",
            WeightUnit.Pound => "lb",
            _ => unit.ToString()
        };
    }
}
