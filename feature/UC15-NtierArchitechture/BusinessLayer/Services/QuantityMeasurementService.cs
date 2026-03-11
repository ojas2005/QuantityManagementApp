using System;
using ModelLayer.DTOs;
using ModelLayer.Entities;
using ModelLayer.Enums;
using ModelLayer.Exceptions;
using ModelLayer.Models;
using ModelLayer.Helpers;
using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Implementation of quantity measurement business logic
    /// </summary>
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementService(IQuantityMeasurementRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #region Public Methods

        public QuantityMeasurementEntity CompareQuantities(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                ValidateInputs(first, second);
                ValidateCategoryCompatibility(first, second);

                var firstModel = CreateQuantityModel(first);
                var secondModel = CreateQuantityModel(second);

                bool areEqual = firstModel.Equals(secondModel);
                double resultValue = areEqual ? 1 : 0;

                var entity = new QuantityMeasurementEntity(
                    "COMPARE",
                    first.Value, first.Unit, first.MeasurementType,
                    second.Value, second.Unit, second.MeasurementType,
                    resultValue, null, null);

                _repository.Save(entity);
                return entity;
            }
            catch (Exception ex)
            {
                return HandleException("COMPARE", ex);
            }
        }

        public QuantityMeasurementEntity ConvertQuantity(QuantityDTO source, string targetUnit)
        {
            try
            {
                ValidateInput(source);
                
                if (string.IsNullOrEmpty(targetUnit))
                    throw new ArgumentException("Target unit cannot be null or empty");

                string measurementType = GetMeasurementTypeFromUnit(source.Unit);
                string targetMeasurementType = GetMeasurementTypeFromUnit(targetUnit);

                if (measurementType != targetMeasurementType)
                    throw new QuantityMeasurementException(
                        $"Cannot convert between different measurement types: {measurementType} and {targetMeasurementType}");

                var sourceModel = CreateQuantityModel(source);
                double result = ConvertUsingModel(sourceModel, targetUnit);

                var entity = new QuantityMeasurementEntity(
                    "CONVERT",
                    source.Value, source.Unit, measurementType,
                    result, targetUnit, measurementType);

                _repository.Save(entity);
                return entity;
            }
            catch (Exception ex)
            {
                return HandleException("CONVERT", ex);
            }
        }

        public QuantityMeasurementEntity AddQuantities(QuantityDTO first, QuantityDTO second)
        {
            return AddQuantities(first, second, first.Unit);
        }

        public QuantityMeasurementEntity AddQuantities(QuantityDTO first, QuantityDTO second, string targetUnit)
        {
            try
            {
                ValidateInputs(first, second);
                ValidateCategoryCompatibility(first, second);
                ValidateArithmeticSupport(first, "ADD");

                var firstModel = CreateQuantityModel(first);
                var secondModel = CreateQuantityModel(second);

                double baseResult = PerformBaseArithmetic(firstModel, secondModel, (a, b) => a + b);
                double result = ConvertFromBase(firstModel, baseResult, targetUnit);

                var entity = new QuantityMeasurementEntity(
                    "ADD",
                    first.Value, first.Unit, first.MeasurementType,
                    second.Value, second.Unit, second.MeasurementType,
                    Math.Round(result, 2), targetUnit, first.MeasurementType);

                _repository.Save(entity);
                return entity;
            }
            catch (Exception ex)
            {
                return HandleException("ADD", ex);
            }
        }

        public QuantityMeasurementEntity SubtractQuantities(QuantityDTO first, QuantityDTO second)
        {
            return SubtractQuantities(first, second, first.Unit);
        }

        public QuantityMeasurementEntity SubtractQuantities(QuantityDTO first, QuantityDTO second, string targetUnit)
        {
            try
            {
                ValidateInputs(first, second);
                ValidateCategoryCompatibility(first, second);
                ValidateArithmeticSupport(first, "SUBTRACT");

                var firstModel = CreateQuantityModel(first);
                var secondModel = CreateQuantityModel(second);

                double baseResult = PerformBaseArithmetic(firstModel, secondModel, (a, b) => a - b);
                double result = ConvertFromBase(firstModel, baseResult, targetUnit);

                var entity = new QuantityMeasurementEntity(
                    "SUBTRACT",
                    first.Value, first.Unit, first.MeasurementType,
                    second.Value, second.Unit, second.MeasurementType,
                    Math.Round(result, 2), targetUnit, first.MeasurementType);

                _repository.Save(entity);
                return entity;
            }
            catch (Exception ex)
            {
                return HandleException("SUBTRACT", ex);
            }
        }

        public QuantityMeasurementEntity DivideQuantities(QuantityDTO first, QuantityDTO second)
        {
            try
            {
                ValidateInputs(first, second);
                ValidateCategoryCompatibility(first, second);
                ValidateArithmeticSupport(first, "DIVIDE");

                var firstModel = CreateQuantityModel(first);
                var secondModel = CreateQuantityModel(second);

                // Get the value from the model
                double secondValue = GetModelValue(secondModel);
                
                if (Math.Abs(secondValue) < 0.0001)
                    throw new DivideByZeroException("Cannot divide by zero quantity");

                double result = PerformBaseArithmetic(firstModel, secondModel, (a, b) => a / b);

                var entity = new QuantityMeasurementEntity(
                    "DIVIDE",
                    first.Value, first.Unit, first.MeasurementType,
                    second.Value, second.Unit, second.MeasurementType,
                    Math.Round(result, 4), null, null);

                _repository.Save(entity);
                return entity;
            }
            catch (Exception ex)
            {
                return HandleException("DIVIDE", ex);
            }
        }

        public string GetMeasurementTypeFromUnit(string unit)
        {
            return unit switch
            {
                "ft" or "in" or "yd" or "cm" => "Length",
                "kg" or "g" or "lb" => "Weight",
                "L" or "mL" or "gal" => "Volume",
                "°C" or "°F" => "Temperature",
                _ => throw new QuantityMeasurementException($"Unknown unit: {unit}")
            };
        }

        public bool AreUnitsCompatible(string unit1, string unit2)
        {
            string type1 = GetMeasurementTypeFromUnit(unit1);
            string type2 = GetMeasurementTypeFromUnit(unit2);
            return type1 == type2;
        }

        #endregion

        #region Private Helper Methods

        private object CreateQuantityModel(QuantityDTO dto)
        {
            string measurementType = GetMeasurementTypeFromUnit(dto.Unit);

            return measurementType switch
            {
                "Length" => new QuantityModel<LengthUnit>(dto.Value, GetLengthUnit(dto.Unit)),
                "Weight" => new QuantityModel<WeightUnit>(dto.Value, GetWeightUnit(dto.Unit)),
                "Volume" => new QuantityModel<VolumeUnit>(dto.Value, GetVolumeUnit(dto.Unit)),
                "Temperature" => new QuantityModel<TemperatureUnit>(dto.Value, GetTemperatureUnit(dto.Unit)),
                _ => throw new QuantityMeasurementException($"Unknown measurement type: {measurementType}")
            };
        }

        private LengthUnit GetLengthUnit(string unit) => unit switch
        {
            "ft" => LengthUnit.Feet,
            "in" => LengthUnit.Inch,
            "yd" => LengthUnit.Yards,
            "cm" => LengthUnit.Centimeters,
            _ => throw new QuantityMeasurementException($"Invalid length unit: {unit}")
        };

        private WeightUnit GetWeightUnit(string unit) => unit switch
        {
            "kg" => WeightUnit.Kilogram,
            "g" => WeightUnit.Gram,
            "lb" => WeightUnit.Pound,
            _ => throw new QuantityMeasurementException($"Invalid weight unit: {unit}")
        };

        private VolumeUnit GetVolumeUnit(string unit) => unit switch
        {
            "L" => VolumeUnit.Litre,
            "mL" => VolumeUnit.Millilitre,
            "gal" => VolumeUnit.Gallon,
            _ => throw new QuantityMeasurementException($"Invalid volume unit: {unit}")
        };

        private TemperatureUnit GetTemperatureUnit(string unit) => unit switch
        {
            "°C" => TemperatureUnit.Celsius,
            "°F" => TemperatureUnit.Fahrenheit,
            _ => throw new QuantityMeasurementException($"Invalid temperature unit: {unit}")
        };

        private double ConvertUsingModel(object model, string targetUnit)
        {
            return model switch
            {
                QuantityModel<LengthUnit> length => length.ConvertTo(GetLengthUnit(targetUnit)),
                QuantityModel<WeightUnit> weight => weight.ConvertTo(GetWeightUnit(targetUnit)),
                QuantityModel<VolumeUnit> volume => volume.ConvertTo(GetVolumeUnit(targetUnit)),
                QuantityModel<TemperatureUnit> temp => temp.ConvertTo(GetTemperatureUnit(targetUnit)),
                _ => throw new QuantityMeasurementException("Unknown model type")
            };
        }

        private double PerformBaseArithmetic(object first, object second, Func<double, double, double> operation)
        {
            double firstBase = GetBaseValue(first);
            double secondBase = GetBaseValue(second);
            return operation(firstBase, secondBase);
        }

        private double GetBaseValue(object model)
        {
            return model switch
            {
                QuantityModel<LengthUnit> length => length.UnitImpl.ToBaseUnit(length.Value),
                QuantityModel<WeightUnit> weight => weight.UnitImpl.ToBaseUnit(weight.Value),
                QuantityModel<VolumeUnit> volume => volume.UnitImpl.ToBaseUnit(volume.Value),
                QuantityModel<TemperatureUnit> temp => temp.UnitImpl.ToBaseUnit(temp.Value),
                _ => throw new QuantityMeasurementException("Unknown model type")
            };
        }

        private double GetModelValue(object model)
        {
            return model switch
            {
                QuantityModel<LengthUnit> length => length.Value,
                QuantityModel<WeightUnit> weight => weight.Value,
                QuantityModel<VolumeUnit> volume => volume.Value,
                QuantityModel<TemperatureUnit> temp => temp.Value,
                _ => throw new QuantityMeasurementException("Unknown model type")
            };
        }

        private double ConvertFromBase(object model, double baseValue, string targetUnit)
        {
            string measurementType = GetMeasurementTypeFromUnit(targetUnit);
            
            // If target unit is the same as model's unit
            if (model is QuantityModel<LengthUnit> length && targetUnit == length.Unit.ToString().ToLower())
                return length.UnitImpl.FromBaseUnit(baseValue);
            if (model is QuantityModel<WeightUnit> weight && targetUnit == GetUnitString(weight.Unit))
                return weight.UnitImpl.FromBaseUnit(baseValue);
            if (model is QuantityModel<VolumeUnit> volume && targetUnit == GetUnitString(volume.Unit))
                return volume.UnitImpl.FromBaseUnit(baseValue);
            if (model is QuantityModel<TemperatureUnit> temp && targetUnit == GetUnitString(temp.Unit))
                return temp.UnitImpl.FromBaseUnit(baseValue);

            // Convert to target unit
            return measurementType switch
            {
                "Length" => new QuantityModel<LengthUnit>(0, GetLengthUnit(targetUnit)).UnitImpl.FromBaseUnit(baseValue),
                "Weight" => new QuantityModel<WeightUnit>(0, GetWeightUnit(targetUnit)).UnitImpl.FromBaseUnit(baseValue),
                "Volume" => new QuantityModel<VolumeUnit>(0, GetVolumeUnit(targetUnit)).UnitImpl.FromBaseUnit(baseValue),
                "Temperature" => new QuantityModel<TemperatureUnit>(0, GetTemperatureUnit(targetUnit)).UnitImpl.FromBaseUnit(baseValue),
                _ => throw new QuantityMeasurementException($"Unknown measurement type: {measurementType}")
            };
        }

        private string GetUnitString(Enum unit)
        {
            return unit switch
            {
                LengthUnit.Feet => "ft",
                LengthUnit.Inch => "in",
                LengthUnit.Yards => "yd",
                LengthUnit.Centimeters => "cm",
                WeightUnit.Kilogram => "kg",
                WeightUnit.Gram => "g",
                WeightUnit.Pound => "lb",
                VolumeUnit.Litre => "L",
                VolumeUnit.Millilitre => "mL",
                VolumeUnit.Gallon => "gal",
                TemperatureUnit.Celsius => "°C",
                TemperatureUnit.Fahrenheit => "°F",
                _ => unit.ToString().ToLower()
            };
        }

        private void ValidateInput(QuantityDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Quantity cannot be null");

            if (string.IsNullOrEmpty(dto.Unit))
                throw new ArgumentException("Unit cannot be null or empty");

            if (string.IsNullOrEmpty(dto.MeasurementType))
                dto.MeasurementType = GetMeasurementTypeFromUnit(dto.Unit);
        }

        private void ValidateInputs(QuantityDTO first, QuantityDTO second)
        {
            ValidateInput(first);
            ValidateInput(second);
        }

        private void ValidateCategoryCompatibility(QuantityDTO first, QuantityDTO second)
        {
            if (first.MeasurementType != second.MeasurementType)
                throw new QuantityMeasurementException(
                    $"Cannot operate on different measurement types: {first.MeasurementType} and {second.MeasurementType}");
        }

        private void ValidateArithmeticSupport(QuantityDTO dto, string operation)
        {
            var model = CreateQuantityModel(dto);
            
            bool supportsArithmetic = model switch
            {
                QuantityModel<TemperatureUnit> temp => temp.UnitImpl.HasArithmeticSupport(),
                _ => true
            };

            if (!supportsArithmetic)
            {
                if (model is QuantityModel<TemperatureUnit> tempModel)
                {
                    tempModel.UnitImpl.ValidateOperationSupport(operation);
                }
            }
        }

        private QuantityMeasurementEntity HandleException(string operation, Exception ex)
        {
            string message = ex switch
            {
                ArgumentNullException => $"Invalid input: {ex.Message}",
                ArgumentException => $"Invalid argument: {ex.Message}",
                DivideByZeroException => ex.Message,
                QuantityMeasurementException => ex.Message,
                NotSupportedException => ex.Message,
                _ => $"Unexpected error: {ex.Message}"
            };

            return new QuantityMeasurementEntity(operation, message);
        }

        #endregion
    }
}
