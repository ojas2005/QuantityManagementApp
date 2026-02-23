using System;

public static class UnitResolver
{
    public static IMeasurable Get<T>(T unit) where T : struct, Enum
    {
        if (unit is LengthUnit l)
            return LengthUnitHelper.Get(l);
        if (unit is WeightUnit w)
            return WeightUnitHelper.Get(w);
        throw new NotSupportedException($"Unit type {typeof(T)} is not supported.");
    }
}