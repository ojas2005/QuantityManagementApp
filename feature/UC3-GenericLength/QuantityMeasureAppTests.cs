using NUnit.Framework;

[TestFixture]
public class QuantityMeasurementAppTests
{
    private QuantityMeasurementApp.QuantityLength CreateQuantity(double value, QuantityMeasurementApp.LengthUnit unit)
    {
        return new QuantityMeasurementApp.QuantityLength(value, unit);
    }

    [Test]
    public void TestFeetEquality_SameValue()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.CompareFeetEquality(2.0, 2.0));
    }

    [Test]
    public void TestFeetEquality_DifferentValue()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.CompareFeetEquality(1.0, 3.5));
    }

    [Test]
    public void TestInchesEquality_SameValue()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.CompareInchesEquality(3.5, 3.5));
    }

    [Test]
    public void TestInchesEquality_DifferentValue()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.CompareInchesEquality(2.0, 4.0));
    }

    [Test]
    public void TestEquality_FeetToFeet_SameValue()
    {
        var feet1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(feet1.Equals(feet2));
    }

    [Test]
    public void TestEquality_FeetToFeet_DifferentValue()
    {
        var feet1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(2.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsFalse(feet1.Equals(feet2));
    }

    [Test]
    public void TestEquality_InchToInch_SameValue()
    {
        var inch1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Inch);
        var inch2 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(inch1.Equals(inch2));
    }

    [Test]
    public void TestEquality_InchToInch_DifferentValue()
    {
        var inch1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Inch);
        var inch2 = CreateQuantity(2.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsFalse(inch1.Equals(inch2));
    }

    [Test]
    public void TestEquality_FeetToInch_EquivalentValue()
    {
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(feet.Equals(inches));
    }

    [Test]
    public void TestEquality_InchToFeet_EquivalentValue()
    {
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(inches.Equals(feet));
    }

    [Test]
    public void TestEquality_FeetToInch_DifferentValue()
    {
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsFalse(feet.Equals(inches));
    }

    [Test]
    public void TestEquality_SameReference()
    {
        var quantity = CreateQuantity(3.5, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(quantity.Equals(quantity));
    }

    [Test]
    public void TestEquality_NullComparison()
    {
        var quantity = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsFalse(quantity.Equals(null));
    }

    [Test]
    public void TestEquality_DifferentType()
    {
        var quantity = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var obj = new object();
        Assert.IsFalse(quantity.Equals(obj));
    }

    [Test]
    public void TestEquality_Transitive()
    {
        var feet1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var feet2 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);

        Assert.IsTrue(feet1.Equals(inches));
        Assert.IsTrue(inches.Equals(feet2));
        Assert.IsTrue(feet1.Equals(feet2));
    }

    [Test]
    public void TestOperator_EqualityOperator_Same()
    {
        var feet1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(feet1 == feet2);
    }

    [Test]
    public void TestOperator_EqualityOperator_CrossUnit()
    {
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(feet == inches);
    }

    [Test]
    public void TestOperator_InequalityOperator()
    {
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(feet != inches);
    }

    [Test]
    public void TestValidation_NaNValue()
    {
        Assert.Throws<ArgumentException>(() =>
            CreateQuantity(double.NaN, QuantityMeasurementApp.LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_InfinityValue()
    {
        Assert.Throws<ArgumentException>(() =>
            CreateQuantity(double.PositiveInfinity, QuantityMeasurementApp.LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_NegativeValue()
    {
        Assert.Throws<ArgumentException>(() =>
            CreateQuantity(-5.0, QuantityMeasurementApp.LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_ZeroValue()
    {
        var quantity = CreateQuantity(0.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsNotNull(quantity);
    }

    [Test]
    public void TestService_ConvertUnits()
    {
        double result = QuantityMeasurementApp.QuantityMeasurementService.ConvertUnits(1.0);
        Assert.AreEqual(12.0, result, 0.0001);
    }

    [Test]
    public void TestService_ValidateMeasurementValue_Positive()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(10.5));
    }

    [Test]
    public void TestService_ValidateMeasurementValue_Zero()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(0.0));
    }

    [Test]
    public void TestService_ValidateMeasurementValue_Negative()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(-5.0));
    }

    [Test]
    public void TestService_ValidateMeasurementValue_NaN()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(double.NaN));
    }

    [Test]
    public void TestService_ValidateMeasurementValue_Infinity()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(double.PositiveInfinity));
    }

    [Test]
    public void TestHashCode_EqualObjects_SameHash()
    {
        var feet1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(feet1.GetHashCode(), inches.GetHashCode());
    }

    [Test]
    public void TestToString_FeetRepresentation()
    {
        var feet = CreateQuantity(5.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual("5 ft", feet.ToString());
    }

    [Test]
    public void TestToString_InchRepresentation()
    {
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual("12 in", inches.ToString());
    }
}
