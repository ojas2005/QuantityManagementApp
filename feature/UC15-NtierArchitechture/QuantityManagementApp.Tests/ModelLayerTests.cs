using NUnit.Framework;
using ModelLayer.DTOs;
using ModelLayer.Entities;
using ModelLayer.Enums;
using ModelLayer.Exceptions;
using ModelLayer.Models;
using ModelLayer.Helpers;
using System;

namespace QuantityManagementApp.Tests
{
    [TestFixture]
    public class ModelLayerTests
    {
        #region QuantityModel Tests

        [Test]
        public void QuantityModel_Construction_ValidValue_Success()
        {
            // Act
            var model = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);

            // Assert
            Assert.That(model.Value, Is.EqualTo(5.0));
            Assert.That(model.Unit, Is.EqualTo(LengthUnit.Feet));
        }

        [Test]
        public void QuantityModel_Construction_InvalidValue_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                new QuantityModel<LengthUnit>(double.NaN, LengthUnit.Feet));
        }

        [Test]
        public void QuantityModel_ConvertTo_SameUnit_ReturnsSameValue()
        {
            // Arrange
            var model = new QuantityModel<LengthUnit>(5.0, LengthUnit.Feet);

            // Act
            var result = model.ConvertTo(LengthUnit.Feet);

            // Assert
            Assert.That(result, Is.EqualTo(5.0).Within(0.001));
        }

        [Test]
        public void QuantityModel_ConvertTo_DifferentUnit_ReturnsConvertedValue()
        {
            // Arrange
            var model = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);

            // Act
            var result = model.ConvertTo(LengthUnit.Inch);

            // Assert
            Assert.That(result, Is.EqualTo(12.0).Within(0.001));
        }

        [Test]
        public void QuantityModel_Equals_SameUnitSameValue_ReturnsTrue()
        {
            // Arrange
            var model1 = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);
            var model2 = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);

            // Act & Assert
            Assert.That(model1.Equals(model2), Is.True);
        }

        [Test]
        public void QuantityModel_Equals_DifferentUnitEquivalentValue_ReturnsTrue()
        {
            // Arrange
            var model1 = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);
            var model2 = new QuantityModel<LengthUnit>(12.0, LengthUnit.Inch);

            // Act & Assert
            Assert.That(model1.Equals(model2), Is.True);
        }

        [Test]
        public void QuantityModel_Equals_DifferentValue_ReturnsFalse()
        {
            // Arrange
            var model1 = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);
            var model2 = new QuantityModel<LengthUnit>(2.0, LengthUnit.Feet);

            // Act & Assert
            Assert.That(model1.Equals(model2), Is.False);
        }

        [Test]
        public void QuantityModel_Equals_Null_ReturnsFalse()
        {
            // Arrange
            var model = new QuantityModel<LengthUnit>(1.0, LengthUnit.Feet);

            // Act & Assert
            Assert.That(model.Equals(null), Is.False);
        }

        [Test]
        public void QuantityModel_ToString_ReturnsFormattedString()
        {
            // Arrange
            var model = new QuantityModel<LengthUnit>(5.5, LengthUnit.Feet);

            // Act
            var result = model.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("5.5 ft"));
        }

        #endregion

        #region QuantityDTO Tests

        [Test]
        public void QuantityDTO_DefaultConstructor_SetsProperties()
        {
            // Act
            var dto = new QuantityDTO();

            // Assert
            Assert.That(dto.Value, Is.EqualTo(0.0));
            Assert.That(dto.Unit, Is.EqualTo(string.Empty));
            Assert.That(dto.MeasurementType, Is.EqualTo(string.Empty));
        }

        [Test]
        public void QuantityDTO_ParameterizedConstructor_SetsProperties()
        {
            // Act
            var dto = new QuantityDTO(5.0, "ft", "Length");

            // Assert
            Assert.That(dto.Value, Is.EqualTo(5.0));
            Assert.That(dto.Unit, Is.EqualTo("ft"));
            Assert.That(dto.MeasurementType, Is.EqualTo("Length"));
        }

        [Test]
        public void QuantityDTO_ToString_ReturnsFormattedString()
        {
            // Arrange
            var dto = new QuantityDTO(5.5, "ft", "Length");

            // Act
            var result = dto.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("5.5 ft"));
        }

        #endregion

        #region QuantityMeasurementEntity Tests

        [Test]
        public void QuantityMeasurementEntity_ConversionConstructor_SetsProperties()
        {
            // Act
            var entity = new QuantityMeasurementEntity(
                "CONVERT",
                5.0, "ft", "Length",
                60.0, "in", "Length");

            // Assert
            Assert.That(entity.OperationType, Is.EqualTo("CONVERT"));
            Assert.That(entity.FirstValue, Is.EqualTo(5.0));
            Assert.That(entity.FirstUnit, Is.EqualTo("ft"));
            Assert.That(entity.FirstMeasurementType, Is.EqualTo("Length"));
            Assert.That(entity.ResultValue, Is.EqualTo(60.0));
            Assert.That(entity.ResultUnit, Is.EqualTo("in"));
            Assert.That(entity.HasError, Is.False);
        }

        [Test]
        public void QuantityMeasurementEntity_BinaryConstructor_SetsProperties()
        {
            // Act
            var entity = new QuantityMeasurementEntity(
                "ADD",
                5.0, "ft", "Length",
                12.0, "in", "Length",
                6.0, "ft", "Length");

            // Assert
            Assert.That(entity.OperationType, Is.EqualTo("ADD"));
            Assert.That(entity.FirstValue, Is.EqualTo(5.0));
            Assert.That(entity.FirstUnit, Is.EqualTo("ft"));
            Assert.That(entity.SecondValue, Is.EqualTo(12.0));
            Assert.That(entity.SecondUnit, Is.EqualTo("in"));
            Assert.That(entity.ResultValue, Is.EqualTo(6.0));
            Assert.That(entity.ResultUnit, Is.EqualTo("ft"));
            Assert.That(entity.HasError, Is.False);
        }

        [Test]
        public void QuantityMeasurementEntity_ErrorConstructor_SetsProperties()
        {
            // Act
            var entity = new QuantityMeasurementEntity("ADD", "Invalid operation");

            // Assert
            Assert.That(entity.OperationType, Is.EqualTo("ADD"));
            Assert.That(entity.ErrorMessage, Is.EqualTo("Invalid operation"));
            Assert.That(entity.HasError, Is.True);
        }

        [Test]
        public void QuantityMeasurementEntity_ToString_CompareOperation_FormatsCorrectly()
        {
            // Arrange
            var entity = new QuantityMeasurementEntity(
                "COMPARE",
                5.0, "ft", "Length",
                60.0, "in", "Length",
                1.0, null, null);

            // Act
            var result = entity.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("5 ft == 60 in? True"));
        }

        [Test]
        public void QuantityMeasurementEntity_ToString_ConvertOperation_FormatsCorrectly()
        {
            // Arrange
            var entity = new QuantityMeasurementEntity(
                "CONVERT",
                5.0, "ft", "Length",
                60.0, "in", "Length");

            // Act
            var result = entity.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("5 ft = 60 in"));
        }

        [Test]
        public void QuantityMeasurementEntity_ToString_AddOperation_FormatsCorrectly()
        {
            // Arrange
            var entity = new QuantityMeasurementEntity(
                "ADD",
                5.0, "ft", "Length",
                12.0, "in", "Length",
                6.0, "ft", "Length");

            // Act
            var result = entity.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("5 ft + 12 in = 6 ft"));
        }

        [Test]
        public void QuantityMeasurementEntity_ToString_Error_FormatsCorrectly()
        {
            // Arrange
            var entity = new QuantityMeasurementEntity("ADD", "Invalid operation");

            // Act
            var result = entity.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("[ERROR] ADD: Invalid operation"));
        }

        #endregion

        #region UnitResolver Tests

        [Test]
        public void UnitResolver_Get_LengthUnit_ReturnsImplementation()
        {
            // Act
            var result = UnitResolver.Get(LengthUnit.Feet);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Label, Is.EqualTo("ft"));
        }

        [Test]
        public void UnitResolver_Get_WeightUnit_ReturnsImplementation()
        {
            // Act
            var result = UnitResolver.Get(WeightUnit.Kilogram);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Label, Is.EqualTo("kg"));
        }

        [Test]
        public void UnitResolver_Get_VolumeUnit_ReturnsImplementation()
        {
            // Act
            var result = UnitResolver.Get(VolumeUnit.Litre);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Label, Is.EqualTo("L"));
        }

        [Test]
        public void UnitResolver_Get_TemperatureUnit_ReturnsImplementation()
        {
            // Act
            var result = UnitResolver.Get(TemperatureUnit.Celsius);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Label, Is.EqualTo("°C"));
        }

        #endregion
    }
}
