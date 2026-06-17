using NUnit.Framework;
using RepositoryLayer.Repositories;
using RepositoryLayer.Interfaces;
using ModelLayer.Entities;
using System;

namespace QuantityManagementApp.Tests
{
    [TestFixture]
    public class RepositoryLayerTests
    {
        private IQuantityMeasurementRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new QuantityMeasurementCacheRepository();
        }

        [Test]
        public void Save_AddsEntityToCache()
        {
            // Arrange
            var entity = new QuantityMeasurementEntity("TEST", 10.0, "ft", "Length", 10.0, "ft", "Length");

            // Act
            _repository.Save(entity);
            var all = _repository.GetAll();

            // Assert
            Assert.That(all.Count, Is.EqualTo(1));
            Assert.That(all[0].OperationType, Is.EqualTo("TEST"));
        }

        [Test]
        public void GetById_ReturnsCorrectEntity()
        {
            // Arrange
            var entity1 = new QuantityMeasurementEntity("TEST1", 10.0, "ft", "Length", 10.0, "ft", "Length");
            var entity2 = new QuantityMeasurementEntity("TEST2", 20.0, "in", "Length", 20.0, "in", "Length");
            
            _repository.Save(entity1);
            _repository.Save(entity2);

            // Act
            var result = _repository.GetById(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.OperationType, Is.EqualTo("TEST2"));
        }

        [Test]
        public void GetById_InvalidId_ReturnsNull()
        {
            // Act
            var result = _repository.GetById(99);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetAll_ReturnsAllEntities()
        {
            // Arrange
            _repository.Save(new QuantityMeasurementEntity("TEST1", 10.0, "ft", "Length", 10.0, "ft", "Length"));
            _repository.Save(new QuantityMeasurementEntity("TEST2", 20.0, "in", "Length", 20.0, "in", "Length"));

            // Act
            var all = _repository.GetAll();

            // Assert
            Assert.That(all.Count, Is.EqualTo(2));
        }

        [Test]
        public void Clear_RemovesAllEntities()
        {
            // Arrange
            _repository.Save(new QuantityMeasurementEntity("TEST", 10.0, "ft", "Length", 10.0, "ft", "Length"));

            // Act
            _repository.Clear();
            var all = _repository.GetAll();

            // Assert
            Assert.That(all.Count, Is.EqualTo(0));
        }
    }
}
