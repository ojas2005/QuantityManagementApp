using System;

/// <summary>
/// UC14: Refactored IMeasurable interface with default methods for operation support.
/// Now follows Interface Segregation Principle,units can override only what they need.
/// </summary>
public interface IMeasurable
{
    // Core methods,must be implemented by all units
    double ToBaseUnit(double value);
    double FromBaseUnit(double baseValue);
    string Label { get; }
    bool IsValidValue(double value);
    
    // Default lambda expression for arithmetic support,all units support by default
    Func<bool> SupportsArithmetic => () => true;
    
    // Default method to check if arithmetic is supported
    bool HasArithmeticSupport() => SupportsArithmetic();
    
    /// <summary>
    /// Validates if an operation is supported for this unit.
    /// Default implementation does nothing (all operations supported).
    /// Units like Temperature can override to throw meaningful exceptions.
    /// </summary>
    void ValidateOperationSupport(string operation)
    {
        // Default: no validation needed
    }
}