using System;

namespace ModelLayer.Entities
{
    /// <summary>
    /// Entity class for storing quantity measurement operations
    /// </summary>
    [Serializable]
    public class QuantityMeasurementEntity
    {
        // Core fields
        public string OperationType { get; }
        public double? FirstValue { get; }
        public string FirstUnit { get; } = string.Empty;
        public string FirstMeasurementType { get; } = string.Empty;
        
        public double? SecondValue { get; }
        public string SecondUnit { get; } = string.Empty;
        public string SecondMeasurementType { get; } = string.Empty;
        
        public double? ResultValue { get; }
        public string ResultUnit { get; } = string.Empty;
        public string ResultMeasurementType { get; } = string.Empty;
        
        public bool HasError { get; }
        public string ErrorMessage { get; } = string.Empty;
        
        public DateTime Timestamp { get; }

        // Constructor for conversion (single operand)
        public QuantityMeasurementEntity(
            string operationType,
            double firstValue, string firstUnit, string firstMeasurementType,
            double resultValue, string resultUnit, string resultMeasurementType)
        {
            OperationType = operationType;
            FirstValue = firstValue;
            FirstUnit = firstUnit ?? string.Empty;
            FirstMeasurementType = firstMeasurementType ?? string.Empty;
            ResultValue = resultValue;
            ResultUnit = resultUnit ?? string.Empty;
            ResultMeasurementType = resultMeasurementType ?? string.Empty;
            HasError = false;
            Timestamp = DateTime.Now;
        }

        // Constructor for binary operations (add, subtract, compare, divide)
        public QuantityMeasurementEntity(
            string operationType,
            double firstValue, string firstUnit, string firstMeasurementType,
            double secondValue, string secondUnit, string secondMeasurementType,
            double? resultValue, string? resultUnit, string? resultMeasurementType)
        {
            OperationType = operationType;
            FirstValue = firstValue;
            FirstUnit = firstUnit ?? string.Empty;
            FirstMeasurementType = firstMeasurementType ?? string.Empty;
            SecondValue = secondValue;
            SecondUnit = secondUnit ?? string.Empty;
            SecondMeasurementType = secondMeasurementType ?? string.Empty;
            ResultValue = resultValue;
            ResultUnit = resultUnit ?? string.Empty;
            ResultMeasurementType = resultMeasurementType ?? string.Empty;
            HasError = false;
            Timestamp = DateTime.Now;
        }

        // Constructor for error
        public QuantityMeasurementEntity(string operationType, string errorMessage)
        {
            OperationType = operationType;
            ErrorMessage = errorMessage ?? string.Empty;
            HasError = true;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            if (HasError)
                return $"[ERROR] {OperationType}: {ErrorMessage}";

            return OperationType switch
            {
                "COMPARE" => $"{FirstValue} {FirstUnit} == {SecondValue} {SecondUnit}? {(ResultValue == 1 ? "True" : "False")}",
                "CONVERT" => $"{FirstValue} {FirstUnit} = {ResultValue} {ResultUnit}",
                "ADD" => $"{FirstValue} {FirstUnit} + {SecondValue} {SecondUnit} = {ResultValue} {ResultUnit}",
                "SUBTRACT" => $"{FirstValue} {FirstUnit} - {SecondValue} {SecondUnit} = {ResultValue} {ResultUnit}",
                "DIVIDE" => $"{FirstValue} {FirstUnit} ÷ {SecondValue} {SecondUnit} = {ResultValue}",
                _ => $"{OperationType} completed"
            };
        }
    }
}
