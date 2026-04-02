using Microsoft.Extensions.Caching.Memory;
using ModelLayer.Interfaces;

namespace QuantityMeasurementApi.Services;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<MemoryCacheService> _logger;

    public MemoryCacheService(IMemoryCache cache, ILogger<MemoryCacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public Task<T?> GetAsync<T>(string key)
    {
        try
        {
            if (_cache.TryGetValue(key, out T? value))
            {
                _logger.LogDebug("Memory cache hit for key: {Key}", key);
                return Task.FromResult(value);
            }
            
            _logger.LogDebug("Memory cache miss for key: {Key}", key);
            return Task.FromResult(default(T));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting from memory cache for key: {Key}", key);
            return Task.FromResult(default(T));
        }
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        try
        {
            var options = new MemoryCacheEntryOptions();
            if (expiration.HasValue)
                options.AbsoluteExpirationRelativeToNow = expiration;
            else
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            
            options.SlidingExpiration = TimeSpan.FromMinutes(5);
            
            _cache.Set(key, value, options);
            _logger.LogDebug("Cached in memory key: {Key}", key);
            
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting memory cache for key: {Key}", key);
            return Task.CompletedTask;
        }
    }

    public Task RemoveAsync(string key)
    {
        try
        {
            _cache.Remove(key);
            _logger.LogDebug("Removed memory cache key: {Key}", key);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing memory cache key: {Key}", key);
            return Task.CompletedTask;
        }
    }

    public Task<bool> ExistsAsync(string key)
    {
        try
        {
            return Task.FromResult(_cache.TryGetValue(key, out _));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking memory cache existence for key: {Key}", key);
            return Task.FromResult(false);
        }
    }
}
