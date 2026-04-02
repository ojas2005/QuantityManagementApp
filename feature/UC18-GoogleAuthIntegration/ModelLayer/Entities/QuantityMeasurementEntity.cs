using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Entities
{
    /// <summary>
    /// Entity class for storing quantity measurement operations
    /// </summary>
    [Table("QuantityMeasurements")]
    public class QuantityMeasurementEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string OperationType { get; set; } = string.Empty;

        public double? FirstValue { get; set; }

        [StringLength(10)]
        public string? FirstUnit { get; set; }

        [StringLength(20)]
        public string? FirstMeasurementType { get; set; }

        public double? SecondValue { get; set; }

        [StringLength(10)]
        public string? SecondUnit { get; set; }

        [StringLength(20)]
        public string? SecondMeasurementType { get; set; }

        public double? ResultValue { get; set; }

        [StringLength(10)]
        public string? ResultUnit { get; set; }

        [StringLength(20)]
        public string? ResultMeasurementType { get; set; }

        public bool HasError { get; set; }

        [StringLength(500)]
        public string? ErrorMessage { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        // Parameterless constructor for EF Core
        public QuantityMeasurementEntity()
        {
            Timestamp = DateTime.Now;
        }

        // Constructor for conversion (single operand)
        public QuantityMeasurementEntity(
            string operationType,
            double firstValue, string firstUnit, string firstMeasurementType,
            double resultValue, string resultUnit, string resultMeasurementType)
        {
            OperationType = operationType;
            FirstValue = firstValue;
            FirstUnit = firstUnit;
            FirstMeasurementType = firstMeasurementType;
            ResultValue = resultValue;
            ResultUnit = resultUnit;
            ResultMeasurementType = resultMeasurementType;
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
            FirstUnit = firstUnit;
            FirstMeasurementType = firstMeasurementType;
            SecondValue = secondValue;
            SecondUnit = secondUnit;
            SecondMeasurementType = secondMeasurementType;
            ResultValue = resultValue;
            ResultUnit = resultUnit;
            ResultMeasurementType = resultMeasurementType;
            HasError = false;
            Timestamp = DateTime.Now;
        }

        // Constructor for error
        public QuantityMeasurementEntity(string operationType, string errorMessage)
        {
            OperationType = operationType;
            ErrorMessage = errorMessage;
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