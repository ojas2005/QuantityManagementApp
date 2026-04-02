using System;
using System.Collections.Generic;
using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace ModelLayer.Helpers
{
    public static class LengthUnitHelper
    {
        private static readonly Dictionary<LengthUnit, IMeasurable> _map = new Dictionary<LengthUnit, IMeasurable>
        {
            { LengthUnit.Feet, new LengthUnitImpl(1.0, "ft") },
            { LengthUnit.Inch, new LengthUnitImpl(1.0 / 12.0, "in") },
            { LengthUnit.Yards, new LengthUnitImpl(3.0, "yd") },
            { LengthUnit.Centimeters, new LengthUnitImpl(0.0328084, "cm") }
        };

        public static IMeasurable Get(LengthUnit unit) => _map[unit];

        private class LengthUnitImpl : IMeasurable
        {
            private readonly double _toBaseFactor;
            private readonly string _label;

            public LengthUnitImpl(double toBaseFactor, string label)
            {
                _toBaseFactor = toBaseFactor;
                _label = label;
            }

            public double ToBaseUnit(double value) => value * _toBaseFactor;
            public double FromBaseUnit(double baseValue) => baseValue / _toBaseFactor;
            public string Label => _label;
            
            public bool IsValidValue(double value) 
            { 
                return !double.IsNaN(value) && !double.IsInfinity(value);
            }

            // Default implementations from IMeasurable
            public Func<bool> SupportsArithmetic => () => true;
            
            public bool HasArithmeticSupport() => SupportsArithmetic();
            
            public void ValidateOperationSupport(string operation)
            {
                // Default implementation - no validation needed for Length
            }
        }
    }
}
