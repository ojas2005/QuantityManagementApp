using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///     UC10: Generic Quantity Class with Unit Interface for Multi-Category Support
///     UC12: Extended with Subtraction and Division Operations
///     UC13: Centralized Arithmetic Logic
///     UC14: Temperature Support with Selective Arithmetic
///     
///     This class represents a quantity with a unit, supporting operations based on
///     what the unit type allows. Temperature units have restricted arithmetic support.
/// </summary>
public class Quantity<TUnit> : IEquatable<Quantity<TUnit>> where TUnit : struct, Enum
{
    private const double Tolerance = 0.0001;
    private const int RoundingDecimals = 2;

    private readonly double _value;
    private readonly TUnit _unit;
    private readonly IMeasurable _unitImpl;

    /// <summary>
    /// Initializes a new instance of the Quantity class.
    /// </summary>
    public Quantity(double value, TUnit unit)
    {
        _unitImpl = UnitResolver.Get(unit);
        if (!_unitImpl.IsValidValue(value))
            throw new ArgumentException($"Invalid value {value} for unit {unit}");
        _value = value;
        _unit = unit;
    }

    public double Value => _value;
    public TUnit Unit => _unit;

    /// <summary>
    /// Converts the quantity to a different unit within the same category.
    /// </summary>
    public double ConvertTo(TUnit targetUnit)
    {
        var targetImpl = UnitResolver.Get(targetUnit);
        double baseValue = _unitImpl.ToBaseUnit(_value);
        return targetImpl.FromBaseUnit(baseValue);
    }

    /// <summary>
    /// Creates a new quantity converted to the specified unit.
    /// </summary>
    public Quantity<TUnit> To(TUnit targetUnit) => new Quantity<TUnit>(ConvertTo(targetUnit), targetUnit);

    public override bool Equals(object? obj) => Equals(obj as Quantity<TUnit>);

    public bool Equals(Quantity<TUnit>? other)
    {
        if (other is null) return false;
        
        // Cross-category prevention - different unit types cannot be equal
        if (_unit.GetType() != other._unit.GetType())
            return false;
            
        double thisBase = _unitImpl.ToBaseUnit(_value);
        double otherBase = other._unitImpl.ToBaseUnit(other._value);
        return Math.Abs(thisBase - otherBase) < Tolerance;
    }

    public override int GetHashCode()
    {
        double baseValue = _unitImpl.ToBaseUnit(_value);
        return Math.Round(baseValue, 4).GetHashCode();
    }

    public override string ToString() => $"{_value} {_unitImpl.Label}";

    public static bool operator ==(Quantity<TUnit>? left, Quantity<TUnit>? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Quantity<TUnit>? left, Quantity<TUnit>? right) => !(left == right);

    #region Private Arithmetic Helper Methods

    /// <summary>
    /// Defines the arithmetic operations using Func delegates.
    /// </summary>
    private enum ArithmeticOperation
    {
        ADD,
        SUBTRACT,
        DIVIDE
    }

    /// <summary>
    /// Gets the lambda function for the specified arithmetic operation.
    /// </summary>
    private Func<double, double, double> GetOperationFunction(ArithmeticOperation operation)
    {
        switch (operation)
        {
            case ArithmeticOperation.ADD:
                return (a, b) => a + b;
                
            case ArithmeticOperation.SUBTRACT:
                return (a, b) => a - b;
                
            case ArithmeticOperation.DIVIDE:
                return (a, b) => 
                {
                    if (Math.Abs(b) < Tolerance)
                        throw new DivideByZeroException("Cannot divide by zero quantity");
                    return a / b;
                };
                
            default:
                throw new NotSupportedException($"Operation {operation} is not supported");
        }
    }

