using NUnit.Framework;
using System;

[TestFixture]
public class QuantityMeasurementAppTests
{
    private Quantity<LengthUnit> CreateLength(double value, LengthUnit unit)
    {
        return new Quantity<LengthUnit>(value, unit);
    }

    private Quantity<WeightUnit> CreateWeight(double value, WeightUnit unit)
    {
        return new Quantity<WeightUnit>(value, unit);
    }

    // UC1, UC2, UC3, UC4 tests (equality)
    [Test]
    public void TestFeetEquality_SameValue()
    {
        Assert.IsTrue(CreateLength(2.0, LengthUnit.Feet).Equals(CreateLength(2.0, LengthUnit.Feet)));
    }

    [Test]
    public void TestFeetEquality_DifferentValue()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(3.5, LengthUnit.Feet)));
    }

    [Test]
    public void TestInchesEquality_SameValue()
    {
        Assert.IsTrue(CreateLength(3.5, LengthUnit.Inch).Equals(CreateLength(3.5, LengthUnit.Inch)));
    }

    [Test]
    public void TestInchesEquality_DifferentValue()
    {
        Assert.IsFalse(CreateLength(2.0, LengthUnit.Inch).Equals(CreateLength(4.0, LengthUnit.Inch)));
    }

    [Test]
    public void TestEquality_FeetToFeet_SameValue()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(1.0, LengthUnit.Feet)));
    }

    [Test]
    public void TestEquality_FeetToFeet_DifferentValue()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(2.0, LengthUnit.Feet)));
    }

    [Test]
    public void TestEquality_InchToInch_SameValue()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Inch).Equals(CreateLength(1.0, LengthUnit.Inch)));
    }

    [Test]
    public void TestEquality_InchToInch_DifferentValue()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Inch).Equals(CreateLength(2.0, LengthUnit.Inch)));
    }

    [Test]
    public void TestEquality_FeetToInch_EquivalentValue()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(12.0, LengthUnit.Inch)));
    }

    [Test]
    public void TestEquality_InchToFeet_EquivalentValue()
    {
        Assert.IsTrue(CreateLength(12.0, LengthUnit.Inch).Equals(CreateLength(1.0, LengthUnit.Feet)));
    }

    [Test]
    public void TestEquality_FeetToInch_DifferentValue()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(1.0, LengthUnit.Inch)));
    }

    [Test]
    public void TestEquality_SameReference()
    {
        var q = CreateLength(3.5, LengthUnit.Feet);
        Assert.IsTrue(q.Equals(q));
    }

    [Test]
    public void TestEquality_NullComparison()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Feet).Equals(null));
    }

    [Test]
    public void TestEquality_DifferentType()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Feet).Equals(new object()));
    }

    [Test]
    public void TestEquality_Transitive()
    {
        var f = CreateLength(1.0, LengthUnit.Feet);
        var i = CreateLength(12.0, LengthUnit.Inch);
        var f2 = CreateLength(1.0, LengthUnit.Feet);
        Assert.IsTrue(f.Equals(i) && i.Equals(f2) && f.Equals(f2));
    }

    // UC3 yards
    [Test]
    public void TestEquality_YardToYard_SameValue()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(1.0, LengthUnit.Yards)));
    }

    [Test]
    public void TestEquality_YardToYard_DifferentValue()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(2.0, LengthUnit.Yards)));
    }

    [Test]
    public void TestEquality_YardToFeet_EquivalentValue()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(3.0, LengthUnit.Feet)));
    }

    [Test]
    public void TestEquality_FeetToYard_EquivalentValue()
    {
        Assert.IsTrue(CreateLength(3.0, LengthUnit.Feet).Equals(CreateLength(1.0, LengthUnit.Yards)));
    }

    [Test]
    public void TestEquality_YardToInches_EquivalentValue()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(36.0, LengthUnit.Inch)));
    }

    [Test]
    public void TestEquality_InchesToYard_EquivalentValue()
    {
        Assert.IsTrue(CreateLength(36.0, LengthUnit.Inch).Equals(CreateLength(1.0, LengthUnit.Yards)));
    }

    [Test]
    public void TestEquality_YardToFeet_NonEquivalentValue()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(2.0, LengthUnit.Feet)));
    }

    // UC4 centimeters
    [Test]
    public void TestEquality_CentimeterToCentimeter_SameValue()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Centimeters).Equals(CreateLength(1.0, LengthUnit.Centimeters)));
    }

    [Test]
    public void TestEquality_CentimeterToCentimeter_DifferentValue()
    {
        Assert.IsFalse(CreateLength(1.0, LengthUnit.Centimeters).Equals(CreateLength(2.0, LengthUnit.Centimeters)));
    }

    [Test]
    public void TestEquality_CentimeterToInches_EquivalentValue()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Centimeters).Equals(CreateLength(0.393701, LengthUnit.Inch)));
    }

    [Test]
    public void TestEquality_InchesToCentimeter_EquivalentValue()
    {
        Assert.IsTrue(CreateLength(0.393701, LengthUnit.Inch).Equals(CreateLength(1.0, LengthUnit.Centimeters)));
    }

    // Operator tests
    [Test]
    public void TestOperator_EqualityOperator_Same()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Feet) == CreateLength(1.0, LengthUnit.Feet));
    }

    [Test]
    public void TestOperator_EqualityOperator_CrossUnit()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Feet) == CreateLength(12.0, LengthUnit.Inch));
    }

    [Test]
    public void TestOperator_InequalityOperator()
    {
        Assert.IsTrue(CreateLength(1.0, LengthUnit.Feet) != CreateLength(1.0, LengthUnit.Inch));
    }

    // Validation
    [Test]
    public void TestValidation_NaNValue()
    {
        Assert.Throws<ArgumentException>(() => CreateLength(double.NaN, LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_InfinityValue()
    {
        Assert.Throws<ArgumentException>(() => CreateLength(double.PositiveInfinity, LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_NegativeValue()
    {
        Assert.Throws<ArgumentException>(() => CreateLength(-5.0, LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_ZeroValue()
    {
        Assert.DoesNotThrow(() => CreateLength(0.0, LengthUnit.Feet));
    }

    // HashCode & ToString
    [Test]
    public void TestHashCode_EqualObjects_SameHash()
    {
        var f = CreateLength(1.0, LengthUnit.Feet);
        var i = CreateLength(12.0, LengthUnit.Inch);
        Assert.AreEqual(f.GetHashCode(), i.GetHashCode());
    }

    [Test]
    public void TestToString_FeetRepresentation()
    {
        Assert.AreEqual("5 ft", CreateLength(5.0, LengthUnit.Feet).ToString());
    }

    [Test]
    public void TestToString_InchRepresentation()
    {
        Assert.AreEqual("12 in", CreateLength(12.0, LengthUnit.Inch).ToString());
    }

    [Test]
    public void TestToString_YardRepresentation()
    {
        Assert.AreEqual("2.5 yd", CreateLength(2.5, LengthUnit.Yards).ToString());
    }

    [Test]
    public void TestToString_CentimeterRepresentation()
    {
        Assert.AreEqual("2.5 cm", CreateLength(2.5, LengthUnit.Centimeters).ToString());
    }

    // UC5 conversion tests
    [Test]
    public void TestConversion_FeetToInches()
    {
        Assert.AreEqual(12.0, CreateLength(1.0, LengthUnit.Feet).ConvertTo(LengthUnit.Inch), 0.0001);
    }

    [Test]
    public void TestConversion_InchesToFeet()
    {
        Assert.AreEqual(2.0, CreateLength(24.0, LengthUnit.Inch).ConvertTo(LengthUnit.Feet), 0.0001);
    }

    [Test]
    public void TestConversion_YardsToFeet()
    {
        Assert.AreEqual(3.0, CreateLength(1.0, LengthUnit.Yards).ConvertTo(LengthUnit.Feet), 0.0001);
    }

    [Test]
    public void TestConversion_FeetToYards()
    {
        Assert.AreEqual(2.0, CreateLength(6.0, LengthUnit.Feet).ConvertTo(LengthUnit.Yards), 0.0001);
    }

    [Test]
    public void TestConversion_YardsToInches()
    {
        Assert.AreEqual(36.0, CreateLength(1.0, LengthUnit.Yards).ConvertTo(LengthUnit.Inch), 0.0001);
    }

    [Test]
    public void TestConversion_InchesToYards()
    {
        Assert.AreEqual(2.0, CreateLength(72.0, LengthUnit.Inch).ConvertTo(LengthUnit.Yards), 0.0001);
    }

    [Test]
    public void TestConversion_CentimetersToInches()
    {
        Assert.AreEqual(1.0, CreateLength(2.54, LengthUnit.Centimeters).ConvertTo(LengthUnit.Inch), 0.001);
    }

    [Test]
    public void TestConversion_InchesToCentimeters()
    {
        Assert.AreEqual(2.54, CreateLength(1.0, LengthUnit.Inch).ConvertTo(LengthUnit.Centimeters), 0.01);
    }

    [Test]
    public void TestConversion_FeetToCentimeters()
    {
        Assert.Greater(CreateLength(1.0, LengthUnit.Feet).ConvertTo(LengthUnit.Centimeters), 29.0);
    }

    [Test]
    public void TestConversion_CentimetersToFeet()
    {
        Assert.AreEqual(1.0, CreateLength(30.48, LengthUnit.Centimeters).ConvertTo(LengthUnit.Feet), 0.01);
    }

    [Test]
    public void TestConversion_SameUnit()
    {
        Assert.AreEqual(5.0, CreateLength(5.0, LengthUnit.Feet).ConvertTo(LengthUnit.Feet), 0.0001);
    }

    [Test]
    public void TestConversion_ZeroValue()
    {
        Assert.AreEqual(0.0, CreateLength(0.0, LengthUnit.Feet).ConvertTo(LengthUnit.Inch), 0.0001);
    }

    [Test]
    public void TestConversion_NegativeValue_Throws()
    {
        Assert.Throws<ArgumentException>(() => CreateLength(-1.0, LengthUnit.Feet));
    }

    [Test]
    public void TestConversion_RoundTrip_FeetToInchesToFeet()
    {
        var original = CreateLength(5.0, LengthUnit.Feet);
        var inInches = original.To(LengthUnit.Inch);
        var back = inInches.To(LengthUnit.Feet);
        Assert.AreEqual(original.Value, back.Value, 0.0001);
    }

    // UC6 addition tests
    [Test]
    public void TestAddition_SameUnit_FeetPlusFeet()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(2.0, LengthUnit.Feet));
        Assert.AreEqual(3.0, result.Value);
        Assert.AreEqual(LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_FeetPlusInches()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(12.0, LengthUnit.Inch));
        Assert.AreEqual(2.0, result.Value);
        Assert.AreEqual(LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_InchPlusFeet()
    {
        var result = CreateLength(12.0, LengthUnit.Inch).Add(CreateLength(1.0, LengthUnit.Feet));
        Assert.AreEqual(24.0, result.Value);
        Assert.AreEqual(LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_YardPlusFeet()
    {
        var result = CreateLength(1.0, LengthUnit.Yards).Add(CreateLength(3.0, LengthUnit.Feet));
        Assert.AreEqual(2.0, result.Value);
        Assert.AreEqual(LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_WithZero()
    {
        var result = CreateLength(5.0, LengthUnit.Feet).Add(CreateLength(0.0, LengthUnit.Inch));
        Assert.AreEqual(5.0, result.Value);
    }

    [Test]
    public void TestAddition_NullFirstOperand_Throws()
    {
        var validQuantity = CreateLength(1.0, LengthUnit.Feet);
        // Fixed: Use AddQuantities static method correctly
        Assert.Throws<ArgumentNullException>(() => Quantity<LengthUnit>.AddQuantities(null, validQuantity));
    }

    [Test]
    public void TestAddition_NullSecondOperand_Throws()
    {
        var validQuantity = CreateLength(1.0, LengthUnit.Feet);
        // Fixed: This uses instance method, which is correct
        Assert.Throws<ArgumentNullException>(() => validQuantity.Add(null));
    }

    [Test]
    public void TestAddition_Commutativity()
    {
        var a = CreateLength(1.0, LengthUnit.Feet);
        var b = CreateLength(12.0, LengthUnit.Inch);
        var r1 = a.Add(b);
        var r2 = b.Add(a);
        Assert.IsTrue(r1.Equals(r2));
    }

    // UC7 addition with explicit target unit
    [Test]
    public void TestAddition_ExplicitTargetUnit_Feet()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(12.0, LengthUnit.Inch), LengthUnit.Feet);
        Assert.AreEqual(2.0, result.Value);
        Assert.AreEqual(LengthUnit.Feet, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Inches()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(12.0, LengthUnit.Inch), LengthUnit.Inch);
        Assert.AreEqual(24.0, result.Value);
        Assert.AreEqual(LengthUnit.Inch, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Yards()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(12.0, LengthUnit.Inch), LengthUnit.Yards);
        Assert.AreEqual(2.0 / 3.0, result.Value, 0.01);
        Assert.AreEqual(LengthUnit.Yards, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_NullTarget_Throws()
    {

    }

    // Weight tests (UC9)
    [Test]
    public void TestEquality_KilogramToKilogram_SameValue()
    {
        Assert.IsTrue(CreateWeight(1.0, WeightUnit.Kilogram).Equals(CreateWeight(1.0, WeightUnit.Kilogram)));
    }

    [Test]
    public void TestEquality_KilogramToGram_EquivalentValue()
    {
        Assert.IsTrue(CreateWeight(1.0, WeightUnit.Kilogram).Equals(CreateWeight(1000.0, WeightUnit.Gram)));
    }

    [Test]
    public void TestEquality_KilogramToPound_EquivalentValue()
    {
        Assert.IsTrue(CreateWeight(1.0, WeightUnit.Kilogram).Equals(CreateWeight(2.20462, WeightUnit.Pound)));
    }

    [Test]
    public void TestEquality_WeightVsLength_Incompatible()
    {
        Assert.IsFalse(CreateWeight(1.0, WeightUnit.Kilogram).Equals(CreateLength(1.0, LengthUnit.Feet)));
    }

    [Test]
    public void TestEquality_WeightNullComparison()
    {
        Assert.IsFalse(CreateWeight(1.0, WeightUnit.Kilogram).Equals(null));
    }

    [Test]
    public void TestConversion_KilogramToGram()
    {
        Assert.AreEqual(1000.0, CreateWeight(1.0, WeightUnit.Kilogram).ConvertTo(WeightUnit.Gram), 0.0001);
    }

    [Test]
    public void TestConversion_KilogramToPound()
    {
        Assert.AreEqual(2.20462, CreateWeight(1.0, WeightUnit.Kilogram).ConvertTo(WeightUnit.Pound), 0.001);
    }

    [Test]
    public void TestConversion_GramToKilogram()
    {
        Assert.AreEqual(1.0, CreateWeight(1000.0, WeightUnit.Gram).ConvertTo(WeightUnit.Kilogram), 0.0001);
    }

    [Test]
    public void TestAddition_SameUnit_KilogramPlusKilogram()
    {
        var result = CreateWeight(1.0, WeightUnit.Kilogram).Add(CreateWeight(2.0, WeightUnit.Kilogram));
        Assert.AreEqual(3.0, result.Value);
        Assert.AreEqual(WeightUnit.Kilogram, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_KilogramPlusGram()
    {
        var result = CreateWeight(1.0, WeightUnit.Kilogram).Add(CreateWeight(1000.0, WeightUnit.Gram));
        Assert.AreEqual(2.0, result.Value);
        Assert.AreEqual(WeightUnit.Kilogram, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Gram()
    {
        var result = CreateWeight(1.0, WeightUnit.Kilogram).Add(CreateWeight(1000.0, WeightUnit.Gram), WeightUnit.Gram);
        Assert.AreEqual(2000.0, result.Value);
        Assert.AreEqual(WeightUnit.Gram, result.Unit);
    }

    [Test]
    public void TestWeightValidation_NegativeAllowed()
    {
        Assert.DoesNotThrow(() => CreateWeight(-1.0, WeightUnit.Kilogram));
    }

    [Test]
    public void TestWeightToString()
    {
        Assert.AreEqual("5.5 kg", CreateWeight(5.5, WeightUnit.Kilogram).ToString());
    }

    // Volume unit tests

    private Quantity<VolumeUnit> CreateVolume(double value, VolumeUnit unit)
    {
        return new Quantity<VolumeUnit>(value, unit);
    }

    // Equality Tests
    [Test]
    public void TestEquality_LitreToLitre_SameValue()
    {
        Assert.IsTrue(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateVolume(1.0, VolumeUnit.Litre)));
    }

    [Test]
    public void TestEquality_LitreToLitre_DifferentValue()
    {
        Assert.IsFalse(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateVolume(2.0, VolumeUnit.Litre)));
    }

    [Test]
    public void TestEquality_MillilitreToMillilitre_SameValue()
    {
        Assert.IsTrue(CreateVolume(1000.0, VolumeUnit.Millilitre).Equals(CreateVolume(1000.0, VolumeUnit.Millilitre)));
    }

    [Test]
    public void TestEquality_GallonToGallon_SameValue()
    {
        Assert.IsTrue(CreateVolume(1.0, VolumeUnit.Gallon).Equals(CreateVolume(1.0, VolumeUnit.Gallon)));
    }

    [Test]
    public void TestEquality_LitreToMillilitre_EquivalentValue()
    {
        Assert.IsTrue(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateVolume(1000.0, VolumeUnit.Millilitre)));
    }

    [Test]
    public void TestEquality_MillilitreToLitre_EquivalentValue()
    {
        Assert.IsTrue(CreateVolume(1000.0, VolumeUnit.Millilitre).Equals(CreateVolume(1.0, VolumeUnit.Litre)));
    }

    [Test]
    public void TestEquality_LitreToGallon_EquivalentValue()
    {
        Assert.IsTrue(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateVolume(0.264172, VolumeUnit.Gallon)));
    }

    [Test]
    public void TestEquality_GallonToLitre_EquivalentValue()
    {
        Assert.IsTrue(CreateVolume(1.0, VolumeUnit.Gallon).Equals(CreateVolume(3.78541, VolumeUnit.Litre)));
    }

    [Test]
    public void TestEquality_MillilitreToGallon_EquivalentValue()
    {
        Assert.IsTrue(CreateVolume(1000.0, VolumeUnit.Millilitre).Equals(CreateVolume(0.264172, VolumeUnit.Gallon)));
    }

    [Test]
    public void TestEquality_VolumeVsLength_Incompatible()
    {
        Assert.IsFalse(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateLength(1.0, LengthUnit.Feet)));
    }

    [Test]
    public void TestEquality_VolumeVsWeight_Incompatible()
    {
        Assert.IsFalse(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateWeight(1.0, WeightUnit.Kilogram)));
    }

    [Test]
    public void TestEquality_VolumeNullComparison()
    {
        Assert.IsFalse(CreateVolume(1.0, VolumeUnit.Litre).Equals(null));
    }

    [Test]
    public void TestEquality_SameReference()
    {
        var vol = CreateVolume(3.5, VolumeUnit.Litre);
        Assert.IsTrue(vol.Equals(vol));
    }

    [Test]
    public void TestEquality_TransitiveProperty()
    {
        var a = CreateVolume(1.0, VolumeUnit.Litre);
        var b = CreateVolume(1000.0, VolumeUnit.Millilitre);
        var c = CreateVolume(1.0, VolumeUnit.Litre);
        Assert.IsTrue(a.Equals(b) && b.Equals(c) && a.Equals(c));
    }

    [Test]
    public void TestEquality_ZeroValue()
    {
        Assert.IsTrue(CreateVolume(0.0, VolumeUnit.Litre).Equals(CreateVolume(0.0, VolumeUnit.Millilitre)));
    }

    [Test]
    public void TestEquality_NegativeVolume()
    {
        Assert.IsTrue(CreateVolume(-1.0, VolumeUnit.Litre).Equals(CreateVolume(-1000.0, VolumeUnit.Millilitre)));
    }

    [Test]
    public void TestEquality_LargeVolumeValue()
    {
        Assert.IsTrue(CreateVolume(1000000.0, VolumeUnit.Millilitre).Equals(CreateVolume(1000.0, VolumeUnit.Litre)));
    }

    [Test]
    public void TestEquality_SmallVolumeValue()
    {
        Assert.IsTrue(CreateVolume(0.001, VolumeUnit.Litre).Equals(CreateVolume(1.0, VolumeUnit.Millilitre)));
    }

    // Conversion Tests
    [Test]
    public void TestConversion_LitreToMillilitre()
    {
        Assert.AreEqual(1000.0, CreateVolume(1.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Millilitre), 0.0001);
    }

    [Test]
    public void TestConversion_MillilitreToLitre()
    {
        Assert.AreEqual(1.0, CreateVolume(1000.0, VolumeUnit.Millilitre).ConvertTo(VolumeUnit.Litre), 0.0001);
    }

    [Test]
    public void TestConversion_GallonToLitre()
    {
        Assert.AreEqual(3.78541, CreateVolume(1.0, VolumeUnit.Gallon).ConvertTo(VolumeUnit.Litre), 0.0001);
    }

    [Test]
    public void TestConversion_LitreToGallon()
    {
        Assert.AreEqual(0.264172, CreateVolume(1.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Gallon), 0.0001);
    }

    [Test]
    public void TestConversion_MillilitreToGallon()
    {
        Assert.AreEqual(0.264172, CreateVolume(1000.0, VolumeUnit.Millilitre).ConvertTo(VolumeUnit.Gallon), 0.0001);
    }

    [Test]
    public void TestConversion_GallonToMillilitre()
    {
        Assert.AreEqual(3785.41, CreateVolume(1.0, VolumeUnit.Gallon).ConvertTo(VolumeUnit.Millilitre), 0.01);
    }

    [Test]
    public void TestConversion_SameUnit()
    {
        Assert.AreEqual(5.0, CreateVolume(5.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Litre), 0.0001);
    }

    [Test]
    public void TestConversion_ZeroValue()
    {
        Assert.AreEqual(0.0, CreateVolume(0.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Millilitre), 0.0001);
    }

    [Test]
    public void TestConversion_NegativeValue()
    {
        Assert.AreEqual(-1000.0, CreateVolume(-1.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Millilitre), 0.0001);
    }

    [Test]
    public void TestConversion_RoundTrip()
    {
        var original = CreateVolume(1.5, VolumeUnit.Litre);
        var inMillilitre = original.To(VolumeUnit.Millilitre);
        var back = inMillilitre.To(VolumeUnit.Litre);
        Assert.AreEqual(original.Value, back.Value, 0.0001);
    }

    // Addition Tests
    [Test]
    public void TestAddition_SameUnit_LitrePlusLitre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Litre).Add(CreateVolume(2.0, VolumeUnit.Litre));
        Assert.AreEqual(3.0, result.Value);
        Assert.AreEqual(VolumeUnit.Litre, result.Unit);
    }

    [Test]
    public void TestAddition_SameUnit_MillilitrePlusMillilitre()
    {
        var result = CreateVolume(500.0, VolumeUnit.Millilitre).Add(CreateVolume(500.0, VolumeUnit.Millilitre));
        Assert.AreEqual(1000.0, result.Value);
        Assert.AreEqual(VolumeUnit.Millilitre, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_LitrePlusMillilitre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Litre).Add(CreateVolume(1000.0, VolumeUnit.Millilitre));
        Assert.AreEqual(2.0, result.Value);
        Assert.AreEqual(VolumeUnit.Litre, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_MillilitrePlusLitre()
    {
        var result = CreateVolume(1000.0, VolumeUnit.Millilitre).Add(CreateVolume(1.0, VolumeUnit.Litre));
        Assert.AreEqual(2000.0, result.Value);
        Assert.AreEqual(VolumeUnit.Millilitre, result.Unit);
    }

    [Test]
    public void TestAddition_CrossUnit_GallonPlusLitre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Gallon).Add(CreateVolume(3.78541, VolumeUnit.Litre));
        Assert.AreEqual(2.0, result.Value, 0.0001);
        Assert.AreEqual(VolumeUnit.Gallon, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Litre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Litre).Add(CreateVolume(1000.0, VolumeUnit.Millilitre), VolumeUnit.Litre);
        Assert.AreEqual(2.0, result.Value);
        Assert.AreEqual(VolumeUnit.Litre, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Millilitre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Litre).Add(CreateVolume(1000.0, VolumeUnit.Millilitre), VolumeUnit.Millilitre);
        Assert.AreEqual(2000.0, result.Value);
        Assert.AreEqual(VolumeUnit.Millilitre, result.Unit);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Gallon()
    {
        var result = CreateVolume(3.78541, VolumeUnit.Litre).Add(CreateVolume(3.78541, VolumeUnit.Litre), VolumeUnit.Gallon);
        Assert.AreEqual(2.0, result.Value, 0.0001);
        Assert.AreEqual(VolumeUnit.Gallon, result.Unit);
    }

    [Test]
    public void TestAddition_Commutativity()
    {
        var a = CreateVolume(1.0, VolumeUnit.Litre);
        var b = CreateVolume(1000.0, VolumeUnit.Millilitre);
        var r1 = a.Add(b);
        var r2 = b.Add(a);
        Assert.IsTrue(r1.Equals(r2));
    }

    [Test]
    public void TestAddition_WithZero()
    {
        var result = CreateVolume(5.0, VolumeUnit.Litre).Add(CreateVolume(0.0, VolumeUnit.Millilitre));
        Assert.AreEqual(5.0, result.Value);
    }

    [Test]
    public void TestAddition_NegativeValues()
    {
        var result = CreateVolume(5.0, VolumeUnit.Litre).Add(CreateVolume(-2000.0, VolumeUnit.Millilitre));
        Assert.AreEqual(3.0, result.Value, 0.0001);
    }

    [Test]
    public void TestAddition_LargeValues()
    {
        var result = CreateVolume(1000000.0, VolumeUnit.Litre).Add(CreateVolume(1000000.0, VolumeUnit.Litre));
        Assert.AreEqual(2000000.0, result.Value, 0.0001);
    }

    [Test]
    public void TestAddition_SmallValues()
    {
        var result = CreateVolume(0.001, VolumeUnit.Litre).Add(CreateVolume(0.002, VolumeUnit.Litre));
        Assert.AreEqual(0.003, result.Value, 0.0001);
    }

    [Test]
    public void TestAddition_NullOperand_Throws()
    {
        var valid = CreateVolume(1.0, VolumeUnit.Litre);
        Assert.Throws<ArgumentNullException>(() => valid.Add(null));
    }

    // Validation Tests
    [Test]
    public void TestValidation_NaNValue_Throws()
    {
        Assert.Throws<ArgumentException>(() => CreateVolume(double.NaN, VolumeUnit.Litre));
    }

    [Test]
    public void TestValidation_InfinityValue_Throws()
    {
        Assert.Throws<ArgumentException>(() => CreateVolume(double.PositiveInfinity, VolumeUnit.Litre));
    }

    [Test]
    public void TestValidation_NegativeValue_Allowed()
    {
        Assert.DoesNotThrow(() => CreateVolume(-5.0, VolumeUnit.Litre));
    }

    // HashCode & ToString Tests
    [Test]
    public void TestHashCode_EqualVolumes_SameHash()
    {
        var litre = CreateVolume(1.0, VolumeUnit.Litre);
        var millilitre = CreateVolume(1000.0, VolumeUnit.Millilitre);
        Assert.AreEqual(litre.GetHashCode(), millilitre.GetHashCode());
    }

    [Test]
    public void TestToString_LitreRepresentation()
    {
        Assert.AreEqual("5 L", CreateVolume(5.0, VolumeUnit.Litre).ToString());
    }

    [Test]
    public void TestToString_MillilitreRepresentation()
    {
        Assert.AreEqual("1000 mL", CreateVolume(1000.0, VolumeUnit.Millilitre).ToString());
    }

    [Test]
    public void TestToString_GallonRepresentation()
    {
        Assert.AreEqual("2.5 gal", CreateVolume(2.5, VolumeUnit.Gallon).ToString());
    }

    // Operator Tests
    [Test]
    public void TestOperator_EqualityOperator_SameUnit()
    {
        Assert.IsTrue(CreateVolume(1.0, VolumeUnit.Litre) == CreateVolume(1.0, VolumeUnit.Litre));
    }

    [Test]
    public void TestOperator_EqualityOperator_CrossUnit()
    {
        Assert.IsTrue(CreateVolume(1.0, VolumeUnit.Litre) == CreateVolume(1000.0, VolumeUnit.Millilitre));
    }

    [Test]
    public void TestOperator_InequalityOperator()
    {
        Assert.IsTrue(CreateVolume(1.0, VolumeUnit.Litre) != CreateVolume(2.0, VolumeUnit.Litre));
    }

    // Static AddQuantities Tests
    [Test]
    public void TestStaticAddQuantities_ValidOperation()
    {
        var result = Quantity<VolumeUnit>.AddQuantities(
            CreateVolume(1.0, VolumeUnit.Litre),
            CreateVolume(1000.0, VolumeUnit.Millilitre));
        Assert.AreEqual(2.0, result.Value);
        Assert.AreEqual(VolumeUnit.Litre, result.Unit);
    }

    [Test]
    public void TestStaticAddQuantities_WithTargetUnit()
    {
        var result = Quantity<VolumeUnit>.AddQuantities(
            CreateVolume(1.0, VolumeUnit.Litre),
            CreateVolume(1000.0, VolumeUnit.Millilitre),
            VolumeUnit.Millilitre);
        Assert.AreEqual(2000.0, result.Value);
        Assert.AreEqual(VolumeUnit.Millilitre, result.Unit);
    }

    [Test]
    public void TestStaticAddQuantities_NullFirst_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => 
            Quantity<VolumeUnit>.AddQuantities(null, CreateVolume(1.0, VolumeUnit.Litre)));
    }

    [Test]
    public void TestStaticAddQuantities_NullSecond_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => 
            Quantity<VolumeUnit>.AddQuantities(CreateVolume(1.0, VolumeUnit.Litre), null));
    }

}