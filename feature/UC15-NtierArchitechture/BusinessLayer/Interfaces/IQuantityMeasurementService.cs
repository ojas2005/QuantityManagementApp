using ModelLayer.DTOs;
using ModelLayer.Entities;

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// Service interface for quantity measurement operations
    /// </summary>
    public interface IQuantityMeasurementService
    {
        // Comparison
        QuantityMeasurementEntity CompareQuantities(QuantityDTO first, QuantityDTO second);
        
        // Conversion
        QuantityMeasurementEntity ConvertQuantity(QuantityDTO source, string targetUnit);
        
        // Arithmetic Operations
        QuantityMeasurementEntity AddQuantities(QuantityDTO first, QuantityDTO second);
        QuantityMeasurementEntity AddQuantities(QuantityDTO first, QuantityDTO second, string targetUnit);
        
        QuantityMeasurementEntity SubtractQuantities(QuantityDTO first, QuantityDTO second);
        QuantityMeasurementEntity SubtractQuantities(QuantityDTO first, QuantityDTO second, string targetUnit);
        
        QuantityMeasurementEntity DivideQuantities(QuantityDTO first, QuantityDTO second);
        
        // Helper methods
        string GetMeasurementTypeFromUnit(string unit);
        bool AreUnitsCompatible(string unit1, string unit2);
    }
}