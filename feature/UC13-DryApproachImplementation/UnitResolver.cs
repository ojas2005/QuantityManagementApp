using System;

/// <summary>
/// Resolves IMeasurable implementations for different unit types.
/// Extended to support VolumeUnit.
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
            
        throw new NotSupportedException($"Unit type {typeof(T)} is not supported.");
    }
}