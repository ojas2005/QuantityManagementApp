using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ModelLayer.Interfaces;
using QuantityMeasurementApi.Services;

var builder = WebApplication.CreateBuilder(args);


// 1. LOGGING

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .WriteTo.File("logs/quantity-api-.log", 
              rollingInterval: RollingInterval.Day,
              retainedFileCountLimit: 7);
});


// 2. ADD SERVICES

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// 3. DATABASE CONFIGURATION

builder.Services.AddDbContext<QuantityDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});


// 4. REDIS CACHE CONFIGURATION

var useRedis = builder.Configuration.GetValue<bool>("CacheSettings:UseRedisCache", false);
var redisConnection = builder.Configuration.GetConnectionString("Redis");

if (useRedis && !string.IsNullOrEmpty(redisConnection))
{
    try
    {
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnection;
            options.InstanceName = builder.Configuration.GetValue<string>("RedisCache:InstanceName", "QuantityApi_");
        });
        builder.Services.AddSingleton<ICacheService, RedisCacheService>();
        Console.WriteLine("✅ Redis cache configured and enabled");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Redis configuration failed: {ex.Message}. Falling back to memory cache.");
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
    }
}
else
{
    builder.Services.AddMemoryCache();
    builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
    Console.WriteLine("📦 Using in-memory cache (Redis not configured)");
}


// 5. RESPONSE CACHING

if (builder.Configuration.GetValue<bool>("CacheSettings:EnableResponseCaching", false))
{
    builder.Services.AddResponseCaching();
    Console.WriteLine("✅ Response caching enabled");
}


// 6. REPOSITORY & SERVICE REGISTRATION

// Register RepositoryFactory as Singleton
builder.Services.AddSingleton<RepositoryFactory>();

// Register Repository as Scoped (important!)
builder.Services.AddScoped<IQuantityMeasurementRepository>(sp =>
{
    var factory = sp.GetRequiredService<RepositoryFactory>();
    return factory.CreateRepository();
});

// Register Service as Scoped
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();


// 7. CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


// 8. HTTP PIPELINE

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();


// 9. LOG APPLICATION STARTUP (inside a scope!)

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("🚀 Quantity Measurement API Starting...");

// IMPORTANT: Use a scope to resolve scoped services
using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<IQuantityMeasurementRepository>();
    logger.LogInformation("📊 Using repository: {RepositoryType}", repo.GetType().Name);
    
    // Verify database connection
    var context = scope.ServiceProvider.GetRequiredService<QuantityDbContext>();
    context.Database.EnsureCreated();
    logger.LogInformation("📊 Database verified");
}

logger.LogInformation("✅ API Ready! Swagger: http://localhost:5000/swagger");

app.Run();
