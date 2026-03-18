using System.Text;
using Microsoft.Extensions.Logging;
using ModelLayer.Entities;
using ModelLayer.Exceptions;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Repositories
{
    /// <summary>
    /// In-memory cache repository for quantity measurements
    /// </summary>
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly List<QuantityMeasurementEntity> _cache = new List<QuantityMeasurementEntity>();
        private readonly object _lock = new object();
        private readonly ILogger<QuantityMeasurementCacheRepository> _logger;
        private int _nextId = 0;

        public QuantityMeasurementCacheRepository(ILogger<QuantityMeasurementCacheRepository>? logger = null)
        {
            _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger<QuantityMeasurementCacheRepository>.Instance;
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            lock (_lock)
            {
                _cache.Add(entity);
                _nextId++;
                _logger.LogDebug("Saved entity: {OperationType}", entity.OperationType);
            }
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            lock (_lock)
            {
                return new List<QuantityMeasurementEntity>(_cache);
            }
        }

        public QuantityMeasurementEntity? GetById(int id)
        {
            lock (_lock)
            {
                return id >= 0 && id < _cache.Count ? _cache[id] : null;
            }
        }

        public List<QuantityMeasurementEntity> GetByOperationType(string operationType)
        {
            if (string.IsNullOrWhiteSpace(operationType))
                throw new ArgumentException("Operation type cannot be null or empty", nameof(operationType));

            lock (_lock)
            {
                return _cache.Where(e => e.OperationType == operationType).ToList();
            }
        }

        public List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType)
        {
            if (string.IsNullOrWhiteSpace(measurementType))
                throw new ArgumentException("Measurement type cannot be null or empty", nameof(measurementType));

            lock (_lock)
            {
                return _cache.Where(e => e.FirstMeasurementType == measurementType 
                                       || e.SecondMeasurementType == measurementType
                                       || e.ResultMeasurementType == measurementType).ToList();
            }
        }

        public List<QuantityMeasurementEntity> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            lock (_lock)
            {
                return _cache.Where(e => e.Timestamp >= startDate && e.Timestamp <= endDate).ToList();
            }
        }

        public int GetTotalCount()
        {
            lock (_lock)
            {
                return _cache.Count;
            }
        }

        public int GetCountByOperationType(string operationType)
        {
            if (string.IsNullOrWhiteSpace(operationType))
                throw new ArgumentException("Operation type cannot be null or empty", nameof(operationType));

            lock (_lock)
            {
                return _cache.Count(e => e.OperationType == operationType);
            }
        }

        // ADD THIS MISSING METHOD
        private int GetCountByMeasurementType(string measurementType)
        {
            lock (_lock)
            {
                return _cache.Count(e => e.FirstMeasurementType == measurementType 
                                       || e.SecondMeasurementType == measurementType
                                       || e.ResultMeasurementType == measurementType);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _cache.Clear();
                _nextId = 0;
                _logger.LogInformation("Cache cleared");
            }
        }

        public bool DeleteById(int id)
        {
            lock (_lock)
            {
                if (id < 0 || id >= _cache.Count)
                    return false;

                _cache.RemoveAt(id);
                _logger.LogDebug("Deleted measurement with ID {Id}", id);
                return true;
            }
        }

        public string GetPoolStatistics()
        {
            var stats = new StringBuilder();
            stats.AppendLine("=== Cache Repository Statistics ===");
            stats.AppendLine($"Total Measurements: {GetTotalCount()}");
            stats.AppendLine($"COMPARE Operations: {GetCountByOperationType("COMPARE")}");
            stats.AppendLine($"CONVERT Operations: {GetCountByOperationType("CONVERT")}");
            stats.AppendLine($"ADD Operations: {GetCountByOperationType("ADD")}");
            stats.AppendLine($"SUBTRACT Operations: {GetCountByOperationType("SUBTRACT")}");
            stats.AppendLine($"DIVIDE Operations: {GetCountByOperationType("DIVIDE")}");
            
            stats.AppendLine($"\n=== Measurements by Type ===");
            stats.AppendLine($"Length: {GetCountByMeasurementType("Length")}");
            stats.AppendLine($"Weight: {GetCountByMeasurementType("Weight")}");
            stats.AppendLine($"Volume: {GetCountByMeasurementType("Volume")}");
            stats.AppendLine($"Temperature: {GetCountByMeasurementType("Temperature")}");
            
            return stats.ToString();
        }

        public void ReleaseResources()
        {
            lock (_lock)
            {
                _cache.Clear();
                _nextId = 0;
                _logger.LogInformation("Cache resources released");
            }
        }
    }
}
