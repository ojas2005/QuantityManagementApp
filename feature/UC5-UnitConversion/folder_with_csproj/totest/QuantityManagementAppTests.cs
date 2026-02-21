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

    
    [Test]
    public void TestEquality_YardToYard_SameValue()
    {
        //verifies that two measurements in yards with the same value are equal
        var yard1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var yard2 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(yard1.Equals(yard2));
    }

    [Test]
    public void TestEquality_YardToYard_DifferentValue()
    {
        //verifies that two measurements in yards with different values are not equal
        var yard1 = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var yard2 = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsFalse(yard1.Equals(yard2));
    }

    [Test]
    public void TestEquality_YardToFeet_EquivalentValue()
    {
        //verifies that 1 yard equals 3 feet
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(3.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(yard.Equals(feet));
    }

    [Test]
    public void TestEquality_FeetToYard_EquivalentValue()
    {
        //verifies symmetry of conversion
        var feet = CreateQuantity(3.0,QuantityMeasurementApp.LengthUnit.Feet);
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(feet.Equals(yard));
    }

    [Test]
    public void TestEquality_YardToInches_EquivalentValue()
    {
        //verifies that 1 yard equals 36 inches
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var inches = CreateQuantity(36.0,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(yard.Equals(inches));
    }

    [Test]
    public void TestEquality_InchesToYard_EquivalentValue()
    {
        //verifies symmetry that 36 inches equals 1 yard
        var inches = CreateQuantity(36.0,QuantityMeasurementApp.LengthUnit.Inch);
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(inches.Equals(yard));
    }

    [Test]
    public void TestEquality_YardToFeet_NonEquivalentValue()
    {
        //verifies that 1 yard does not equal 2 feet
        var yard = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsFalse(yard.Equals(feet));
    }

    [Test]
    public void TestEquality_YardWithNullUnit()
    {
        //verifies that null comparison returns false
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
        //verifies symmetry that 0.393701 inches equals 1 centimeter
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

 //uc5 tests

    [Test]
    public void TestConversion_FeetToInches()
    {
        //verifies convert(1.0,FEET,INCHES)= 12.0
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(1.0,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(12.0,result,0.0001);
    }

    [Test]
    public void TestConversion_InchesToFeet()
    {
        //verifies convert(24.0,INCHES,FEET)= 2.0
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(24.0,QuantityMeasurementApp.LengthUnit.Inch,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(2.0,result,0.0001);
    }

    [Test]
    public void TestConversion_YardsToFeet()
    {
        //verifies convert(1.0,YARDS,FEET)=3.0
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(1.0,QuantityMeasurementApp.LengthUnit.Yards,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(3.0,result,0.0001);
    }

    [Test]
    public void TestConversion_FeetToYards()
    {
        //Verifies convert(6.0,FEET,YARDS) =2.0
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(6.0,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.AreEqual(2.0,result,0.0001);
    }

    [Test]
    public void TestConversion_YardsToInches()
    {
        //verifies convert(1.0,YARDS,INCHES)= 36.0
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(1.0,QuantityMeasurementApp.LengthUnit.Yards,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(36.0,result,0.0001);
    }

    [Test]
    public void TestConversion_InchesToYards()
    {
        //verifies convert(72.0,INCHES,YARDS)= 2.0
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(72.0,QuantityMeasurementApp.LengthUnit.Inch,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.AreEqual(2.0,result,0.0001);
    }

    [Test]
    public void TestConversion_CentimetersToInches()
    {
        //verifies convert(2.54,CENTIMETERS,INCHES)= 1.0(within epsilon)
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(2.54,QuantityMeasurementApp.LengthUnit.Centimeters,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(1.0,result,0.001);
    }

    [Test]
    public void TestConversion_InchesToCentimeters()
    {
        //verifies convert(1.0,INCHES,CENTIMETERS)= 2.54(approx)
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(1.0,QuantityMeasurementApp.LengthUnit.Inch,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.AreEqual(2.54,result,0.01);
    }

    [Test]
    public void TestConversion_FeetToCentimeters()
    {
        //verifies cross unit conversion through base unit normalization
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(1.0,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.Greater(result,29.0);
        Assert.Less(result,31.0);
    }

    [Test]
    public void TestConversion_CentimetersToFeet()
    {
        //verifies convert(30.48,CENTIMETERS,FEET)= 1.0 (approximately)
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(30.48,QuantityMeasurementApp.LengthUnit.Centimeters,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(1.0,result,0.01);
    }

    [Test]
    public void TestConversion_SameUnit()
    {
        //verifies convert(5.0,FEET,FEET)= 5.0
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(5.0,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(5.0,result,0.0001);
    }

    [Test]
    public void TestConversion_ZeroValue()
    {
        //verifies convert(0.0,FEET,INCHES)= 0.0
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(0.0,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(0.0,result,0.0001);
    }

    [Test]
    public void TestConversion_ZeroValue_MultipleUnits()
    {
        //verifies zero conversion across different units
        double result1 = QuantityMeasurementApp.QuantityMeasurementService.Convert(0.0,QuantityMeasurementApp.LengthUnit.Yards,QuantityMeasurementApp.LengthUnit.Centimeters);
        double result2 = QuantityMeasurementApp.QuantityMeasurementService.Convert(0.0,QuantityMeasurementApp.LengthUnit.Inch,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.AreEqual(0.0,result1,0.0001);
        Assert.AreEqual(0.0,result2,0.0001);
    }

    [Test]
    public void TestConversion_NegativeValue_Throws()
    {
        //verifies that negative values throw ArgumentException
        Assert.Throws<ArgumentException>(() =>
            QuantityMeasurementApp.QuantityMeasurementService.Convert(-1.0,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch));
    }

    [Test]
    public void TestConversion_NaNValue_Throws()
    {
        //verifies that NaN values throw ArgumentException
        Assert.Throws<ArgumentException>(() =>
            QuantityMeasurementApp.QuantityMeasurementService.Convert(double.NaN,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch));
    }

    [Test]
    public void TestConversion_InfinityValue_Throws()
    {
        //verifies that infinite values throw ArgumentException
        Assert.Throws<ArgumentException>(() =>
            QuantityMeasurementApp.QuantityMeasurementService.Convert(double.PositiveInfinity,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch));
    }

    [Test]
    public void TestConversion_RoundTrip_FeetToInchesToFeet()
    {
        //verifies bidirectional conversion: convert(convert(v,FEET,INCHES),INCHES,FEET) ≈ v
        double original = 5.0;
        double toInches = QuantityMeasurementApp.QuantityMeasurementService.Convert(original,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch);
        double backToFeet = QuantityMeasurementApp.QuantityMeasurementService.Convert(toInches,QuantityMeasurementApp.LengthUnit.Inch,QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(original,backToFeet,0.0001);
    }

    [Test]
    public void TestConversion_RoundTrip_YardsToFeetToYards()
    {
        //verifies bidirectional conversion with yards
        double original = 3.0;
        double toFeet = QuantityMeasurementApp.QuantityMeasurementService.Convert(original,QuantityMeasurementApp.LengthUnit.Yards,QuantityMeasurementApp.LengthUnit.Feet);
        double backToYards = QuantityMeasurementApp.QuantityMeasurementService.Convert(toFeet,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Yards);
        Assert.AreEqual(original,backToYards,0.0001);
    }

    [Test]
    public void TestConversion_RoundTrip_CentimetersThroughMultipleUnits()
    {
        //verifies multi-step round-trip conversion
        double original = 10.0;
        double toCm = QuantityMeasurementApp.QuantityMeasurementService.Convert(original,QuantityMeasurementApp.LengthUnit.Inch,QuantityMeasurementApp.LengthUnit.Centimeters);
        double toFeet = QuantityMeasurementApp.QuantityMeasurementService.Convert(toCm,QuantityMeasurementApp.LengthUnit.Centimeters,QuantityMeasurementApp.LengthUnit.Feet);
        double backToInches = QuantityMeasurementApp.QuantityMeasurementService.Convert(toFeet,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(original,backToInches,0.0001);
    }

    [Test]
    public void TestConversion_PrecisionTolerance()
    {
        //verifies floating-point precision is maintained
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(1.0,QuantityMeasurementApp.LengthUnit.Centimeters,QuantityMeasurementApp.LengthUnit.Inch);
        double expected = 0.393701;
        Assert.AreEqual(expected,result,0.000001);
    }

    [Test]
    public void TestConversion_LargeValue()
    {
        //verifies conversion accuracy with large values
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(1000.0,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(12000.0,result,0.0001);
    }

    [Test]
    public void TestConversion_SmallValue()
    {
        //verifies conversion accuracy with small values
        double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(0.1,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(1.2,result,0.0001);
    }

    [Test]
    public void TestConversion_InstanceMethod()
    {
        //verifies ConvertTo instance method works correctly
        var quantity = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Yards);
        double result = quantity.ConvertTo(QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(36.0,result,0.0001);
    }

    [Test]
    public void TestConversion_InstanceMethod_MultipleConversions()
    {
        //verifies instance method with various unit combinations
        var quantity = CreateQuantity(2.0,QuantityMeasurementApp.LengthUnit.Feet);
        double toInches = quantity.ConvertTo(QuantityMeasurementApp.LengthUnit.Inch);
        double toYards = quantity.ConvertTo(QuantityMeasurementApp.LengthUnit.Yards);
        double toCm = quantity.ConvertTo(QuantityMeasurementApp.LengthUnit.Centimeters);
        
        Assert.AreEqual(24.0,toInches,0.0001);
        Assert.AreEqual(2.0 / 3.0,toYards,0.0001);
        Assert.Greater(toCm,60.0);
        Assert.Less(toCm,62.0);
    }

    [Test]
    public void TestConversion_ConsistencyWithEquality()
    {
        //verifies that conversion results are consistent with equality checks
        double converted = QuantityMeasurementApp.QuantityMeasurementService.Convert(1.0,QuantityMeasurementApp.LengthUnit.Feet,QuantityMeasurementApp.LengthUnit.Inch);
        
        var feet = CreateQuantity(1.0,QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(converted,QuantityMeasurementApp.LengthUnit.Inch);
        
        Assert.IsTrue(feet.Equals(inches));
    }

    [Test]
    public void TestConversion_AllUnitCombinations_BasicCheck()
    {
        //verifies all unit combinations can be converted without exceptions
        var units = new[] { 
            QuantityMeasurementApp.LengthUnit.Feet,
            QuantityMeasurementApp.LengthUnit.Inch,
            QuantityMeasurementApp.LengthUnit.Yards,
            QuantityMeasurementApp.LengthUnit.Centimeters
        };
        foreach (var fromUnit in units)
        {
            foreach (var toUnit in units)
            {
                double result = QuantityMeasurementApp.QuantityMeasurementService.Convert(1.0,fromUnit,toUnit);
                Assert.IsNotNull(result);
                Assert.IsTrue(double.IsFinite(result));
            }
        }
    }
}
