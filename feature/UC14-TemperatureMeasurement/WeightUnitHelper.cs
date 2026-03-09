using System;
using System.Collections.Generic;

public static class WeightUnitHelper
{
    private static readonly Dictionary<WeightUnit, IMeasurable> _map = new Dictionary<WeightUnit, IMeasurable>
    {
        { WeightUnit.Kilogram, new WeightUnitImpl(1.0, "kg") },
        { WeightUnit.Gram, new WeightUnitImpl(0.001, "g") },
        { WeightUnit.Pound, new WeightUnitImpl(0.453592, "lb") }
    };

    public static IMeasurable Get(WeightUnit unit) => _map[unit];

    private class WeightUnitImpl : IMeasurable
    {
        private readonly double _toBaseFactor;
        private readonly string _label;

        public WeightUnitImpl(double toBaseFactor, string label)
        {
            _toBaseFactor = toBaseFactor;
            _label = label;
        }

        public double ToBaseUnit(double value) => value * _toBaseFactor;
        public double FromBaseUnit(double baseValue) => baseValue / _toBaseFactor;
        public string Label => _label;
        public bool IsValidValue(double value) => !double.IsNaN(value) && !double.IsInfinity(value); // allow negative
    }
}