using System;

/// <summary>
/// Resolves IMeasurable implementations for different unit types.
/// Extended to support TemperatureUnit.
/// </summary>
public static class UnitResolver
{
    public static IMeasurable Get<T>(T unit) where T : struct, Enum
    {
        if (unit is LengthUnit l)
            return LengthUnitHelper.Get(l);
        if (unit is WeightUnit w)
            return WeightUnitHelper.Get(w);
        if (unit is VolumeUnit v)
            return VolumeUnitHelper.Get(v);
        if (unit is TemperatureUnit t)
            return TemperatureUnitHelper.Get(t);
            
        throw new NotSupportedException($"Unit type {typeof(T)} is not supported.");
    }
}