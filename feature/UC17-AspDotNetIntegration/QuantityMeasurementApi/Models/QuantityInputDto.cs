namespace QuantityMeasurementApi.Models;

public class QuantityInputDto
{
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string MeasurementType { get; set; } = string.Empty;
}

public class CompareRequest
{
    public QuantityInputDto First { get; set; } = new();
    public QuantityInputDto Second { get; set; } = new();
}

public class ConvertRequest
{
    public QuantityInputDto Source { get; set; } = new();
    public string TargetUnit { get; set; } = string.Empty;
}

public class ArithmeticRequest
{
    public QuantityInputDto First { get; set; } = new();
    public QuantityInputDto Second { get; set; } = new();
    public string? TargetUnit { get; set; }
}
