
namespace ModelLayer.DTOs
{
    /// <summary>
    /// Data Transfer Object for quantity measurement input/output
    /// </summary>
    public class QuantityDTO
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public string MeasurementType { get; set; }

        public QuantityDTO()
        {
            Value = 0.0;
            Unit = string.Empty;
            MeasurementType = string.Empty;
        }

        public QuantityDTO(double value, string unit, string measurementType)
        {
            Value = value;
            Unit = unit ?? string.Empty;
            MeasurementType = measurementType ?? string.Empty;
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}