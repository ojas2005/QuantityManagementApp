using System;
using System.Collections.Generic;
using ModelLayer.Entities;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Repositories
{
    /// <summary>
    /// In-memory cache repository for quantity measurements
    /// Minimal implementation for UC15 - will be expanded in UC16
    /// </summary>
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly List<QuantityMeasurementEntity> _cache = new List<QuantityMeasurementEntity>();
        private static readonly object _lock = new object();

        public void Save(QuantityMeasurementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            lock (_lock)
            {
                _cache.Add(entity);
            }
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            lock (_lock)
            {
                return new List<QuantityMeasurementEntity>(_cache);
            }
        }

        public QuantityMeasurementEntity GetById(int id)
        {
            lock (_lock)
            {
                return id >= 0 && id < _cache.Count ? _cache[id] : null;
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _cache.Clear();
            }
        }
    }
}