using System;
using System.Collections.Generic;
using ModelLayer.Enums;
using ModelLayer.Interfaces;

namespace ModelLayer.Helpers
{
    public static class TemperatureUnitHelper
    {
        private static readonly Dictionary<TemperatureUnit, IMeasurable> _map = new Dictionary<TemperatureUnit, IMeasurable>
        {
            { TemperatureUnit.Celsius, new TemperatureUnitImpl("°C", true) },
            { TemperatureUnit.Fahrenheit, new TemperatureUnitImpl("°F", false) }
        };

        public static IMeasurable Get(TemperatureUnit unit) => _map[unit];

        private class TemperatureUnitImpl : IMeasurable
        {
            private readonly string _label;
            private readonly bool _isBaseUnit;
            private readonly Func<double, double> _toBaseUnit;
            private readonly Func<double, double> _fromBaseUnit;

            public TemperatureUnitImpl(string label, bool isBaseUnit)
            {
                _label = label;
                _isBaseUnit = isBaseUnit;
                
                if (isBaseUnit)
                {
                    _toBaseUnit = (celsius) => celsius;
                    _fromBaseUnit = (celsius) => celsius;
                }
                else
                {
                    _toBaseUnit = (fahrenheit) => (fahrenheit - 32) * 5.0 / 9.0;
                    _fromBaseUnit = (celsius) => (celsius * 9.0 / 5.0) + 32;
                }
            }

            public double ToBaseUnit(double value) => _toBaseUnit(value);
            public double FromBaseUnit(double baseValue) => _fromBaseUnit(baseValue);
            public string Label => _label;
            
            public bool IsValidValue(double value) 
                => !double.IsNaN(value) && !double.IsInfinity(value);

            // Temperature does NOT support arithmetic
            public Func<bool> SupportsArithmetic => () => false;
            
            public bool HasArithmeticSupport() => SupportsArithmetic();
            
            public void ValidateOperationSupport(string operation)
            {
                throw new NotSupportedException(
                    $"Temperature does not support {operation} operations. " +
                    $"Only equality comparison and unit conversion are supported for temperature."
                );
            }
        }
    }
}
