using System;
using System.Collections.Generic;

public static class LengthUnitHelper
{
    private static readonly Dictionary<LengthUnit, IMeasurable> _map = new Dictionary<LengthUnit, IMeasurable>
    {
        { LengthUnit.Feet, new LengthUnitImpl(1.0, "ft", true) },
        { LengthUnit.Inch, new LengthUnitImpl(1.0 / 12.0, "in", true) },
        { LengthUnit.Yards, new LengthUnitImpl(3.0, "yd", true) },
        { LengthUnit.Centimeters, new LengthUnitImpl(0.0328084, "cm", true) }
    };

    public static IMeasurable Get(LengthUnit unit) => _map[unit];

    private class LengthUnitImpl : IMeasurable
    {
        private readonly double _toBaseFactor;
        private readonly string _label;
        private readonly bool _allowNegative;

        public LengthUnitImpl(double toBaseFactor, string label, bool allowNegative)
        {
            _toBaseFactor = toBaseFactor;
            _label = label;
            _allowNegative = allowNegative;
        }

        public double ToBaseUnit(double value) => value * _toBaseFactor;
        public double FromBaseUnit(double baseValue) => baseValue / _toBaseFactor;
        public string Label => _label;
        
        public bool IsValidValue(double value) 
        { 
            return !double.IsNaN(value) && 
                   !double.IsInfinity(value) && 
                   (_allowNegative || value >= 0);
        }
    }
}