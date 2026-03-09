using System;
using System.Collections.Generic;

/// <summary>
/// Temperature unit enum implementing IMeasurable.
/// Temperature does NOT support arithmetic operations (addition, subtraction, division).
/// Uses lambda expressions for conversion logic and operation support.
/// </summary>
public enum TemperatureUnit
{
    Celsius,
    Fahrenheit
}

public static class TemperatureUnitHelper
{
    // Celsius is the base unit for temperature conversions
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

        // Lambda expressions for temperature conversions
        private readonly Func<double, double> _toBaseUnit;
        private readonly Func<double, double> _fromBaseUnit;
        
        // Lambda expression to indicate temperature does NOT support arithmetic
        private readonly Func<bool> _supportsArithmetic = () => false;

        public TemperatureUnitImpl(string label, bool isBaseUnit)
        {
            _label = label;
            _isBaseUnit = isBaseUnit;
            
            // Define conversion lambdas based on unit type
            if (isBaseUnit)
            {
                // Celsius is base unit - identity functions
                _toBaseUnit = (celsius) => celsius;      // Celsius -> Celsius
                _fromBaseUnit = (celsius) => celsius;    // Celsius -> Celsius
            }
            else
            {
                // Fahrenheit conversion lambdas
                _toBaseUnit = (fahrenheit) => (fahrenheit - 32) * 5.0 / 9.0;     // °F -> °C
                _fromBaseUnit = (celsius) => (celsius * 9.0 / 5.0) + 32;         // °C -> °F
            }
        }

        public double ToBaseUnit(double value) => _toBaseUnit(value);
        
        public double FromBaseUnit(double baseValue) => _fromBaseUnit(baseValue);
        
        public string Label => _label;
        
        public bool IsValidValue(double value) => !double.IsNaN(value) && !double.IsInfinity(value);
        
        // Override to indicate temperature does NOT support arithmetic
        public Func<bool> SupportsArithmetic => _supportsArithmetic;
        
        /// <summary>
        /// Override to validate operations - temperature supports only equality and conversion.
        /// Throws NotSupportedException for any arithmetic operation.
        /// </summary>
        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException(
                $"Temperature does not support {operation} operations. " +
                $"Only equality comparison and unit conversion are supported for temperature."
            );
        }
    }
}