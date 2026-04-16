using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using QuantityMeasurementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. ENHANCED LOGGING with Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .Enrich.FromLogContext()
          .Enrich.WithMachineName()
          .Enrich.WithThreadId()
          .WriteTo.Console()
          .WriteTo.File("logs/quantity-api-.log", 
              rollingInterval: RollingInterval.Day,
              retainedFileCountLimit: 7,
              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");
});

// 2. ADD SERVICES
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. DATABASE CONFIGURATION
builder.Services.AddDbContext<QuantityDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
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
        builder.Services.AddHealthChecks().AddRedis(redisConnection, "redis");
        Console.WriteLine("✅ Redis cache configured");
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
    Console.WriteLine("📦 Using in-memory cache");
}

// 5. RESPONSE CACHING
if (builder.Configuration.GetValue<bool>("CacheSettings:EnableResponseCaching", false))
{
    builder.Services.AddResponseCaching();
    Console.WriteLine("✅ Response caching enabled");
}

// 6. REPOSITORY & SERVICE REGISTRATION
builder.Services.AddSingleton<RepositoryFactory>();
builder.Services.AddSingleton<IQuantityMeasurementRepository>(sp =>
{
    var factory = sp.GetRequiredService<RepositoryFactory>();
    return factory.CreateRepository();
});
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

// 8. GOOGLE AUTHENTICATION LOGGING - FIXED LINE 236
var googleConfig = builder.Configuration.GetSection("GoogleAuth");
if (!string.IsNullOrEmpty(googleConfig["ClientId"]) && !string.IsNullOrEmpty(googleConfig["ClientSecret"]))
{
    // SAFE SUBSTRING HANDLING
    var clientId = googleConfig["ClientId"];
    var displayId = string.IsNullOrEmpty(clientId) ? "null" : 
                    clientId.Length <= 20 ? clientId : 
                    clientId.Substring(0, 20) + "...";
    Console.WriteLine($"✅ Google Authentication configured with ClientId: {displayId}");
}
else
{
    Console.WriteLine("⚠️ Google Authentication not configured. Set GoogleAuth:ClientId and GoogleAuth:ClientSecret in appsettings.json");
}

var app = builder.Build();

// 9. LOG APPLICATION STARTUP
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("🚀 Quantity Measurement API Starting...");
logger.LogInformation("📊 Using repository: {RepositoryType}", 
    app.Services.GetRequiredService<IQuantityMeasurementRepository>().GetType().Name);

// 10. HTTP PIPELINE


    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity API V1");
    });


app.UseHttpsRedirection();

if (builder.Configuration.GetValue<bool>("CacheSettings:EnableResponseCaching", false))
{
    app.UseResponseCaching();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

// 11. DATABASE VERIFICATION
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QuantityDbContext>();
    context.Database.EnsureCreated();
    logger.LogInformation("📊 Database verified");
}
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.Run();