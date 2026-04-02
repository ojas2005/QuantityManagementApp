using ModelLayer.Entities;

namespace QuantityMeasurementApi.Models;

public class QuantityResponseDto
{
    public double? FirstValue { get; set; }
    public string? FirstUnit { get; set; }
    public double? SecondValue { get; set; }
    public string? SecondUnit { get; set; }
    public string? Operation { get; set; }
    public double? Result { get; set; }
    public string? ResultUnit { get; set; }
    public bool IsEqual { get; set; }
    public DateTime Timestamp { get; set; }
    public bool HasError { get; set; }
    public string? ErrorMessage { get; set; }

    public static QuantityResponseDto FromEntity(QuantityMeasurementEntity entity)
    {
        return new QuantityResponseDto
        {
            FirstValue = entity.FirstValue,
            FirstUnit = entity.FirstUnit,
            SecondValue = entity.SecondValue,
            SecondUnit = entity.SecondUnit,
            Operation = entity.OperationType,
            Result = entity.ResultValue,
            ResultUnit = entity.ResultUnit,
            IsEqual = entity.OperationType == "COMPARE" && entity.ResultValue == 1,
            Timestamp = entity.Timestamp,
            HasError = entity.HasError,
            ErrorMessage = entity.ErrorMessage
        };
    }
}
