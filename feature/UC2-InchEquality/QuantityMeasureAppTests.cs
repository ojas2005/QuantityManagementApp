using NUnit.Framework;

[TestFixture]
public class QuantityMeasurementAppTests
{
    //feet equality tests
    [Test]
    public void testFeetEquality_SameValue()
    {
        var f1 = new QuantityMeasurementApp.Feet(1.0);
        var f2 = new QuantityMeasurementApp.Feet(1.0);
        Assert.IsTrue(f1.Equals(f2));
    }
    [Test]
    public void testFeetEquality_DifferentValue()
    {
        var f1 = new QuantityMeasurementApp.Feet(1.0);
        var f2 = new QuantityMeasurementApp.Feet(2.0);
        Assert.IsFalse(f1.Equals(f2));
    }
    [Test]
    public void testFeetEquality_NullComparison()
    {
        var f = new QuantityMeasurementApp.Feet(1.0);
        Assert.IsFalse(f.Equals(null));
    }
    [Test]
    public void testFeetEquality_NonNumericInput()
    {
        var f = new QuantityMeasurementApp.Feet(1.0);
        var obj = new object();
        Assert.IsFalse(f.Equals(obj));
    }
    [Test]
    public void testFeetEquality_SameReference()
    {
        var f = new QuantityMeasurementApp.Feet(3.5);
        Assert.IsTrue(f.Equals(f));
    }

    //inches equality tests
    [Test]
    public void testInchesEquality_SameValue()
    {
        var i1 = new QuantityMeasurementApp.Inches(1.0);
        var i2 = new QuantityMeasurementApp.Inches(1.0);
        Assert.IsTrue(i1.Equals(i2));
    }
    [Test]
    public void testInchesEquality_DifferentValue()
    {
        var i1 = new QuantityMeasurementApp.Inches(1.0);
        var i2 = new QuantityMeasurementApp.Inches(2.5);
        Assert.IsFalse(i1.Equals(i2));
    }
    [Test]
    public void testInchesEquality_NullComparison()
    {
        var i = new QuantityMeasurementApp.Inches(2.0);
        Assert.IsFalse(i.Equals(null));
    }
    [Test]
    public void testInchesEquality_NonNumericInput()
    {
        var i = new QuantityMeasurementApp.Inches(1.0);
        var obj = new object();
        Assert.IsFalse(i.Equals(obj));
    }
    [Test]
    public void testInchesEquality_SameReference()
    {
        var i = new QuantityMeasurementApp.Inches(4.5);
        Assert.IsTrue(i.Equals(i));
    }
    
    //service tests
    [Test]
    public void testService_CompareFeetEquality()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.CompareFeetEquality(2.0, 2.0));
    }
    [Test]
    public void testService_CompareFeetInequality()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.CompareFeetEquality(1.0, 3.5));
    }
    [Test]
    public void testService_CompareInchesEquality()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.CompareInchesEquality(3.5, 3.5));
    }
    [Test]
    public void testService_CompareInchesInequality()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.CompareInchesEquality(2.0, 4.0));
    }
    [Test]
    public void testService_ConvertUnits()
    {
        double result = QuantityMeasurementApp.QuantityMeasurementService.ConvertUnits(1.0);
        Assert.AreEqual(12.0, result, 0.0001);
    }
    [Test]
    public void testService_ValidateMeasurementValue_Positive()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(10.5));
    }
    [Test]
    public void testService_ValidateMeasurementValue_Zero()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(0.0));
    }
    [Test]
    public void testService_ValidateMeasurementValue_Negative()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(-5.0));
    }
    [Test]
    public void testService_ValidateMeasurementValue_NaN()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(double.NaN));
    }
    [Test]
    public void testService_ValidateMeasurementValue_Infinity()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.ValidateMeasurementValue(double.PositiveInfinity));
    }
}