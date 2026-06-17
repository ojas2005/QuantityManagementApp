using System;

namespace ModelLayer.Interfaces
{
    /// <summary>
    /// IMeasurable interface with default methods for operation support.
    /// Follows Interface Segregation Principle.
    /// </summary>
    public interface IMeasurable
    {
        // Core methods - must be implemented by all units
        double ToBaseUnit(double value);
        double FromBaseUnit(double baseValue);
        string Label { get; }
        bool IsValidValue(double value);
        
        // Default lambda expression for arithmetic support
        Func<bool> SupportsArithmetic => () => true;
        
        // Default method to check if arithmetic is supported
        bool HasArithmeticSupport() => SupportsArithmetic();
        
        /// <summary>
        /// Validates if an operation is supported for this unit.
        /// Units like Temperature can override to throw meaningful exceptions.
        /// </summary>
        void ValidateOperationSupport(string operation)
        {
            // Default: no validation needed
        }
    }
}