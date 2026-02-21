using NUnit.Framework;

[TestFixture]
public class QuantityMeasurementAppTests
{
    private QuantityMeasurementApp.QuantityLength CreateQuantity(double value,QuantityMeasurementApp.LengthUnit unit)
    {
        return new QuantityMeasurementApp.QuantityLength(value,unit);
    }
    [Test]
    public void TestFeetEquality_SameValue()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.CompareFeetEquality(2.0,2.0));
    }
    [Test]
    public void TestFeetEquality_DifferentValue()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.CompareFeetEquality(1.0,3.5));
    }
    [Test]
    public void TestInchesEquality_SameValue()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.CompareInchesEquality(3.5,3.5));
    }

    [Test]
    public void TestInchesEquality_DifferentValue()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.CompareInchesEquality(2.0,4.0));
    }

    [Test]
    public void TestEquality_FeetToFeet_SameValue()
    {
        var feet1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(feet1.Equals(feet2));
    }

    [Test]
    public void TestEquality_FeetToFeet_DifferentValue()
    {
        var feet1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsFalse(feet1.Equals(feet2));
    }

    [Test]
    public void TestEquality_InchToInch_SameValue()
    {
        var inch1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Inch);
        var inch2 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(inch1.Equals(inch2));
    }

    [Test]
    public void TestEquality_InchToInch_DifferentValue()
    {
        var inch1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Inch);
        var inch2 = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsFalse(inch1.Equals(inch2));
    }

    [Test]
    public void TestEquality_FeetToInch_EquivalentValue()
    {
        var feet = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(feet.Equals(inches));
    }

    [Test]
    public void TestEquality_InchToFeet_EquivalentValue()
    {
        var inches = CreateQuantity(12.0,QuantityMeasurementApp.LengthUnit.Inch);
        var feet = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(inches.Equals(feet));
    }
    [Test]
    public void TestEquality_FeetToInch_DifferentValue()
    {
        var feet = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsFalse(feet.Equals(inches));
    }
    [Test]
    public void TestEquality_SameReference()
    {
        var quantity = CreateQuantity(3.5,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(quantity.Equals(quantity));
    }
    [Test]
    public void TestEquality_NullComparison()
    {
        var quantity = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsFalse(quantity.Equals(null));
    }
    [Test]
    public void TestEquality_DifferentType()
    {
        var quantity = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var obj = new object();
        Assert.IsFalse(quantity.Equals(obj));
    }
    [Test]
    public void TestEquality_Transitive()
    {
        var feet1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0,QuantityMeasurementApp.LengthUnit.Inch);
        var feet2 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);

        Assert.IsTrue(feet1.Equals(inches));
        Assert.IsTrue(inches.Equals(feet2));
        Assert.IsTrue(feet1.Equals(feet2));
    }

    [Test]
    public void TestOperator_EqualityOperator_Same()
    {
        var feet1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(feet1 == feet2);
    }

    [Test]
    public void TestOperator_EqualityOperator_CrossUnit()
    {
        var feet = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(feet == inches);
    }

    [Test]
    public void TestOperator_InequalityOperator()
    {
        var feet = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(feet != inches);
    }

    [Test]
    public void TestValidation_NaNValue()
    {
        Assert.Throws<ArgumentException>(() =>
            CreateQuantity(double.NaN,QuantityMeasurementApp.LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_InfinityValue()
    {
        Assert.Throws<ArgumentException>(() =>
            CreateQuantity(double.PositiveInfinity,QuantityMeasurementApp.LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_NegativeValue()
    {
        Assert.Throws<ArgumentException>(() =>
            CreateQuantity(-5.0,QuantityMeasurementApp.LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_ZeroValue()
    {
        var quantity = CreateQuantity(0.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsNotNull(quantity);
    }

    [Test]
    public void TestService_ConvertUnits()
    {
        double result = QuantityMeasurementApp.QuantityMeasurementService.ConvertUnits(1.0);
        Assert.AreEqual(12.0,result,0.0001);
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
        var feet1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(feet1.GetHashCode(),inches.GetHashCode());
    }

    [Test]
    public void TestToString_FeetRepresentation()
    {
        var feet = CreateQuantity(5.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual("5 ft",feet.ToString());
    }

    [Test]
    public void TestToString_InchRepresentation()
    {
        var inches = CreateQuantity(12.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual("12 in",inches.ToString());
    }

    // ==================== UC4: Yards Tests ====================
    
    [Test]
    public void TestEquality_YardToYard_SameValue()
    {
        // Verifies that two measurements in yards with the same value are equal
        var yard1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var yard2 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(yard1.Equals(yard2));
    }

    [Test]
    public void TestEquality_YardToYard_DifferentValue()
    {
        // Verifies that two measurements in yards with different values are not equal
        var yard1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var yard2 = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsFalse(yard1.Equals(yard2));
    }

    [Test]
    public void TestEquality_YardToFeet_EquivalentValue()
    {
        // Verifies that 1 yard equals 3 feet (conversion: 1 yard = 3 feet)
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(3.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(yard.Equals(feet));
    }

    [Test]
    public void TestEquality_FeetToYard_EquivalentValue()
    {
        // Verifies symmetry of conversion: 3 feet equals 1 yard
        var feet = CreateQuantity(3.0,QuantityMeasurementApp.LengthUnit.Feet);
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(feet.Equals(yard));
    }

    [Test]
    public void TestEquality_YardToInches_EquivalentValue()
    {
        // Verifies that 1 yard equals 36 inches (conversion: 1 yard = 3 feet = 36 inches)
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var inches = CreateQuantity(36.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(yard.Equals(inches));
    }

    [Test]
    public void TestEquality_InchesToYard_EquivalentValue()
    {
        // Verifies symmetry: 36 inches equals 1 yard
        var inches = CreateQuantity(36.0,QuantityMeasurementApp.LengthUnit.Inch);
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(inches.Equals(yard));
    }

    [Test]
    public void TestEquality_YardToFeet_NonEquivalentValue()
    {
        // Verifies that 1 yard does not equal 2 feet
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsFalse(yard.Equals(feet));
    }

    [Test]
    public void TestEquality_YardWithNullUnit()
    {
        // Verifies that null comparison returns false
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsFalse(yard.Equals(null));
    }

    [Test]
    public void TestEquality_YardSameReference()
    {
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(yard.Equals(yard));
    }

    [Test]
    public void TestEquality_YardNullComparison()
    {
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsFalse(yard.Equals(null));
    }

    [Test]
    public void TestService_CompareYardsEquality_SameValue()
    {
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.CompareYardsEquality(2.0,2.0));
    }

    [Test]
    public void TestService_CompareYardsEquality_DifferentValue()
    {
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.CompareYardsEquality(1.0,2.0));
    }

    [Test]
    public void TestToString_YardRepresentation()
    {
        var yard = CreateQuantity(2.5,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.AreEqual("2.5 yd",yard.ToString());
    }

    //from here uc4 tests starts

    [Test]
    public void TestEquality_CentimeterToCentimeter_SameValue()
    {
        //verifies that two measurements in centimeters with the same value are equal
        var cm1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        var cm2 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsTrue(cm1.Equals(cm2));
    }

    [Test]
    public void TestEquality_CentimeterToCentimeter_DifferentValue()
    {
        //verifies that two measurements in centimeters with different values are not equal
        var cm1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        var cm2 = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsFalse(cm1.Equals(cm2));
    }

    [Test]
    public void TestEquality_CentimeterToInches_EquivalentValue()
    {
        //verifies that 1 cm equals 0.393701 inches (conversion: 1 cm = 0.393701 in)
        var cm = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        var inches = CreateQuantity(0.393701,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(cm.Equals(inches));
    }

    [Test]
    public void TestEquality_InchesToCentimeter_EquivalentValue()
    {
        //verifies symmetry: 0.393701 inches equals 1 centimeter
        var inches = CreateQuantity(0.393701,QuantityMeasurementApp.LengthUnit.Inch);
        var cm = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsTrue(inches.Equals(cm));
    }

    [Test]
    public void TestEquality_CentimeterToFeet_NonEquivalentValue()
    {
        //verifies that 1 centimeter does not equal 1 foot
        var cm = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        var feet = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsFalse(cm.Equals(feet));
    }

    [Test]
    public void TestEquality_CentimetersWithNullUnit()
    {
        //verifies that null comparison returns false
        var cm = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsFalse(cm.Equals(null));
    }

    [Test]
    public void TestEquality_CentimetersSameReference()
    {
        //verifies reflexive property: an object equals itself
        var cm = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsTrue(cm.Equals(cm));
    }

    [Test]
    public void TestEquality_CentimetersNullComparison()
    {
        //verifies that a centimeter object is not equal to null
        var cm = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsFalse(cm.Equals(null));
    }

    [Test]
    public void TestService_CompareCentimetersEquality_SameValue()
    {
        //verifies service method for comparing centimeters
        Assert.IsTrue(QuantityMeasurementApp.QuantityMeasurementService.CompareCentimetersEquality(2.0,2.0));
    }

    [Test]
    public void TestService_CompareCentimetersEquality_DifferentValue()
    {
        //verifies service returns false for different centimeter values
        Assert.IsFalse(QuantityMeasurementApp.QuantityMeasurementService.CompareCentimetersEquality(1.0,2.0));
    }

    [Test]
    public void TestToString_CentimeterRepresentation()
    {
        //verifies string representation of centimeters
        var cm = CreateQuantity(2.5,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.AreEqual("2.5 cm",cm.ToString());
    }

    //multi unit tests

    [Test]
    public void TestEquality_MultiUnit_TransitiveProperty()
    {

        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(3.0,QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(36.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(yard.Equals(feet));    //1 yard=3 feet
        Assert.IsTrue(feet.Equals(inches));  //3 feet=36 inches
        Assert.IsTrue(yard.Equals(inches));  //1 yard=36 inches(transitive property)
    }

    [Test]
    public void TestEquality_AllUnits_ComplexScenario()
    {
        var twoYards = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Yards);
        var sixFeet = CreateQuantity(6.0,QuantityMeasurementApp.LengthUnit.Feet);
        var seventyTwoInches = CreateQuantity(72.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(twoYards.Equals(sixFeet));
        Assert.IsTrue(sixFeet.Equals(seventyTwoInches));
        Assert.IsTrue(twoYards.Equals(seventyTwoInches));
    }

    [Test]
    public void TestService_CompareQuantityEquality_DifferentUnits()
    {
        bool result = QuantityMeasurementApp.QuantityMeasurementService.CompareQuantityEquality(
            1.0,QuantityMeasurementApp.LengthUnit.Yards,
            3.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(result);
    }

    [Test]
    public void TestEquality_Operator_YardComparison()
    {
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(3.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(yard == feet);
    }

    [Test]
    public void TestEquality_Operator_CentimeterComparison()
    {
        var cm = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        var inches = CreateQuantity(0.393701,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(cm == inches);
    }

    [Test]
    public void TestHashCode_YardObjects_SameHash()
    {
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(3.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(yard.GetHashCode(),feet.GetHashCode());
    }

    [Test]
    public void TestHashCode_CentimeterObjects_SameHash()
    {
        var cm = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        var inches = CreateQuantity(0.393701,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(cm.GetHashCode(),inches.GetHashCode());
    }

    [Test]
    public void TestValidation_InvalidUnit_ThrowsException()
    {
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var cm = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsNotNull(yard);
        Assert.IsNotNull(cm);
    }

    [Test]
    public void TestPrecision_CentimeterConversion_MaintainsAccuracy()
    {
        var cm5 = CreateQuantity(5.0,QuantityMeasurementApp.LengthUnit.Centimeters);
        var inches196850 = CreateQuantity(1.96850,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(cm5.Equals(inches196850));
    }
}
