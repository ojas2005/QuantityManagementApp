using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Entities;
using ModelLayer.Exceptions;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using System.Text;

namespace RepositoryLayer.Repositories
{
    /// <summary>
    /// Database repository implementation using Entity Framework Core
    /// </summary>
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository
    {
        private readonly QuantityDbContext _context;
        private readonly ILogger<QuantityMeasurementDatabaseRepository> _logger;
        private readonly object _lock = new object();
        private DateTime _lastStatsUpdate;
        private int _activeConnections;

        public QuantityMeasurementDatabaseRepository(
            QuantityDbContext context,
            ILogger<QuantityMeasurementDatabaseRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _lastStatsUpdate = DateTime.Now;
            
            _logger.LogInformation("QuantityMeasurementDatabaseRepository initialized");
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _context.QuantityMeasurements.Add(entity);
                _context.SaveChanges();
                
                _logger.LogDebug("Saved entity: {OperationType} at {Timestamp}", 
                    entity.OperationType, entity.Timestamp);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error while saving entity");
                throw new DatabaseException("Failed to save measurement to database", ex);
            }
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            try
            {
                return _context.QuantityMeasurements
                    .OrderByDescending(e => e.Timestamp)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all measurements");
                throw new DatabaseException("Failed to retrieve measurements from database", ex);
            }
        }

        public QuantityMeasurementEntity? GetById(int id)
        {
            try
            {
                return _context.QuantityMeasurements.Find(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving measurement with ID {Id}", id);
                throw new DatabaseException($"Failed to retrieve measurement with ID {id}", ex);
            }
        }

        public List<QuantityMeasurementEntity> GetByOperationType(string operationType)
        {
            if (string.IsNullOrWhiteSpace(operationType))
                throw new ArgumentException("Operation type cannot be null or empty", nameof(operationType));

            try
            {
                return _context.QuantityMeasurements
                    .Where(e => e.OperationType == operationType)
                    .OrderByDescending(e => e.Timestamp)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving measurements by operation type: {OperationType}", operationType);
                throw new DatabaseException($"Failed to retrieve measurements by operation type: {operationType}", ex);
            }
        }

        public List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType)
        {
            if (string.IsNullOrWhiteSpace(measurementType))
                throw new ArgumentException("Measurement type cannot be null or empty", nameof(measurementType));

            try
            {
                return _context.QuantityMeasurements
                    .Where(e => e.FirstMeasurementType == measurementType 
                             || e.SecondMeasurementType == measurementType
                             || e.ResultMeasurementType == measurementType)
                    .OrderByDescending(e => e.Timestamp)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving measurements by measurement type: {MeasurementType}", measurementType);
                throw new DatabaseException($"Failed to retrieve measurements by type: {measurementType}", ex);
            }
        }

        public List<QuantityMeasurementEntity> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _context.QuantityMeasurements
                    .Where(e => e.Timestamp >= startDate && e.Timestamp <= endDate)
                    .OrderByDescending(e => e.Timestamp)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving measurements by date range");
                throw new DatabaseException("Failed to retrieve measurements by date range", ex);
            }
        }

        public int GetTotalCount()
        {
            try
            {
                return _context.QuantityMeasurements.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total count");
                throw new DatabaseException("Failed to get total count", ex);
            }
        }

        public int GetCountByOperationType(string operationType)
        {
            if (string.IsNullOrWhiteSpace(operationType))
                throw new ArgumentException("Operation type cannot be null or empty", nameof(operationType));

            try
            {
                return _context.QuantityMeasurements.Count(e => e.OperationType == operationType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting count for operation type: {OperationType}", operationType);
                throw new DatabaseException($"Failed to get count for operation type: {operationType}", ex);
            }
        }

        public void Clear()
        {
            try
            {
                _context.QuantityMeasurements.RemoveRange(_context.QuantityMeasurements);
                _context.SaveChanges();
                
                _logger.LogInformation("Cleared all measurements from database");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing all measurements");
                throw new DatabaseException("Failed to clear measurements from database", ex);
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                var entity = _context.QuantityMeasurements.Find(id);
                if (entity == null)
                    return false;

                _context.QuantityMeasurements.Remove(entity);
                _context.SaveChanges();
                
                _logger.LogDebug("Deleted measurement with ID {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting measurement with ID {Id}", id);
                throw new DatabaseException($"Failed to delete measurement with ID {id}", ex);
            }
        }

        public string GetPoolStatistics()
        {
            var stats = new StringBuilder();
            stats.AppendLine("=== Database Statistics ===");
            stats.AppendLine($"Total Measurements: {GetTotalCount()}");
            stats.AppendLine($"COMPARE Operations: {GetCountByOperationType("COMPARE")}");
            stats.AppendLine($"CONVERT Operations: {GetCountByOperationType("CONVERT")}");
            stats.AppendLine($"ADD Operations: {GetCountByOperationType("ADD")}");
            stats.AppendLine($"SUBTRACT Operations: {GetCountByOperationType("SUBTRACT")}");
            stats.AppendLine($"DIVIDE Operations: {GetCountByOperationType("DIVIDE")}");
            
            // Get counts by measurement type
            stats.AppendLine($"\n=== Measurements by Type ===");
            stats.AppendLine($"Length: {GetCountByMeasurementType("Length")}");
            stats.AppendLine($"Weight: {GetCountByMeasurementType("Weight")}");
            stats.AppendLine($"Volume: {GetCountByMeasurementType("Volume")}");
            stats.AppendLine($"Temperature: {GetCountByMeasurementType("Temperature")}");
            
            return stats.ToString();
        }

        private int GetCountByMeasurementType(string measurementType)
        {
            return _context.QuantityMeasurements
                .Count(e => e.FirstMeasurementType == measurementType 
                         || e.SecondMeasurementType == measurementType
                         || e.ResultMeasurementType == measurementType);
        }

        public void ReleaseResources()
        {
            try
            {
                _context.Dispose();
                _logger.LogInformation("Database context disposed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error releasing database resources");
            }
        }
    }
}