using System;

public enum LengthUnit
{
    Feet,Inch,Yards,Centimeters
}

public static class LengthUnitExtensions
{
    private const double InchToFeetConversionFactor = 1.0/12.0;
    private const double YardsToFeetConversionFactor = 3.0;
    private const double CentimetersToFeetConversionFactor = 0.0328084;

    public static double ConvertToBaseUnit(this LengthUnit unit,double value)
    {
        return unit switch
        {
            LengthUnit.Feet => value,
            LengthUnit.Inch => value * InchToFeetConversionFactor,
            LengthUnit.Yards => value * YardsToFeetConversionFactor,
            LengthUnit.Centimeters => value * CentimetersToFeetConversionFactor,
            _ => throw new ArgumentException($"Invalid length unit: {unit}")
        };
    }

    public static double ConvertFromBaseUnit(this LengthUnit unit,double baseValue)
    {
        return unit switch
        {
            LengthUnit.Feet => baseValue,
            LengthUnit.Inch => baseValue / InchToFeetConversionFactor,
            LengthUnit.Yards => baseValue / YardsToFeetConversionFactor,
            LengthUnit.Centimeters => baseValue / CentimetersToFeetConversionFactor,
            _ => throw new ArgumentException($"enter valid length unit {unit}")
        };
    }

    public static double GetConversionFactor(this LengthUnit unit)
    {
        return unit switch
        {
            LengthUnit.Feet => 1.0,
            LengthUnit.Inch => InchToFeetConversionFactor,
            LengthUnit.Yards => YardsToFeetConversionFactor,
            LengthUnit.Centimeters => CentimetersToFeetConversionFactor,
            _ => throw new ArgumentException($"enter valid length unit {unit}")
        };
    }

    public static string GetLabel(this LengthUnit unit)
    {
        return unit switch
        {
            LengthUnit.Feet => "ft",
            LengthUnit.Inch => "in",
            LengthUnit.Yards => "yd",
            LengthUnit.Centimeters => "cm",
            _ => unit.ToString()
        };
    }
}
