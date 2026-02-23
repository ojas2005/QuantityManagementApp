using System;

/// <summary>
///     UC10: Generic Quantity Class with Unit Interface for Multi-Category Support
///     
///     This use case refactors the design into a single,generic Quantity&lt;TUnit&gt; class that works with 
///     any measurement category through a common IMeasurable interface. It eliminates code duplication across 
///     parallel QuantityLength and QuantityWeight classes,consolidates unit patterns,and simplifies 
///     the application to adhere to the Single Responsibility Principle.
///     
///     The refactoring maintains all functionality from UC1-UC9 while establishing a scalable,
///     maintainable architecture that supports seamless addition of new measurement categories 
///     (volume,temperature,etc.) without code duplication.
/// </summary>

public class Quantity<TUnit> : IEquatable<Quantity<TUnit>> where TUnit : struct,Enum
{
    private const double Tolerance = 0.0001;

    private readonly double _value;
    private readonly TUnit _unit;
    private readonly IMeasurable _unitImpl;

    public Quantity(double value,TUnit unit)
    {
        _unitImpl = UnitResolver.Get(unit);
        if (!_unitImpl.IsValidValue(value))
            throw new ArgumentException("Invalid value for this unit");
        _value = value;
        _unit = unit;
    }

    public double Value => _value;
    public TUnit Unit => _unit;

    public double ConvertTo(TUnit targetUnit)
    {
        var targetImpl = UnitResolver.Get(targetUnit);
        double baseValue = _unitImpl.ToBaseUnit(_value);
        return targetImpl.FromBaseUnit(baseValue);
    }

    public Quantity<TUnit> To(TUnit targetUnit) => new Quantity<TUnit>(ConvertTo(targetUnit),targetUnit);

    public override bool Equals(object obj) => Equals(obj as Quantity<TUnit>);

    public bool Equals(Quantity<TUnit> other)
    {
        if (other is null) return false;
        double thisBase = _unitImpl.ToBaseUnit(_value);
        double otherBase = other._unitImpl.ToBaseUnit(other._value);
        return Math.Abs(thisBase - otherBase) < Tolerance;
    }

    public override int GetHashCode()
    {
        double baseValue = _unitImpl.ToBaseUnit(_value);
        return Math.Round(baseValue,4).GetHashCode();
    }

    public override string ToString() => $"{_value} {_unitImpl.Label}";

    public static bool operator ==(Quantity<TUnit> left,Quantity<TUnit> right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Quantity<TUnit> left,Quantity<TUnit> right) => !(left == right);

    public Quantity<TUnit> Add(Quantity<TUnit> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        double baseSum = _unitImpl.ToBaseUnit(_value) + other._unitImpl.ToBaseUnit(other._value);
        double resultValue = _unitImpl.FromBaseUnit(baseSum);
        return new Quantity<TUnit>(resultValue,_unit);
    }

    public Quantity<TUnit> Add(Quantity<TUnit> other,TUnit targetUnit)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        var targetImpl = UnitResolver.Get(targetUnit);
        double baseSum = _unitImpl.ToBaseUnit(_value) + other._unitImpl.ToBaseUnit(other._value);
        double resultValue = targetImpl.FromBaseUnit(baseSum);
        return new Quantity<TUnit>(resultValue,targetUnit);
    }
    
    /// <summary>
    /// Static method to add two quantities.Renamed to AddQuantities to avoid ambiguity with instance Add method.
    /// </summary>
    public static Quantity<TUnit> AddQuantities(Quantity<TUnit> first,Quantity<TUnit> second)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        return first.Add(second);
    }

    /// <summary>
    /// Static method to add two quantities with target unit.Renamed to AddQuantities to avoid ambiguity with instance Add method.
    /// </summary>
    public static Quantity<TUnit> AddQuantities(Quantity<TUnit> first,Quantity<TUnit> second,TUnit targetUnit)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        //targetUnit is a value type(enum),cannot be null no need to check
        return first.Add(second,targetUnit);
    }
}