    /// <summary>
    /// Validates arithmetic operands for null and finiteness.
    /// Also checks if the unit supports arithmetic operations.
    /// </summary>
    private void ValidateOperands(Quantity<TUnit> other, TUnit? targetUnit, bool targetRequired, ArithmeticOperation operation)
    {
        // Null check
        if (other == null)
            throw new ArgumentNullException(nameof(other), "Quantity operand cannot be null");

        // Check if this unit supports arithmetic operations
        if (!_unitImpl.HasArithmeticSupport())
        {
            _unitImpl.ValidateOperationSupport(operation.ToString());
        }

        // Value validation
        if (!_unitImpl.IsValidValue(_value))
            throw new ArgumentException($"Invalid value {_value} for this quantity");
        
        if (!other._unitImpl.IsValidValue(other._value))
            throw new ArgumentException($"Invalid value {other._value} for operand quantity");

        // Target unit check for add/subtract
        if (targetRequired && targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit), "Target unit cannot be null for this operation");
    }

    /// <summary>
    /// Performs arithmetic operation using lambda expressions.
    /// </summary>
    private double PerformBaseArithmetic(Quantity<TUnit> other, ArithmeticOperation operation)
    {
        // Convert both to base unit
        double thisBase = _unitImpl.ToBaseUnit(_value);
        double otherBase = other._unitImpl.ToBaseUnit(other._value);

        // Get the lambda function for the operation
        var operationFunc = GetOperationFunction(operation);
        
        // Execute the lambda
        return operationFunc(thisBase, otherBase);
    }

    /// <summary>
    /// Rounds a value to the specified decimal places.
    /// </summary>
    private double RoundToDecimals(double value) => 
        Math.Round(value, RoundingDecimals, MidpointRounding.AwayFromZero);

    #endregion

    #region Public Arithmetic Operations

    /// <summary>
    /// Adds another quantity to this quantity.
    /// </summary>
    public Quantity<TUnit> Add(Quantity<TUnit> other)
    {
        ValidateOperands(other, _unit, true, ArithmeticOperation.ADD);
        double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
        double resultValue = _unitImpl.FromBaseUnit(baseResult);
        return new Quantity<TUnit>(RoundToDecimals(resultValue), _unit);
    }

    /// <summary>
    /// Adds another quantity to this quantity and returns result in specified unit.
    /// </summary>
    public Quantity<TUnit> Add(Quantity<TUnit> other, TUnit targetUnit)
    {
        ValidateOperands(other, targetUnit, true, ArithmeticOperation.ADD);
        double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
        var targetImpl = UnitResolver.Get(targetUnit);
        double resultValue = targetImpl.FromBaseUnit(baseResult);
        return new Quantity<TUnit>(RoundToDecimals(resultValue), targetUnit);
    }

    /// <summary>
    /// Subtracts another quantity from this quantity.
    /// </summary>
    public Quantity<TUnit> Subtract(Quantity<TUnit> other)
    {
        ValidateOperands(other, _unit, true, ArithmeticOperation.SUBTRACT);
        double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
        double resultValue = _unitImpl.FromBaseUnit(baseResult);
        return new Quantity<TUnit>(RoundToDecimals(resultValue), _unit);
    }

    /// <summary>
    /// Subtracts another quantity from this quantity and returns result in specified unit.
    /// </summary>
    public Quantity<TUnit> Subtract(Quantity<TUnit> other, TUnit targetUnit)
    {
        ValidateOperands(other, targetUnit, true, ArithmeticOperation.SUBTRACT);
        double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
        var targetImpl = UnitResolver.Get(targetUnit);
        double resultValue = targetImpl.FromBaseUnit(baseResult);
        return new Quantity<TUnit>(RoundToDecimals(resultValue), targetUnit);
    }

    /// <summary>
    /// Divides this quantity by another quantity, returning a dimensionless ratio.
    /// </summary>
    public double Divide(Quantity<TUnit> other)
    {
        ValidateOperands(other, null, false, ArithmeticOperation.DIVIDE);
        return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
    }

    #endregion

    #region Static Factory Methods

    /// <summary>
    /// Static method to add two quantities.
    /// </summary>
    public static Quantity<TUnit> AddQuantities(Quantity<TUnit>? first, Quantity<TUnit>? second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return first.Add(second);
    }

    /// <summary>
    /// Static method to add two quantities with target unit.
    /// </summary>
    public static Quantity<TUnit> AddQuantities(Quantity<TUnit>? first, Quantity<TUnit>? second, TUnit targetUnit)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return first.Add(second, targetUnit);
    }

    /// <summary>
    /// Static method to subtract two quantities.
    /// </summary>
    public static Quantity<TUnit> SubtractQuantities(Quantity<TUnit>? first, Quantity<TUnit>? second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return first.Subtract(second);
    }

    /// <summary>
    /// Static method to subtract two quantities with target unit.
    /// </summary>
    public static Quantity<TUnit> SubtractQuantities(Quantity<TUnit>? first, Quantity<TUnit>? second, TUnit targetUnit)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return first.Subtract(second, targetUnit);
    }

    /// <summary>
    /// Static method to divide two quantities.
    /// </summary>
    public static double DivideQuantities(Quantity<TUnit>? first, Quantity<TUnit>? second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return first.Divide(second);
    }

    #endregion
}