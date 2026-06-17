using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///     UC10: Generic Quantity Class with Unit Interface for Multi-Category Support
///     UC12: Extended with Subtraction and Division Operations
///     
///     This class represents a quantity with a unit, supporting arithmetic operations
///     across different measurement categories through the IMeasurable interface.
///     All operations maintain immutability and provide comprehensive validation.
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
    /// <param name="value">The numeric value.</param>
    /// <param name="unit">The unit of measurement.</param>
    /// <exception cref="ArgumentException">Thrown when value is invalid for the unit.</exception>
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
    /// <param name="targetUnit">The target unit.</param>
    /// <returns>The converted value.</returns>
    public double ConvertTo(TUnit targetUnit)
    {
        var targetImpl = UnitResolver.Get(targetUnit);
        double baseValue = _unitImpl.ToBaseUnit(_value);
        return targetImpl.FromBaseUnit(baseValue);
    }

    /// <summary>
    /// Creates a new quantity converted to the specified unit.
    /// </summary>
    /// <param name="targetUnit">The target unit.</param>
    /// <returns>A new quantity in the target unit.</returns>
    public Quantity<TUnit> To(TUnit targetUnit) => new Quantity<TUnit>(ConvertTo(targetUnit), targetUnit);

    public override bool Equals(object? obj) => Equals(obj as Quantity<TUnit>);

    public bool Equals(Quantity<TUnit>? other)
    {
        if (other is null) return false;
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

    #region Arithmetic Operations

    /// <summary>
    /// Adds another quantity to this quantity.
    /// </summary>
    /// <param name="other">The quantity to add.</param>
    /// <returns>A new quantity representing the sum in this quantity's unit.</returns>
    /// <exception cref="ArgumentNullException">Thrown when other is null.</exception>
    public Quantity<TUnit> Add(Quantity<TUnit> other)
    {
        ValidateOperand(other, nameof(other));
        return PerformArithmeticOperation(other, (a, b) => a + b, _unit);
    }

    /// <summary>
    /// Adds another quantity to this quantity and returns result in specified unit.
    /// </summary>
    /// <param name="other">The quantity to add.</param>
    /// <param name="targetUnit">The unit for the result.</param>
    /// <returns>A new quantity representing the sum in the target unit.</returns>
    /// <exception cref="ArgumentNullException">Thrown when other is null.</exception>
    public Quantity<TUnit> Add(Quantity<TUnit> other, TUnit targetUnit)
    {
        ValidateOperand(other, nameof(other));
        return PerformArithmeticOperation(other, (a, b) => a + b, targetUnit);
    }

    /// <summary>
    /// Subtracts another quantity from this quantity.
    /// </summary>
    /// <param name="other">The quantity to subtract.</param>
    /// <returns>A new quantity representing the difference in this quantity's unit.</returns>
    /// <exception cref="ArgumentNullException">Thrown when other is null.</exception>
    public Quantity<TUnit> Subtract(Quantity<TUnit> other)
    {
        ValidateOperand(other, nameof(other));
        return PerformArithmeticOperation(other, (a, b) => a - b, _unit);
    }

    /// <summary>
    /// Subtracts another quantity from this quantity and returns result in specified unit.
    /// </summary>
    /// <param name="other">The quantity to subtract.</param>
    /// <param name="targetUnit">The unit for the result.</param>
    /// <returns>A new quantity representing the difference in the target unit.</returns>
    /// <exception cref="ArgumentNullException">Thrown when other is null.</exception>
    public Quantity<TUnit> Subtract(Quantity<TUnit> other, TUnit targetUnit)
    {
        ValidateOperand(other, nameof(other));
        return PerformArithmeticOperation(other, (a, b) => a - b, targetUnit);
    }

    /// <summary>
    /// Divides this quantity by another quantity, returning a dimensionless ratio.
    /// </summary>
    /// <param name="other">The divisor quantity.</param>
    /// <returns>The ratio of this quantity to the other quantity.</returns>
    /// <exception cref="ArgumentNullException">Thrown when other is null.</exception>
    /// <exception cref="DivideByZeroException">Thrown when the divisor quantity is zero.</exception>
    public double Divide(Quantity<TUnit> other)
    {
        ValidateOperand(other, nameof(other));
        
        double thisBase = _unitImpl.ToBaseUnit(_value);
        double otherBase = other._unitImpl.ToBaseUnit(other._value);
        
        if (Math.Abs(otherBase) < Tolerance)
            throw new DivideByZeroException("Cannot divide by zero quantity");
            
        return thisBase / otherBase;
    }

    /// <summary>
    /// Validates that the operand is not null and belongs to the same category.
    /// </summary>
    private void ValidateOperand(Quantity<TUnit>? other, string paramName)
    {
        if (other == null)
            throw new ArgumentNullException(paramName, "Quantity operand cannot be null");
            
        // Additional validation could be added here if needed
    }

    /// <summary>
    /// Performs an arithmetic operation between this quantity and another.
    /// </summary>
    private Quantity<TUnit> PerformArithmeticOperation(
        Quantity<TUnit> other, 
        Func<double, double, double> operation, 
        TUnit targetUnit)
    {
        double thisBase = _unitImpl.ToBaseUnit(_value);
        double otherBase = other._unitImpl.ToBaseUnit(other._value);
        
        double baseResult = operation(thisBase, otherBase);
        
        var targetImpl = UnitResolver.Get(targetUnit);
        double resultValue = targetImpl.FromBaseUnit(baseResult);
        
        // Round to specified decimal places for consistency
        resultValue = Math.Round(resultValue, RoundingDecimals, MidpointRounding.AwayFromZero);
        
        return new Quantity<TUnit>(resultValue, targetUnit);
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