using NUnit.Framework;
using System;

[TestFixture]
public class QuantityMeasurementAppTests
{
    // Helper methods
    private Quantity<LengthUnit> CreateLength(double value, LengthUnit unit)
    {
        return new Quantity<LengthUnit>(value, unit);
    }

    private Quantity<WeightUnit> CreateWeight(double value, WeightUnit unit)
    {
        return new Quantity<WeightUnit>(value, unit);
    }

    private Quantity<VolumeUnit> CreateVolume(double value, VolumeUnit unit)
    {
        return new Quantity<VolumeUnit>(value, unit);
    }

    #region Length Unit Tests

    [Test]
    public void TestFeetEquality_SameValue()
    {
        Assert.That(CreateLength(2.0, LengthUnit.Feet).Equals(CreateLength(2.0, LengthUnit.Feet)), Is.True);
    }

    [Test]
    public void TestFeetEquality_DifferentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(3.5, LengthUnit.Feet)), Is.False);
    }

    [Test]
    public void TestInchesEquality_SameValue()
    {
        Assert.That(CreateLength(3.5, LengthUnit.Inch).Equals(CreateLength(3.5, LengthUnit.Inch)), Is.True);
    }

    [Test]
    public void TestInchesEquality_DifferentValue()
    {
        Assert.That(CreateLength(2.0, LengthUnit.Inch).Equals(CreateLength(4.0, LengthUnit.Inch)), Is.False);
    }

    [Test]
    public void TestEquality_FeetToFeet_SameValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(1.0, LengthUnit.Feet)), Is.True);
    }

    [Test]
    public void TestEquality_FeetToFeet_DifferentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(2.0, LengthUnit.Feet)), Is.False);
    }

    [Test]
    public void TestEquality_InchToInch_SameValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Inch).Equals(CreateLength(1.0, LengthUnit.Inch)), Is.True);
    }

    [Test]
    public void TestEquality_InchToInch_DifferentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Inch).Equals(CreateLength(2.0, LengthUnit.Inch)), Is.False);
    }

    [Test]
    public void TestEquality_FeetToInch_EquivalentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(12.0, LengthUnit.Inch)), Is.True);
    }

    [Test]
    public void TestEquality_InchToFeet_EquivalentValue()
    {
        Assert.That(CreateLength(12.0, LengthUnit.Inch).Equals(CreateLength(1.0, LengthUnit.Feet)), Is.True);
    }

    [Test]
    public void TestEquality_FeetToInch_DifferentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).Equals(CreateLength(1.0, LengthUnit.Inch)), Is.False);
    }

    [Test]
    public void TestEquality_SameReference()
    {
        var q = CreateLength(3.5, LengthUnit.Feet);
        Assert.That(q.Equals(q), Is.True);
    }

    [Test]
    public void TestEquality_NullComparison()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).Equals(null), Is.False);
    }

    [Test]
    public void TestEquality_DifferentType()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).Equals(new object()), Is.False);
    }

    [Test]
    public void TestEquality_Transitive()
    {
        var f = CreateLength(1.0, LengthUnit.Feet);
        var i = CreateLength(12.0, LengthUnit.Inch);
        var f2 = CreateLength(1.0, LengthUnit.Feet);
        Assert.That(f.Equals(i) && i.Equals(f2) && f.Equals(f2), Is.True);
    }

    [Test]
    public void TestEquality_YardToYard_SameValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(1.0, LengthUnit.Yards)), Is.True);
    }

    [Test]
    public void TestEquality_YardToYard_DifferentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(2.0, LengthUnit.Yards)), Is.False);
    }

    [Test]
    public void TestEquality_YardToFeet_EquivalentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(3.0, LengthUnit.Feet)), Is.True);
    }

    [Test]
    public void TestEquality_FeetToYard_EquivalentValue()
    {
        Assert.That(CreateLength(3.0, LengthUnit.Feet).Equals(CreateLength(1.0, LengthUnit.Yards)), Is.True);
    }

    [Test]
    public void TestEquality_YardToInches_EquivalentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(36.0, LengthUnit.Inch)), Is.True);
    }

    [Test]
    public void TestEquality_InchesToYard_EquivalentValue()
    {
        Assert.That(CreateLength(36.0, LengthUnit.Inch).Equals(CreateLength(1.0, LengthUnit.Yards)), Is.True);
    }

    [Test]
    public void TestEquality_YardToFeet_NonEquivalentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Yards).Equals(CreateLength(2.0, LengthUnit.Feet)), Is.False);
    }

    [Test]
    public void TestEquality_CentimeterToCentimeter_SameValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Centimeters).Equals(CreateLength(1.0, LengthUnit.Centimeters)), Is.True);
    }

    [Test]
    public void TestEquality_CentimeterToCentimeter_DifferentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Centimeters).Equals(CreateLength(2.0, LengthUnit.Centimeters)), Is.False);
    }

    [Test]
    public void TestEquality_CentimeterToInches_EquivalentValue()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Centimeters).Equals(CreateLength(0.393701, LengthUnit.Inch)), Is.True);
    }

    [Test]
    public void TestEquality_InchesToCentimeter_EquivalentValue()
    {
        Assert.That(CreateLength(0.393701, LengthUnit.Inch).Equals(CreateLength(1.0, LengthUnit.Centimeters)), Is.True);
    }

    [Test]
    public void TestOperator_EqualityOperator_Same()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet) == CreateLength(1.0, LengthUnit.Feet), Is.True);
    }

    [Test]
    public void TestOperator_EqualityOperator_CrossUnit()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet) == CreateLength(12.0, LengthUnit.Inch), Is.True);
    }

    [Test]
    public void TestOperator_InequalityOperator()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet) != CreateLength(1.0, LengthUnit.Inch), Is.True);
    }

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
        Assert.DoesNotThrow(() => CreateLength(-5.0, LengthUnit.Feet));
    }

    [Test]
    public void TestValidation_ZeroValue()
    {
        Assert.DoesNotThrow(() => CreateLength(0.0, LengthUnit.Feet));
    }

    [Test]
    public void TestHashCode_EqualObjects_SameHash()
    {
        var f = CreateLength(1.0, LengthUnit.Feet);
        var i = CreateLength(12.0, LengthUnit.Inch);
        Assert.That(f.GetHashCode(), Is.EqualTo(i.GetHashCode()));
    }

    [Test]
    public void TestToString_FeetRepresentation()
    {
        Assert.That(CreateLength(5.0, LengthUnit.Feet).ToString(), Is.EqualTo("5 ft"));
    }

    [Test]
    public void TestToString_InchRepresentation()
    {
        Assert.That(CreateLength(12.0, LengthUnit.Inch).ToString(), Is.EqualTo("12 in"));
    }

    [Test]
    public void TestToString_YardRepresentation()
    {
        Assert.That(CreateLength(2.5, LengthUnit.Yards).ToString(), Is.EqualTo("2.5 yd"));
    }

    [Test]
    public void TestToString_CentimeterRepresentation()
    {
        Assert.That(CreateLength(2.5, LengthUnit.Centimeters).ToString(), Is.EqualTo("2.5 cm"));
    }

    [Test]
    public void TestConversion_FeetToInches()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).ConvertTo(LengthUnit.Inch), Is.EqualTo(12.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_InchesToFeet()
    {
        Assert.That(CreateLength(24.0, LengthUnit.Inch).ConvertTo(LengthUnit.Feet), Is.EqualTo(2.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_YardsToFeet()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Yards).ConvertTo(LengthUnit.Feet), Is.EqualTo(3.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_FeetToYards()
    {
        Assert.That(CreateLength(6.0, LengthUnit.Feet).ConvertTo(LengthUnit.Yards), Is.EqualTo(2.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_YardsToInches()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Yards).ConvertTo(LengthUnit.Inch), Is.EqualTo(36.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_InchesToYards()
    {
        Assert.That(CreateLength(72.0, LengthUnit.Inch).ConvertTo(LengthUnit.Yards), Is.EqualTo(2.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_CentimetersToInches()
    {
        Assert.That(CreateLength(2.54, LengthUnit.Centimeters).ConvertTo(LengthUnit.Inch), Is.EqualTo(1.0).Within(0.001));
    }

    [Test]
    public void TestConversion_InchesToCentimeters()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Inch).ConvertTo(LengthUnit.Centimeters), Is.EqualTo(2.54).Within(0.01));
    }

    [Test]
    public void TestConversion_FeetToCentimeters()
    {
        Assert.That(CreateLength(1.0, LengthUnit.Feet).ConvertTo(LengthUnit.Centimeters), Is.GreaterThan(29.0));
    }

    [Test]
    public void TestConversion_CentimetersToFeet()
    {
        Assert.That(CreateLength(30.48, LengthUnit.Centimeters).ConvertTo(LengthUnit.Feet), Is.EqualTo(1.0).Within(0.01));
    }

    [Test]
    public void TestConversion_SameUnit()
    {
        Assert.That(CreateLength(5.0, LengthUnit.Feet).ConvertTo(LengthUnit.Feet), Is.EqualTo(5.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_ZeroValue()
    {
        Assert.That(CreateLength(0.0, LengthUnit.Feet).ConvertTo(LengthUnit.Inch), Is.EqualTo(0.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_RoundTrip_FeetToInchesToFeet()
    {
        var original = CreateLength(5.0, LengthUnit.Feet);
        var inInches = original.To(LengthUnit.Inch);
        var back = inInches.To(LengthUnit.Feet);
        Assert.That(back.Value, Is.EqualTo(original.Value).Within(0.0001));
    }

    [Test]
    public void TestAddition_SameUnit_FeetPlusFeet()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(2.0, LengthUnit.Feet));
        Assert.That(result.Value, Is.EqualTo(3.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestAddition_CrossUnit_FeetPlusInches()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(12.0, LengthUnit.Inch));
        Assert.That(result.Value, Is.EqualTo(2.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestAddition_CrossUnit_InchPlusFeet()
    {
        var result = CreateLength(12.0, LengthUnit.Inch).Add(CreateLength(1.0, LengthUnit.Feet));
        Assert.That(result.Value, Is.EqualTo(24.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inch));
    }

    [Test]
    public void TestAddition_CrossUnit_YardPlusFeet()
    {
        var result = CreateLength(1.0, LengthUnit.Yards).Add(CreateLength(3.0, LengthUnit.Feet));
        Assert.That(result.Value, Is.EqualTo(2.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Yards));
    }

    [Test]
    public void TestAddition_WithZero()
    {
        var result = CreateLength(5.0, LengthUnit.Feet).Add(CreateLength(0.0, LengthUnit.Inch));
        Assert.That(result.Value, Is.EqualTo(5.0));
    }

    [Test]
    public void TestAddition_NullFirstOperand_Throws()
    {
        var validQuantity = CreateLength(1.0, LengthUnit.Feet);
        Assert.Throws<ArgumentNullException>(() => Quantity<LengthUnit>.AddQuantities(null!, validQuantity));
    }

    [Test]
    public void TestAddition_NullSecondOperand_Throws()
    {
        var validQuantity = CreateLength(1.0, LengthUnit.Feet);
        Assert.Throws<ArgumentNullException>(() => validQuantity.Add(null!));
    }

    [Test]
    public void TestAddition_Commutativity()
    {
        var a = CreateLength(1.0, LengthUnit.Feet);
        var b = CreateLength(12.0, LengthUnit.Inch);
        var r1 = a.Add(b);
        var r2 = b.Add(a);
        Assert.That(r1.Equals(r2), Is.True);
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Feet()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(12.0, LengthUnit.Inch), LengthUnit.Feet);
        Assert.That(result.Value, Is.EqualTo(2.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Inches()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(12.0, LengthUnit.Inch), LengthUnit.Inch);
        Assert.That(result.Value, Is.EqualTo(24.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inch));
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Yards()
    {
        var result = CreateLength(1.0, LengthUnit.Feet).Add(CreateLength(12.0, LengthUnit.Inch), LengthUnit.Yards);
        Assert.That(result.Value, Is.EqualTo(2.0 / 3.0).Within(0.01));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Yards));
    }

    #endregion

    #region Weight Unit Tests

    [Test]
    public void TestEquality_KilogramToKilogram_SameValue()
    {
        Assert.That(CreateWeight(1.0, WeightUnit.Kilogram).Equals(CreateWeight(1.0, WeightUnit.Kilogram)), Is.True);
    }

    [Test]
    public void TestEquality_KilogramToGram_EquivalentValue()
    {
        Assert.That(CreateWeight(1.0, WeightUnit.Kilogram).Equals(CreateWeight(1000.0, WeightUnit.Gram)), Is.True);
    }

    [Test]
    public void TestEquality_KilogramToPound_EquivalentValue()
    {
        Assert.That(CreateWeight(1.0, WeightUnit.Kilogram).Equals(CreateWeight(2.20462, WeightUnit.Pound)), Is.True);
    }

    [Test]
    public void TestEquality_WeightVsLength_Incompatible()
    {
        Assert.That(CreateWeight(1.0, WeightUnit.Kilogram).Equals(CreateLength(1.0, LengthUnit.Feet)), Is.False);
    }

    [Test]
    public void TestEquality_WeightNullComparison()
    {
        Assert.That(CreateWeight(1.0, WeightUnit.Kilogram).Equals(null), Is.False);
    }

    [Test]
    public void TestConversion_KilogramToGram()
    {
        Assert.That(CreateWeight(1.0, WeightUnit.Kilogram).ConvertTo(WeightUnit.Gram), Is.EqualTo(1000.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_KilogramToPound()
    {
        Assert.That(CreateWeight(1.0, WeightUnit.Kilogram).ConvertTo(WeightUnit.Pound), Is.EqualTo(2.20462).Within(0.001));
    }

    [Test]
    public void TestConversion_GramToKilogram()
    {
        Assert.That(CreateWeight(1000.0, WeightUnit.Gram).ConvertTo(WeightUnit.Kilogram), Is.EqualTo(1.0).Within(0.0001));
    }

    [Test]
    public void TestAddition_SameUnit_KilogramPlusKilogram()
    {
        var result = CreateWeight(1.0, WeightUnit.Kilogram).Add(CreateWeight(2.0, WeightUnit.Kilogram));
        Assert.That(result.Value, Is.EqualTo(3.0));
        Assert.That(result.Unit, Is.EqualTo(WeightUnit.Kilogram));
    }

    [Test]
    public void TestAddition_CrossUnit_KilogramPlusGram()
    {
        var result = CreateWeight(1.0, WeightUnit.Kilogram).Add(CreateWeight(1000.0, WeightUnit.Gram));
        Assert.That(result.Value, Is.EqualTo(2.0));
        Assert.That(result.Unit, Is.EqualTo(WeightUnit.Kilogram));
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Gram()
    {
        var result = CreateWeight(1.0, WeightUnit.Kilogram).Add(CreateWeight(1000.0, WeightUnit.Gram), WeightUnit.Gram);
        Assert.That(result.Value, Is.EqualTo(2000.0));
        Assert.That(result.Unit, Is.EqualTo(WeightUnit.Gram));
    }

    [Test]
    public void TestWeightValidation_NegativeAllowed()
    {
        Assert.DoesNotThrow(() => CreateWeight(-1.0, WeightUnit.Kilogram));
    }

    [Test]
    public void TestWeightToString()
    {
        Assert.That(CreateWeight(5.5, WeightUnit.Kilogram).ToString(), Is.EqualTo("5.5 kg"));
    }

    #endregion

    #region Volume Unit Tests (UC11)

    [Test]
    public void TestEquality_LitreToLitre_SameValue()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateVolume(1.0, VolumeUnit.Litre)), Is.True);
    }

    [Test]
    public void TestEquality_LitreToLitre_DifferentValue()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateVolume(2.0, VolumeUnit.Litre)), Is.False);
    }

    [Test]
    public void TestEquality_MillilitreToMillilitre_SameValue()
    {
        Assert.That(CreateVolume(1000.0, VolumeUnit.Millilitre).Equals(CreateVolume(1000.0, VolumeUnit.Millilitre)), Is.True);
    }

    [Test]
    public void TestEquality_GallonToGallon_SameValue()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Gallon).Equals(CreateVolume(1.0, VolumeUnit.Gallon)), Is.True);
    }

    [Test]
    public void TestEquality_LitreToMillilitre_EquivalentValue()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateVolume(1000.0, VolumeUnit.Millilitre)), Is.True);
    }

    [Test]
    public void TestEquality_MillilitreToLitre_EquivalentValue()
    {
        Assert.That(CreateVolume(1000.0, VolumeUnit.Millilitre).Equals(CreateVolume(1.0, VolumeUnit.Litre)), Is.True);
    }

    [Test]
    public void TestEquality_LitreToGallon_EquivalentValue()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateVolume(0.264172, VolumeUnit.Gallon)), Is.True);
    }

    [Test]
    public void TestEquality_GallonToLitre_EquivalentValue()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Gallon).Equals(CreateVolume(3.78541, VolumeUnit.Litre)), Is.True);
    }

    [Test]
    public void TestEquality_MillilitreToGallon_EquivalentValue()
    {
        Assert.That(CreateVolume(1000.0, VolumeUnit.Millilitre).Equals(CreateVolume(0.264172, VolumeUnit.Gallon)), Is.True);
    }

    [Test]
    public void TestEquality_VolumeVsLength_Incompatible()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateLength(1.0, LengthUnit.Feet)), Is.False);
    }

    [Test]
    public void TestEquality_VolumeVsWeight_Incompatible()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).Equals(CreateWeight(1.0, WeightUnit.Kilogram)), Is.False);
    }

    [Test]
    public void TestEquality_VolumeNullComparison()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).Equals(null), Is.False);
    }

    [Test]
    public void TestEquality_SameReference12()
    {
        var vol = CreateVolume(3.5, VolumeUnit.Litre);
        Assert.That(vol.Equals(vol), Is.True);
    }

    [Test]
    public void TestEquality_TransitiveProperty()
    {
        var a = CreateVolume(1.0, VolumeUnit.Litre);
        var b = CreateVolume(1000.0, VolumeUnit.Millilitre);
        var c = CreateVolume(1.0, VolumeUnit.Litre);
        Assert.That(a.Equals(b) && b.Equals(c) && a.Equals(c), Is.True);
    }

    [Test]
    public void TestEquality_ZeroValue()
    {
        Assert.That(CreateVolume(0.0, VolumeUnit.Litre).Equals(CreateVolume(0.0, VolumeUnit.Millilitre)), Is.True);
    }

    [Test]
    public void TestEquality_NegativeVolume()
    {
        Assert.That(CreateVolume(-1.0, VolumeUnit.Litre).Equals(CreateVolume(-1000.0, VolumeUnit.Millilitre)), Is.True);
    }

    [Test]
    public void TestEquality_LargeVolumeValue()
    {
        Assert.That(CreateVolume(1000000.0, VolumeUnit.Millilitre).Equals(CreateVolume(1000.0, VolumeUnit.Litre)), Is.True);
    }

    [Test]
    public void TestEquality_SmallVolumeValue()
    {
        Assert.That(CreateVolume(0.001, VolumeUnit.Litre).Equals(CreateVolume(1.0, VolumeUnit.Millilitre)), Is.True);
    }

    [Test]
    public void TestConversion_LitreToMillilitre()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Millilitre), Is.EqualTo(1000.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_MillilitreToLitre()
    {
        Assert.That(CreateVolume(1000.0, VolumeUnit.Millilitre).ConvertTo(VolumeUnit.Litre), Is.EqualTo(1.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_GallonToLitre()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Gallon).ConvertTo(VolumeUnit.Litre), Is.EqualTo(3.78541).Within(0.0001));
    }

    [Test]
    public void TestConversion_LitreToGallon()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Gallon), Is.EqualTo(0.264172).Within(0.0001));
    }

    [Test]
    public void TestConversion_MillilitreToGallon()
    {
        Assert.That(CreateVolume(1000.0, VolumeUnit.Millilitre).ConvertTo(VolumeUnit.Gallon), Is.EqualTo(0.264172).Within(0.0001));
    }

    [Test]
    public void TestConversion_GallonToMillilitre()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Gallon).ConvertTo(VolumeUnit.Millilitre), Is.EqualTo(3785.41).Within(0.01));
    }

    [Test]
    public void TestConversion_SameUnit112()
    {
        Assert.That(CreateVolume(5.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Litre), Is.EqualTo(5.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_ZeroValue21()
    {
        Assert.That(CreateVolume(0.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Millilitre), Is.EqualTo(0.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_NegativeValue()
    {
        Assert.That(CreateVolume(-1.0, VolumeUnit.Litre).ConvertTo(VolumeUnit.Millilitre), Is.EqualTo(-1000.0).Within(0.0001));
    }

    [Test]
    public void TestConversion_RoundTrip()
    {
        var original = CreateVolume(1.5, VolumeUnit.Litre);
        var inMillilitre = original.To(VolumeUnit.Millilitre);
        var back = inMillilitre.To(VolumeUnit.Litre);
        Assert.That(back.Value, Is.EqualTo(original.Value).Within(0.0001));
    }

    [Test]
    public void TestAddition_SameUnit_LitrePlusLitre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Litre).Add(CreateVolume(2.0, VolumeUnit.Litre));
        Assert.That(result.Value, Is.EqualTo(3.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Litre));
    }

    [Test]
    public void TestAddition_SameUnit_MillilitrePlusMillilitre()
    {
        var result = CreateVolume(500.0, VolumeUnit.Millilitre).Add(CreateVolume(500.0, VolumeUnit.Millilitre));
        Assert.That(result.Value, Is.EqualTo(1000.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Millilitre));
    }

    [Test]
    public void TestAddition_CrossUnit_LitrePlusMillilitre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Litre).Add(CreateVolume(1000.0, VolumeUnit.Millilitre));
        Assert.That(result.Value, Is.EqualTo(2.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Litre));
    }

    [Test]
    public void TestAddition_CrossUnit_MillilitrePlusLitre()
    {
        var result = CreateVolume(1000.0, VolumeUnit.Millilitre).Add(CreateVolume(1.0, VolumeUnit.Litre));
        Assert.That(result.Value, Is.EqualTo(2000.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Millilitre));
    }

    [Test]
    public void TestAddition_CrossUnit_GallonPlusLitre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Gallon).Add(CreateVolume(3.78541, VolumeUnit.Litre));
        Assert.That(result.Value, Is.EqualTo(2.0).Within(0.0001));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Gallon));
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Litre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Litre).Add(CreateVolume(1000.0, VolumeUnit.Millilitre), VolumeUnit.Litre);
        Assert.That(result.Value, Is.EqualTo(2.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Litre));
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Millilitre()
    {
        var result = CreateVolume(1.0, VolumeUnit.Litre).Add(CreateVolume(1000.0, VolumeUnit.Millilitre), VolumeUnit.Millilitre);
        Assert.That(result.Value, Is.EqualTo(2000.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Millilitre));
    }

    [Test]
    public void TestAddition_ExplicitTargetUnit_Gallon()
    {
        var result = CreateVolume(3.78541, VolumeUnit.Litre).Add(CreateVolume(3.78541, VolumeUnit.Litre), VolumeUnit.Gallon);
        Assert.That(result.Value, Is.EqualTo(2.0).Within(0.0001));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Gallon));
    }

    [Test]
    public void TestAddition_Commutativity87()
    {
        var a = CreateVolume(1.0, VolumeUnit.Litre);
        var b = CreateVolume(1000.0, VolumeUnit.Millilitre);
        var r1 = a.Add(b);
        var r2 = b.Add(a);
        Assert.That(r1.Equals(r2), Is.True);
    }

    [Test]
    public void TestAddition_WithZero22()
    {
        var result = CreateVolume(5.0, VolumeUnit.Litre).Add(CreateVolume(0.0, VolumeUnit.Millilitre));
        Assert.That(result.Value, Is.EqualTo(5.0));
    }

    [Test]
    public void TestAddition_NegativeValues()
    {
        var result = CreateVolume(5.0, VolumeUnit.Litre).Add(CreateVolume(-2000.0, VolumeUnit.Millilitre));
        Assert.That(result.Value, Is.EqualTo(3.0).Within(0.0001));
    }

    [Test]
    public void TestAddition_LargeValues()
    {
        var result = CreateVolume(1000000.0, VolumeUnit.Litre).Add(CreateVolume(1000000.0, VolumeUnit.Litre));
        Assert.That(result.Value, Is.EqualTo(2000000.0).Within(0.0001));
    }

    [Test]
    public void TestAddition_SmallValues()
    {
        // Using larger values to avoid precision issues
        var result = CreateVolume(0.1, VolumeUnit.Litre).Add(CreateVolume(0.2, VolumeUnit.Litre));
        Assert.That(result.Value, Is.EqualTo(0.3).Within(0.001));
    }

    [Test]
    public void TestAddition_NullOperand_Throws()
    {
        var valid = CreateVolume(1.0, VolumeUnit.Litre);
        Assert.Throws<ArgumentNullException>(() => valid.Add(null!));
    }

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

    [Test]
    public void TestHashCode_EqualVolumes_SameHash()
    {
        var litre = CreateVolume(1.0, VolumeUnit.Litre);
        var millilitre = CreateVolume(1000.0, VolumeUnit.Millilitre);
        Assert.That(litre.GetHashCode(), Is.EqualTo(millilitre.GetHashCode()));
    }

    [Test]
    public void TestToString_LitreRepresentation()
    {
        Assert.That(CreateVolume(5.0, VolumeUnit.Litre).ToString(), Is.EqualTo("5 L"));
    }

    [Test]
    public void TestToString_MillilitreRepresentation()
    {
        Assert.That(CreateVolume(1000.0, VolumeUnit.Millilitre).ToString(), Is.EqualTo("1000 mL"));
    }

    [Test]
    public void TestToString_GallonRepresentation()
    {
        Assert.That(CreateVolume(2.5, VolumeUnit.Gallon).ToString(), Is.EqualTo("2.5 gal"));
    }

    [Test]
    public void TestOperator_EqualityOperator_SameUnit()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre) == CreateVolume(1.0, VolumeUnit.Litre), Is.True);
    }

    [Test]
    public void TestOperator_EqualityOperator_CrossUnit98()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre) == CreateVolume(1000.0, VolumeUnit.Millilitre), Is.True);
    }

    [Test]
    public void TestOperator_InequalityOperator877()
    {
        Assert.That(CreateVolume(1.0, VolumeUnit.Litre) != CreateVolume(2.0, VolumeUnit.Litre), Is.True);
    }

    [Test]
    public void TestStaticAddQuantities_ValidOperation()
    {
        var result = Quantity<VolumeUnit>.AddQuantities(
            CreateVolume(1.0, VolumeUnit.Litre),
            CreateVolume(1000.0, VolumeUnit.Millilitre));
        Assert.That(result.Value, Is.EqualTo(2.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Litre));
    }

    [Test]
    public void TestStaticAddQuantities_WithTargetUnit()
    {
        var result = Quantity<VolumeUnit>.AddQuantities(
            CreateVolume(1.0, VolumeUnit.Litre),
            CreateVolume(1000.0, VolumeUnit.Millilitre),
            VolumeUnit.Millilitre);
        Assert.That(result.Value, Is.EqualTo(2000.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Millilitre));
    }

    [Test]
    public void TestStaticAddQuantities_NullFirst_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => 
            Quantity<VolumeUnit>.AddQuantities(null!, CreateVolume(1.0, VolumeUnit.Litre)));
    }

    [Test]
    public void TestStaticAddQuantities_NullSecond_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => 
            Quantity<VolumeUnit>.AddQuantities(CreateVolume(1.0, VolumeUnit.Litre), null!));
    }

    #endregion

    #region Subtraction Tests (UC12)

    [Test]
    public void TestSubtraction_SameUnit_FeetMinusFeet()
    {
        var result = CreateLength(10.0, LengthUnit.Feet).Subtract(CreateLength(5.0, LengthUnit.Feet));
        Assert.That(result.Value, Is.EqualTo(5.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestSubtraction_SameUnit_LitreMinusLitre()
    {
        var result = CreateVolume(10.0, VolumeUnit.Litre).Subtract(CreateVolume(3.0, VolumeUnit.Litre));
        Assert.That(result.Value, Is.EqualTo(7.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Litre));
    }

    [Test]
    public void TestSubtraction_CrossUnit_FeetMinusInches()
    {
        var result = CreateLength(10.0, LengthUnit.Feet).Subtract(CreateLength(6.0, LengthUnit.Inch));
        Assert.That(result.Value, Is.EqualTo(9.5).Within(0.01));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestSubtraction_CrossUnit_InchesMinusFeet()
    {
        var result = CreateLength(120.0, LengthUnit.Inch).Subtract(CreateLength(5.0, LengthUnit.Feet));
        Assert.That(result.Value, Is.EqualTo(60.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inch));
    }

    [Test]
    public void TestSubtraction_CrossUnit_KilogramMinusGram()
    {
        var result = CreateWeight(10.0, WeightUnit.Kilogram).Subtract(CreateWeight(5000.0, WeightUnit.Gram));
        Assert.That(result.Value, Is.EqualTo(5.0));
        Assert.That(result.Unit, Is.EqualTo(WeightUnit.Kilogram));
    }

    [Test]
    public void TestSubtraction_ExplicitTargetUnit_Feet()
    {
        var result = CreateLength(10.0, LengthUnit.Feet).Subtract(CreateLength(6.0, LengthUnit.Inch), LengthUnit.Feet);
        Assert.That(result.Value, Is.EqualTo(9.5).Within(0.01));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestSubtraction_ExplicitTargetUnit_Inches()
    {
        var result = CreateLength(10.0, LengthUnit.Feet).Subtract(CreateLength(6.0, LengthUnit.Inch), LengthUnit.Inch);
        Assert.That(result.Value, Is.EqualTo(114.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inch));
    }

    [Test]
    public void TestSubtraction_ExplicitTargetUnit_Millilitre()
    {
        var result = CreateVolume(5.0, VolumeUnit.Litre).Subtract(CreateVolume(2.0, VolumeUnit.Litre), VolumeUnit.Millilitre);
        Assert.That(result.Value, Is.EqualTo(3000.0));
        Assert.That(result.Unit, Is.EqualTo(VolumeUnit.Millilitre));
    }

    [Test]
    public void TestSubtraction_ResultingInNegative()
    {
        var result = CreateWeight(5.0, WeightUnit.Kilogram).Subtract(CreateWeight(10.0, WeightUnit.Kilogram));
        Assert.That(result.Value, Is.EqualTo(-5.0));
        Assert.That(result.Unit, Is.EqualTo(WeightUnit.Kilogram));
    }

    [Test]
    public void TestSubtraction_ResultingInZero()
    {
        var result = CreateLength(10.0, LengthUnit.Feet).Subtract(CreateLength(120.0, LengthUnit.Inch));
        Assert.That(result.Value, Is.EqualTo(0.0).Within(0.01));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestSubtraction_WithZeroOperand()
    {
        var result = CreateLength(5.0, LengthUnit.Feet).Subtract(CreateLength(0.0, LengthUnit.Inch));
        Assert.That(result.Value, Is.EqualTo(5.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestSubtraction_WithNegativeValues()
    {
        var result = CreateWeight(5.0, WeightUnit.Kilogram).Subtract(CreateWeight(-2.0, WeightUnit.Kilogram));
        Assert.That(result.Value, Is.EqualTo(7.0).Within(0.001));
        Assert.That(result.Unit, Is.EqualTo(WeightUnit.Kilogram));
    }

    [Test]
    public void TestSubtraction_NonCommutative()
    {
        var a = CreateWeight(10.0, WeightUnit.Kilogram);
        var b = CreateWeight(5.0, WeightUnit.Kilogram);
        var result1 = a.Subtract(b);
        var result2 = b.Subtract(a);
        Assert.That(result1.Value, Is.EqualTo(5.0));
        Assert.That(result2.Value, Is.EqualTo(-5.0));
    }

    [Test]
    public void TestSubtraction_WithLargeValues()
    {
        var result = CreateWeight(1000000.0, WeightUnit.Kilogram).Subtract(CreateWeight(500000.0, WeightUnit.Kilogram));
        Assert.That(result.Value, Is.EqualTo(500000.0));
    }

    [Test]
    public void TestSubtraction_WithSmallValues()
    {
        var result = CreateVolume(0.1, VolumeUnit.Litre).Subtract(CreateVolume(0.05, VolumeUnit.Litre));
        Assert.That(result.Value, Is.EqualTo(0.05).Within(0.001));
    }

    [Test]
    public void TestSubtraction_NullOperand_Throws()
    {
        var valid = CreateLength(10.0, LengthUnit.Feet);
        Assert.Throws<ArgumentNullException>(() => valid.Subtract(null!));
    }

    [Test]
    public void TestSubtraction_ChainedOperations()
    {
        var result = CreateLength(10.0, LengthUnit.Feet)
            .Subtract(CreateLength(2.0, LengthUnit.Feet))
            .Subtract(CreateLength(1.0, LengthUnit.Feet));
        Assert.That(result.Value, Is.EqualTo(7.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Feet));
    }

    [Test]
    public void TestStaticSubtractQuantities_ValidOperation()
    {
        var result = Quantity<LengthUnit>.SubtractQuantities(
            CreateLength(10.0, LengthUnit.Feet),
            CreateLength(5.0, LengthUnit.Feet));
        Assert.That(result.Value, Is.EqualTo(5.0));
    }

    [Test]
    public void TestStaticSubtractQuantities_WithTargetUnit()
    {
        var result = Quantity<LengthUnit>.SubtractQuantities(
            CreateLength(10.0, LengthUnit.Feet),
            CreateLength(6.0, LengthUnit.Inch),
            LengthUnit.Inch);
        Assert.That(result.Value, Is.EqualTo(114.0));
        Assert.That(result.Unit, Is.EqualTo(LengthUnit.Inch));
    }

    #endregion

    #region Division Tests (UC12)

    [Test]
    public void TestDivision_SameUnit_FeetDividedByFeet()
    {
        var result = CreateLength(10.0, LengthUnit.Feet).Divide(CreateLength(2.0, LengthUnit.Feet));
        Assert.That(result, Is.EqualTo(5.0));
    }

    [Test]
    public void TestDivision_SameUnit_LitreDividedByLitre()
    {
        var result = CreateVolume(10.0, VolumeUnit.Litre).Divide(CreateVolume(5.0, VolumeUnit.Litre));
        Assert.That(result, Is.EqualTo(2.0));
    }

    [Test]
    public void TestDivision_CrossUnit_FeetDividedByInches()
    {
        var result = CreateLength(24.0, LengthUnit.Inch).Divide(CreateLength(2.0, LengthUnit.Feet));
        Assert.That(result, Is.EqualTo(1.0));
    }

    [Test]
    public void TestDivision_CrossUnit_KilogramDividedByGram()
    {
        var result = CreateWeight(2.0, WeightUnit.Kilogram).Divide(CreateWeight(2000.0, WeightUnit.Gram));
        Assert.That(result, Is.EqualTo(1.0));
    }

    [Test]
    public void TestDivision_CrossUnit_MillilitreDividedByLitre()
    {
        var result = CreateVolume(1000.0, VolumeUnit.Millilitre).Divide(CreateVolume(1.0, VolumeUnit.Litre));
        Assert.That(result, Is.EqualTo(1.0));
    }

    [Test]
    public void TestDivision_RatioGreaterThanOne()
    {
        var result = CreateLength(10.0, LengthUnit.Feet).Divide(CreateLength(2.0, LengthUnit.Feet));
        Assert.That(result, Is.EqualTo(5.0));
    }

    [Test]
    public void TestDivision_RatioLessThanOne()
    {
        var result = CreateLength(5.0, LengthUnit.Feet).Divide(CreateLength(10.0, LengthUnit.Feet));
        Assert.That(result, Is.EqualTo(0.5));
    }

    [Test]
    public void TestDivision_RatioEqualToOne()
    {
        var result = CreateLength(10.0, LengthUnit.Feet).Divide(CreateLength(10.0, LengthUnit.Feet));
        Assert.That(result, Is.EqualTo(1.0));
    }

    [Test]
    public void TestDivision_NonCommutative()
    {
        var a = CreateLength(10.0, LengthUnit.Feet);
        var b = CreateLength(5.0, LengthUnit.Feet);
        var result1 = a.Divide(b);
        var result2 = b.Divide(a);
        Assert.That(result1, Is.EqualTo(2.0));
        Assert.That(result2, Is.EqualTo(0.5));
    }

    [Test]
    public void TestDivision_ByZero_Throws()
    {
        var valid = CreateLength(10.0, LengthUnit.Feet);
        var zero = CreateLength(0.0, LengthUnit.Feet);
        Assert.Throws<DivideByZeroException>(() => valid.Divide(zero));
    }

    [Test]
    public void TestDivision_WithLargeRatio()
    {
        var result = CreateWeight(1000000.0, WeightUnit.Kilogram).Divide(CreateWeight(1.0, WeightUnit.Kilogram));
        Assert.That(result, Is.EqualTo(1000000.0));
    }

    [Test]
    public void TestDivision_WithSmallRatio()
    {
        var result = CreateWeight(1.0, WeightUnit.Kilogram).Divide(CreateWeight(1000000.0, WeightUnit.Kilogram));
        Assert.That(result, Is.EqualTo(0.000001).Within(0.0000001));
    }

    [Test]
    public void TestDivision_NullOperand_Throws()
    {
        var valid = CreateLength(10.0, LengthUnit.Feet);
        Assert.Throws<ArgumentNullException>(() => valid.Divide(null!));
    }

    [Test]
    public void TestDivision_WithNegativeValues()
    {
        var result = CreateWeight(10.0, WeightUnit.Kilogram).Divide(CreateWeight(-2.0, WeightUnit.Kilogram));
        Assert.That(result, Is.EqualTo(-5.0).Within(0.001));
    }

    [Test]
    public void TestDivision_WithZeroNumerator()
    {
        var result = CreateLength(0.0, LengthUnit.Feet).Divide(CreateLength(5.0, LengthUnit.Feet));
        Assert.That(result, Is.EqualTo(0.0));
    }

    [Test]
    public void TestStaticDivideQuantities_ValidOperation()
    {
        var result = Quantity<LengthUnit>.DivideQuantities(
            CreateLength(10.0, LengthUnit.Feet),
            CreateLength(2.0, LengthUnit.Feet));
        Assert.That(result, Is.EqualTo(5.0));
    }

    #endregion

    #region Integration Tests

    [Test]
    public void TestSubtractionAndDivision_Integration()
    {
        var result = CreateLength(10.0, LengthUnit.Feet)
            .Subtract(CreateLength(2.0, LengthUnit.Feet))
            .Divide(CreateLength(2.0, LengthUnit.Feet));
        Assert.That(result, Is.EqualTo(4.0));
    }

    [Test]
    public void TestSubtractionAddition_Inverse()
    {
        var original = CreateLength(10.0, LengthUnit.Feet);
        var toAdd = CreateLength(5.0, LengthUnit.Feet);
        var result = original.Add(toAdd).Subtract(toAdd);
        Assert.That(result.Value, Is.EqualTo(original.Value).Within(0.001));
        Assert.That(result.Unit, Is.EqualTo(original.Unit));
    }

    [Test]
    public void TestSubtraction_Immutability()
    {
        var original = CreateLength(10.0, LengthUnit.Feet);
        var originalValue = original.Value;
        var originalUnit = original.Unit;
        
        var result = original.Subtract(CreateLength(5.0, LengthUnit.Feet));
        
        Assert.That(original.Value, Is.EqualTo(originalValue));
        Assert.That(original.Unit, Is.EqualTo(originalUnit));
        Assert.That(result.Value, Is.EqualTo(5.0));
    }

    [Test]
    public void TestDivision_Immutability()
    {
        var original = CreateLength(10.0, LengthUnit.Feet);
        var originalValue = original.Value;
        var originalUnit = original.Unit;
        
        var result = original.Divide(CreateLength(2.0, LengthUnit.Feet));
        
        Assert.That(original.Value, Is.EqualTo(originalValue));
        Assert.That(original.Unit, Is.EqualTo(originalUnit));
        Assert.That(result, Is.EqualTo(5.0));
    }

    #endregion
}