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

    //uc6 tests - addition of two length units
    
    [Test]
    public void TestAddition_SameUnit_FeetPlusFeet()
    {
        //verifies adding two quantities in feet returns result in feet
        var feet1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(2.0, QuantityMeasurementApp.LengthUnit.Feet);
        var result = QuantityMeasurementApp.QuantityLength.Add(feet1, feet2);
        Assert.AreEqual(3.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_SameUnit_InchPlusInch()
    {
        //verifies adding two quantities in inches returns result in inches
        var inch1 = CreateQuantity(6.0, QuantityMeasurementApp.LengthUnit.Inch);
        var inch2 = CreateQuantity(6.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(inch1, inch2);
        Assert.AreEqual(12.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_FeetPlusInches()
    {
        //verifies cross unit addition returns result in unit of first operand (feet)
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(feet, inches);
        Assert.AreEqual(2.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_InchPlusFeet()
    {
        //verifies cross unit addition returns result in unit of first operand (inches)
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var result = QuantityMeasurementApp.QuantityLength.Add(inches, feet);
        Assert.AreEqual(24.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_YardPlusFeet()
    {
        //verifies cross unit addition with yards returns result in yards
        var yard = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(3.0, QuantityMeasurementApp.LengthUnit.Feet);
        var result = QuantityMeasurementApp.QuantityLength.Add(yard, feet);
        Assert.AreEqual(2.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_CentimeterPlusInch()
    {
        //verifies cross unit addition with centimeters returns result in centimeters
        var cm = CreateQuantity(2.54, QuantityMeasurementApp.LengthUnit.Centimeters);
        var inches = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(cm, inches);
        Assert.IsTrue(Math.Abs(result.Value - 5.08) < 0.01);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Centimeters, result.Unit);
    }

    [Test]
    public void TestAddition_WithZero()
    {
        //verifies adding zero acts as identity element
        var feet = CreateQuantity(5.0, QuantityMeasurementApp.LengthUnit.Feet);
        var zeroInches = CreateQuantity(0.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(feet, zeroInches);
        Assert.AreEqual(5.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_NegativeValues()
    {
        //verifies addition with negative values
        Assert.Throws<ArgumentException>(() =>
        {
            var feet = CreateQuantity(5.0, QuantityMeasurementApp.LengthUnit.Feet);
            var negFeet = CreateQuantity(-2.0, QuantityMeasurementApp.LengthUnit.Feet);
        });
    }

    [Test]
    public void TestAddition_NullFirstOperand()
    {
        //verifies that null first operand throws exception
        var feet = CreateQuantity(5.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.Throws<ArgumentNullException>(() =>
            QuantityMeasurementApp.QuantityLength.Add(null, feet));
    }

    [Test]
    public void TestAddition_NullSecondOperand()
    {
        //verifies that null second operand throws exception
        var feet = CreateQuantity(5.0, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.Throws<ArgumentNullException>(() =>
            QuantityMeasurementApp.QuantityLength.Add(feet, null));
    }

    [Test]
    public void TestAddition_LargeValues()
    {
        //verifies addition with large magnitude values
        var largeFeet1 = CreateQuantity(1e6, QuantityMeasurementApp.LengthUnit.Feet);
        var largeFeet2 = CreateQuantity(1e6, QuantityMeasurementApp.LengthUnit.Feet);
        var result = QuantityMeasurementApp.QuantityLength.Add(largeFeet1, largeFeet2);
        Assert.AreEqual(2e6, result.Value, 1e3);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_SmallValues()
    {
        //verifies addition with small magnitude values
        var smallFeet1 = CreateQuantity(0.001, QuantityMeasurementApp.LengthUnit.Feet);
        var smallFeet2 = CreateQuantity(0.002, QuantityMeasurementApp.LengthUnit.Feet);
        var result = QuantityMeasurementApp.QuantityLength.Add(smallFeet1, smallFeet2);
        Assert.IsTrue(Math.Abs(result.Value - 0.003) < 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_Commutativity_VerifyResultEquality()
    {
        //verifies addition is commutative: add(a,b) equals add(b,a) in terms of actual value
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        
        var result1 = QuantityMeasurementApp.QuantityLength.Add(feet, inches);
        var result2 = QuantityMeasurementApp.QuantityLength.Add(inches, feet);
        
        //result1 should be in feet, result2 in inches, but represent same value
        Assert.IsTrue(result1.Equals(result2));
    }

    [Test]
    public void TestAddition_Service_SameUnit()
    {
        //verifies service method for addition with same units
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            CreateQuantity(2.0, QuantityMeasurementApp.LengthUnit.Feet),
            CreateQuantity(3.0, QuantityMeasurementApp.LengthUnit.Feet));
        Assert.AreEqual(5.0, result.Value, 0.0001);
    }

    [Test]
    public void TestAddition_Service_CrossUnit()
    {
        //verifies service method for addition with cross units
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet),
            CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch));
        Assert.AreEqual(2.0, result.Value, 0.0001);
    }

    [Test]
    public void TestAddition_Service_WithTargetUnit()
    {
        //verifies service method for addition with specified target unit
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            1.0, QuantityMeasurementApp.LengthUnit.Feet,
            12.0, QuantityMeasurementApp.LengthUnit.Inch,
            QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(2.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_Service_WithTargetUnitInches()
    {
        //verifies service method returns result in specified target unit
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            1.0, QuantityMeasurementApp.LengthUnit.Feet,
            12.0, QuantityMeasurementApp.LengthUnit.Inch,
            QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(24.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_Service_WithTargetUnitYards()
    {
        //verifies service method with yards as target unit
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            1.0, QuantityMeasurementApp.LengthUnit.Yards,
            3.0, QuantityMeasurementApp.LengthUnit.Feet,
            QuantityMeasurementApp.LengthUnit.Yards);
        Assert.AreEqual(2.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_Immutability()
    {
        //verifies that original quantities remain unchanged after addition
        var feet1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(2.0, QuantityMeasurementApp.LengthUnit.Feet);
        
        var result = QuantityMeasurementApp.QuantityLength.Add(feet1, feet2);
        
        Assert.AreEqual(1.0, feet1.Value, 0.0001);
        Assert.AreEqual(2.0, feet2.Value, 0.0001);
        Assert.AreEqual(3.0, result.Value, 0.0001);
    }

    //uc7 tests - addition with explicit target unit specification
    
    [Test]
    public void TestAddition_ExplicitTargetUnit_Feet()
    {
        //verifies explicit target unit specification in feet
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(2.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Inches()
    {
        //verifies explicit target unit specification in inches
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(24.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Yards()
    {
        //verifies explicit target unit specification in yards (result unit different from both operands)
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(Math.Abs(result.Value - 0.67) < 0.01);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Centimeters()
    {
        //verifies explicit target unit specification in centimeters
        var inches1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Inch);
        var inches2 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(inches1, inches2, QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsTrue(Math.Abs(result.Value - 5.08) < 0.01);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Centimeters, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_SameAsFirstOperand()
    {
        //verifies target unit explicitly matches first operand unit
        var yards = CreateQuantity(2.0, QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(3.0, QuantityMeasurementApp.LengthUnit.Feet);
        var result = QuantityMeasurementApp.QuantityLength.Add(yards, feet, QuantityMeasurementApp.LengthUnit.Yards);
        Assert.AreEqual(3.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_SameAsSecondOperand()
    {
        //verifies target unit explicitly matches second operand unit
        var yards = CreateQuantity(2.0, QuantityMeasurementApp.LengthUnit.Yards);
        var feet = CreateQuantity(3.0, QuantityMeasurementApp.LengthUnit.Feet);
        var result = QuantityMeasurementApp.QuantityLength.Add(yards, feet, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(9.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Commutativity()
    {
        //verifies commutativity holds with explicit target unit
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        
        var result1 = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Yards);
        var result2 = QuantityMeasurementApp.QuantityLength.Add(inches, feet, QuantityMeasurementApp.LengthUnit.Yards);
        
        Assert.AreEqual(result1.Value, result2.Value, 0.0001);
        Assert.AreEqual(result1.Unit, result2.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_WithZero()
    {
        //verifies zero operand with explicit target unit conversion
        var feet = CreateQuantity(5.0, QuantityMeasurementApp.LengthUnit.Feet);
        var zeroInches = CreateQuantity(0.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(feet, zeroInches, QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(Math.Abs(result.Value - 1.67) < 0.01);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_NegativeValues()
    {
        //verifies negative values with explicit target unit
        Assert.Throws<ArgumentException>(() =>
        {
            var feet = CreateQuantity(5.0, QuantityMeasurementApp.LengthUnit.Feet);
            var negFeet = CreateQuantity(-2.0, QuantityMeasurementApp.LengthUnit.Feet);
        });
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_NullTargetUnit()
    {
        //verifies null target unit throws exception
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.Throws<ArgumentNullException>(() =>
            QuantityMeasurementApp.QuantityLength.Add(feet,inches,null));
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_LargeToSmallScale()
    {
        //verifies addition with result converted to smaller scale unit
        var largeFeet1 = CreateQuantity(1000.0, QuantityMeasurementApp.LengthUnit.Feet);
        var largeFeet2 = CreateQuantity(500.0, QuantityMeasurementApp.LengthUnit.Feet);
        var result = QuantityMeasurementApp.QuantityLength.Add(largeFeet1, largeFeet2, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(18000.0, result.Value, 1.0);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_SmallToLargeScale()
    {
        //verifies addition with result converted to larger scale unit
        var inches1 = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var inches2 = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var result = QuantityMeasurementApp.QuantityLength.Add(inches1, inches2, QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(Math.Abs(result.Value - 0.67) < 0.01);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_AllUnitCombinations()
    {
        //comprehensive test covering all unit pair additions with multiple target units
        var feet = CreateQuantity(6.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        var yards = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Yards);
        var centimeters = CreateQuantity(30.48, QuantityMeasurementApp.LengthUnit.Centimeters);
        
        var result1 = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(7.0, result1.Value, 0.0001);
        
        var result2 = QuantityMeasurementApp.QuantityLength.Add(yards, feet, QuantityMeasurementApp.LengthUnit.Yards);
        Assert.AreEqual(3.0, result2.Value, 0.0001);
        
        var result3 = QuantityMeasurementApp.QuantityLength.Add(centimeters, inches, QuantityMeasurementApp.LengthUnit.Centimeters);
        Assert.IsTrue(Math.Abs(result3.Value - 60.96) < 0.01);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_PrecisionTolerance()
    {
        //verifies epsilon-based comparison for floating-point precision across conversions
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        
        var resultInFeet = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Feet);
        var resultInInches = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Inch);
        
        double feetInInches = resultInFeet.ConvertTo(QuantityMeasurementApp.LengthUnit.Inch);
        Assert.IsTrue(Math.Abs(feetInInches - resultInInches.Value) < 0.01);
    }

    [Test]
    public void TestAddition_Service_ExplicitTargetUnit_Feet()
    {
        //verifies service method with explicit target unit in feet
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet),
            CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch),
            QuantityMeasurementApp.LengthUnit.Feet);
        Assert.AreEqual(2.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_Service_ExplicitTargetUnit_Inches()
    {
        //verifies service method with explicit target unit in inches
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet),
            CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch),
            QuantityMeasurementApp.LengthUnit.Inch);
        Assert.AreEqual(24.0, result.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_Service_ExplicitTargetUnit_Yards()
    {
        //verifies service method with explicit target unit in yards
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet),
            CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch),
            QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(Math.Abs(result.Value - 0.67) < 0.01);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_Service_RawValues_ExplicitTargetUnit()
    {
        //verifies service method with raw values and explicit target unit
        var result = QuantityMeasurementApp.QuantityMeasurementService.Add(
            1.0, QuantityMeasurementApp.LengthUnit.Feet,
            12.0, QuantityMeasurementApp.LengthUnit.Inch,
            QuantityMeasurementApp.LengthUnit.Yards);
        Assert.IsTrue(Math.Abs(result.Value - 0.67) < 0.01);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_Service_ExplicitTargetUnit_NullTargetUnit()
    {
        //verifies service method throws exception for null target unit
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        Assert.Throws<ArgumentNullException>(() =>
            QuantityMeasurementApp.QuantityMeasurementService.Add(feet, inches, null));
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Immutability()
    {
        //verifies original quantities remain unchanged after addition with explicit target unit
        var feet1 = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var feet2 = CreateQuantity(2.0, QuantityMeasurementApp.LengthUnit.Feet);
        
        var result = QuantityMeasurementApp.QuantityLength.Add(feet1, feet2, QuantityMeasurementApp.LengthUnit.Inch);
        
        Assert.AreEqual(1.0, feet1.Value, 0.0001);
        Assert.AreEqual(2.0, feet2.Value, 0.0001);
        Assert.AreEqual(QuantityMeasurementApp.LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_EquivalentResults()
    {
        //verifies same addition yields equivalent results when expressed in different target units
        var feet = CreateQuantity(1.0, QuantityMeasurementApp.LengthUnit.Feet);
        var inches = CreateQuantity(12.0, QuantityMeasurementApp.LengthUnit.Inch);
        
        var resultInFeet = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Feet);
        var resultInInches = QuantityMeasurementApp.QuantityLength.Add(feet, inches, QuantityMeasurementApp.LengthUnit.Inch);
        
        double resultInInchesConvertedToFeet = resultInInches.ConvertTo(QuantityMeasurementApp.LengthUnit.Feet);
        Assert.IsTrue(Math.Abs(resultInFeet.Value - resultInInchesConvertedToFeet) < 0.01);
    }
}
