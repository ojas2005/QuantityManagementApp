using NUnit.Framework;
using Moq;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entities;
using System;

namespace QuantityManagementApp.Tests
{
    [TestFixture]
    public class BusinessLayerTests
    {
        private Mock<IQuantityMeasurementRepository> _mockRepo;
        private QuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IQuantityMeasurementRepository>();
            _service = new QuantityMeasurementService(_mockRepo.Object);
        }

        [Test]
        public void GetMeasurementTypeFromUnit_Feet_ReturnsLength()
        {
            // Act
            var result = _service.GetMeasurementTypeFromUnit("ft");
            
            // Assert
            Assert.That(result, Is.EqualTo("Length"));
        }

        [Test]
        public void GetMeasurementTypeFromUnit_Kilogram_ReturnsWeight()
        {
            // Act
            var result = _service.GetMeasurementTypeFromUnit("kg");
            
            // Assert
            Assert.That(result, Is.EqualTo("Weight"));
        }

        [Test]
        public void GetMeasurementTypeFromUnit_Litre_ReturnsVolume()
        {
            // Act
            var result = _service.GetMeasurementTypeFromUnit("L");
            
            // Assert
            Assert.That(result, Is.EqualTo("Volume"));
        }

        [Test]
        public void GetMeasurementTypeFromUnit_Celsius_ReturnsTemperature()
        {
            // Act
            var result = _service.GetMeasurementTypeFromUnit("°C");
            
            // Assert
            Assert.That(result, Is.EqualTo("Temperature"));
        }

        [Test]
        public void AreUnitsCompatible_SameType_ReturnsTrue()
        {
            // Act
            var result = _service.AreUnitsCompatible("ft", "in");
            
            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void AreUnitsCompatible_DifferentType_ReturnsFalse()
        {
            // Act
            var result = _service.AreUnitsCompatible("ft", "kg");
            
            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CompareQuantities_SameValue_ReturnsTrue()
        {
            // Arrange
            var first = new QuantityDTO(1.0, "ft", "Length");
            var second = new QuantityDTO(1.0, "ft", "Length");

            // Act
            var result = _service.CompareQuantities(first, second);

            // Assert
            Assert.That(result.HasError, Is.False);
            Assert.That(result.ResultValue, Is.EqualTo(1));
        }

        [Test]
        public void CompareQuantities_DifferentValue_ReturnsFalse()
        {
            // Arrange
            var first = new QuantityDTO(1.0, "ft", "Length");
            var second = new QuantityDTO(2.0, "ft", "Length");

            // Act
            var result = _service.CompareQuantities(first, second);

            // Assert
            Assert.That(result.HasError, Is.False);
            Assert.That(result.ResultValue, Is.EqualTo(0));
        }

        [Test]
        public void CompareQuantities_DifferentTypes_ThrowsException()
        {
            // Arrange
            var first = new QuantityDTO(1.0, "ft", "Length");
            var second = new QuantityDTO(1.0, "kg", "Weight");

            // Act
            var result = _service.CompareQuantities(first, second);

            // Assert
            Assert.That(result.HasError, Is.True);
            Assert.That(result.ErrorMessage, Does.Contain("different measurement types"));
        }
    }
}
