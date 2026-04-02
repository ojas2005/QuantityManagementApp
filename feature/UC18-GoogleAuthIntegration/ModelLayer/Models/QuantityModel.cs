using System;
using ModelLayer.Enums;
using ModelLayer.Helpers;
using ModelLayer.Interfaces;

namespace ModelLayer.Models
{
    /// <summary>
    /// Generic model class for representing a quantity with its unit
    /// </summary>
    /// <typeparam name="TUnit">Unit type that implements IMeasurable</typeparam>
    public class QuantityModel<TUnit> where TUnit : struct, Enum
    {
        private readonly double _value;
        private readonly TUnit _unit;
        private readonly IMeasurable _unitImpl;
        private const double Tolerance = 0.0001;

        public QuantityModel(double value, TUnit unit)
        {
            _unitImpl = UnitResolver.Get(unit);
            if (!_unitImpl.IsValidValue(value))
                throw new ArgumentException($"Invalid value {value} for unit {unit}");
            
            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public TUnit Unit => _unit;
        public IMeasurable UnitImpl => _unitImpl;

        /// <summary>
        /// Converts the quantity to a different unit within the same category
        /// </summary>
        public double ConvertTo(TUnit targetUnit)
        {
            var targetImpl = UnitResolver.Get(targetUnit);
            double baseValue = _unitImpl.ToBaseUnit(_value);
            return targetImpl.FromBaseUnit(baseValue);
        }

        /// <summary>
        /// Creates a new quantity converted to the specified unit
        /// </summary>
        public QuantityModel<TUnit> To(TUnit targetUnit) 
            => new QuantityModel<TUnit>(ConvertTo(targetUnit), targetUnit);

        public override bool Equals(object obj)
        {
            if (obj is not QuantityModel<TUnit> other)
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

        public static bool operator ==(QuantityModel<TUnit> left, QuantityModel<TUnit> right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(QuantityModel<TUnit> left, QuantityModel<TUnit> right) 
            => !(left == right);
    }
}
