using System.Collections.Generic;
using ModelLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    /// <summary>
    /// Repository interface for quantity measurement data access
    /// </summary>
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAll();
        QuantityMeasurementEntity GetById(int id);
        void Clear();
    }
}