using NUnit.Framework;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using RepositoryLayer.Repositories;
using RepositoryLayer.Interfaces;
using ModelLayer.DTOs;
using System;

namespace QuantityManagementApp.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private IQuantityMeasurementRepository _repository;
        private IQuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _repository = new QuantityMeasurementCacheRepository();
            _service = new QuantityMeasurementService(_repository);
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
        public void ConvertQuantity_FeetToInches_ReturnsCorrectValue()
        {
            // Arrange
            var source = new QuantityDTO(1.0, "ft", "Length");

            // Act
            var result = _service.ConvertQuantity(source, "in");

            // Assert
            Assert.That(result.HasError, Is.False);
            Assert.That(result.ResultValue, Is.EqualTo(12.0).Within(0.001));
        }

        [Test]
        public void ConvertQuantity_KilogramToGram_ReturnsCorrectValue()
        {
            // Arrange
            var source = new QuantityDTO(1.0, "kg", "Weight");

            // Act
            var result = _service.ConvertQuantity(source, "g");

            // Assert
            Assert.That(result.HasError, Is.False);
            Assert.That(result.ResultValue, Is.EqualTo(1000.0).Within(0.001));
        }

        [Test]
        public void AddQuantities_FeetAndInches_ReturnsCorrectValue()
        {
            // Arrange
            var first = new QuantityDTO(1.0, "ft", "Length");
            var second = new QuantityDTO(12.0, "in", "Length");

            // Act
            var result = _service.AddQuantities(first, second);

            // Assert
            Assert.That(result.HasError, Is.False);
            Assert.That(result.ResultValue, Is.EqualTo(2.0).Within(0.001));
        }

        [Test]
        public void SubtractQuantities_FeetAndInches_ReturnsCorrectValue()
        {
            // Arrange
            var first = new QuantityDTO(10.0, "ft", "Length");
            var second = new QuantityDTO(6.0, "in", "Length");

            // Act
            var result = _service.SubtractQuantities(first, second);

            // Assert
            Assert.That(result.HasError, Is.False);
            Assert.That(result.ResultValue, Is.EqualTo(9.5).Within(0.001));
        }

        [Test]
        public void DivideQuantities_FeetAndFeet_ReturnsRatio()
        {
            // Arrange
            var first = new QuantityDTO(10.0, "ft", "Length");
            var second = new QuantityDTO(2.0, "ft", "Length");

            // Act
            var result = _service.DivideQuantities(first, second);

            // Assert
            Assert.That(result.HasError, Is.False);
            Assert.That(result.ResultValue, Is.EqualTo(5.0).Within(0.001));
        }
    }
}
