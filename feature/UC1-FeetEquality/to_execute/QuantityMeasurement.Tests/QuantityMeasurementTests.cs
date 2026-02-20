using NUnit.Framework;
using QuantityMeasurement;

namespace QuantityMeasurement.Tests
{
    [TestFixture]
    public class QuantityMeasurementAppTests
    {
        [Test]
        public void TestEquality_SameValue_ReturnsTrue()
        {
            var feet1 = new QuantityMeasurementApp.Feet(1.0);
            var feet2 = new QuantityMeasurementApp.Feet(1.0);
            Assert.IsTrue(feet1.Equals(feet2));
        }

        [Test]
        public void TestEquality_DifferentValue_ReturnsFalse()
        {
            var feet1 = new QuantityMeasurementApp.Feet(1.0);
            var feet2 = new QuantityMeasurementApp.Feet(2.0);
            Assert.IsFalse(feet1.Equals(feet2));
        }

        [Test]
        public void TestEquality_NullComparison_ReturnsFalse()
        {
            var feet = new QuantityMeasurementApp.Feet(1.0);
            Assert.IsFalse(feet.Equals(null));
        }

        [Test]
        public void TestEquality_SameReference_ReturnsTrue()
        {
            var feet = new QuantityMeasurementApp.Feet(5.0);
            Assert.IsTrue(feet.Equals(feet));
        }

        [Test]
        public void TestEquality_DifferentType_ReturnsFalse()
        {
            var feet = new QuantityMeasurementApp.Feet(1.0);
            var obj = new object();
            Assert.IsFalse(feet.Equals(obj));
        }

        [Test]
        public void TestEquality_WithinTolerance_ReturnsTrue()
        {
            var feet1 = new QuantityMeasurementApp.Feet(5.0);
            var feet2 = new QuantityMeasurementApp.Feet(5.00005);
            Assert.IsTrue(feet1.Equals(feet2));
        }

        [Test]
        public void TestEquality_SymmetricProperty_ReturnsTrue()
        {
            var feet1 = new QuantityMeasurementApp.Feet(3.5);
            var feet2 = new QuantityMeasurementApp.Feet(3.5);
            Assert.IsTrue(feet1.Equals(feet2));
            Assert.IsTrue(feet2.Equals(feet1));
        }

        [Test]
        public void TestEquality_TransitiveProperty_ReturnsTrue()
        {
            var feet1 = new QuantityMeasurementApp.Feet(2.0);
            var feet2 = new QuantityMeasurementApp.Feet(2.0);
            var feet3 = new QuantityMeasurementApp.Feet(2.0);
            Assert.IsTrue(feet1.Equals(feet2) && feet2.Equals(feet3) && feet1.Equals(feet3));
        }

        [Test]
        public void TestOperator_EqualityOperator_ReturnsTrue()
        {
            var feet1 = new QuantityMeasurementApp.Feet(4.0);
            var feet2 = new QuantityMeasurementApp.Feet(4.0);
            Assert.IsTrue(feet1==feet2);
        }

        [Test]
        public void TestOperator_InequalityOperator_ReturnsTrue()
        {
            var feet1 = new QuantityMeasurementApp.Feet(4.0);
            var feet2 = new QuantityMeasurementApp.Feet(5.0);
            Assert.IsTrue(feet1!=feet2);
        }

        [Test]
        public void TestValue_CanRetrieveValue_ReturnsCorrectValue()
        {
            double expectedValue = 7.5;
            var feet = new QuantityMeasurementApp.Feet(expectedValue);
            Assert.AreEqual(expectedValue,feet.Value);
        }

        [Test]
        public void TestGetHashCode_SameValues_HaveSameHash()
        {
            var feet1 = new QuantityMeasurementApp.Feet(1.0);
            var feet2 = new QuantityMeasurementApp.Feet(1.0);
            Assert.AreEqual(feet1.GetHashCode(),feet2.GetHashCode());
        }
    }
}