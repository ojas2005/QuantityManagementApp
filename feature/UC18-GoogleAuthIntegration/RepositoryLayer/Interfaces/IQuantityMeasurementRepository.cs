using ModelLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    /// <summary>
    /// Repository interface for quantity measurement data access
    /// </summary>
    public interface IQuantityMeasurementRepository
    {
        // Basic CRUD operations
        void Save(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAll();
        QuantityMeasurementEntity? GetById(int id);
        
        // Query operations
        List<QuantityMeasurementEntity> GetByOperationType(string operationType);
        List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType);
        List<QuantityMeasurementEntity> GetByDateRange(DateTime startDate, DateTime endDate);
        
        // Aggregate operations
        int GetTotalCount();
        int GetCountByOperationType(string operationType);
        
        // Delete operations
        void Clear();
        bool DeleteById(int id);
        
        // Pool statistics (for monitoring)
        string GetPoolStatistics();
        
        // Resource management
        void ReleaseResources();
    }
}