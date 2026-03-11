using System;
using System.Collections.Generic;
using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace ModelLayer.Helpers
{
    public static class VolumeUnitHelper
    {
        private static readonly Dictionary<VolumeUnit, IMeasurable> _map = new Dictionary<VolumeUnit, IMeasurable>
        {
            { VolumeUnit.Litre, new VolumeUnitImpl(1.0, "L") },
            { VolumeUnit.Millilitre, new VolumeUnitImpl(0.001, "mL") },
            { VolumeUnit.Gallon, new VolumeUnitImpl(3.78541, "gal") }
        };

        public static IMeasurable Get(VolumeUnit unit) => _map[unit];

        private class VolumeUnitImpl : IMeasurable
        {
            private readonly double _toBaseFactor;
            private readonly string _label;

            public VolumeUnitImpl(double toBaseFactor, string label)
            {
                _toBaseFactor = toBaseFactor;
                _label = label;
            }

            public double ToBaseUnit(double value) => value * _toBaseFactor;
            public double FromBaseUnit(double baseValue) => baseValue / _toBaseFactor;
            public string Label => _label;
            
            public bool IsValidValue(double value) 
                => !double.IsNaN(value) && !double.IsInfinity(value);

            public Func<bool> SupportsArithmetic => () => true;
            
            public bool HasArithmeticSupport() => SupportsArithmetic();
            
            public void ValidateOperationSupport(string operation)
            {
                // Default implementation
            }
        }
    }
}
