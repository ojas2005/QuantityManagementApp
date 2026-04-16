using System.Text;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Interfaces;
using QuantityMeasurementApi.Services;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// 1. LOGGING

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .WriteTo.File("logs/quantity-api-.log", rollingInterval: RollingInterval.Day);
});


// 2. AUTHENTICATION - JWT + Google

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secret = jwtSettings["Secret"] ?? "DevelopmentSecretKeyForTestingOnly1234567890!@#$%";
var secretKey = Encoding.UTF8.GetBytes(secret);

// Add authentication with multiple schemes
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "QuantityMeasurementApi",
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"] ?? "QuantityMeasurementClients",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["GoogleAuth:ClientId"] ?? "";
    options.ClientSecret = builder.Configuration["GoogleAuth:ClientSecret"] ?? "";
    options.CallbackPath = "/signin-google";
    options.SaveTokens = true;
    
    // Add additional scopes if needed
    options.Scope.Add("profile");
    options.Scope.Add("email");
    
    Console.WriteLine($" Google Auth configured with ClientId: {(options.ClientId?.Length > 20 ? options.ClientId?.Substring(0, 20) : options.ClientId)}...");
});


// 3. AUTHORIZATION

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User", "Admin"));
});


// 4. ADD SERVICES

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token"
    });
    
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// 5. DATABASE CONFIGURATION

builder.Services.AddDbContext<QuantityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// 6. REDIS CACHE

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
        Console.WriteLine(" Redis cache configured and enabled");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Redis configuration failed: {ex.Message}");
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


// 7. RESPONSE CACHING

if (builder.Configuration.GetValue<bool>("CacheSettings:EnableResponseCaching", false))
{
    builder.Services.AddResponseCaching();
}


// 8. CUSTOM SERVICES

builder.Services.AddHttpClient();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<RepositoryFactory>();
builder.Services.AddScoped<IQuantityMeasurementRepository>(sp =>
{
    var factory = sp.GetRequiredService<RepositoryFactory>();
    return factory.CreateRepository();
});
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();


// 9. CORS

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


// 10. HTTP PIPELINE

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity API V1");
    });
}

app.UseCors("AllowAll");

// Skip HTTPS redirect in development (Angular dev server calls http://localhost:5000)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

// Health check endpoint for frontend
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));


// 11. DATABASE VERIFICATION & MIGRATION

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<QuantityDbContext>();
        // context.Database.EnsureCreated(); // Replaced with Migrate()
        context.Database.Migrate();
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("✅ Database migrated and verified");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "❌ An error occurred while migrating the database.");
        Console.WriteLine($" Database connection issue: {ex.Message}");
    }
}

// Log Google Auth status
var googleConfig = builder.Configuration.GetSection("GoogleAuth");
if (!string.IsNullOrEmpty(googleConfig["ClientId"]) && !string.IsNullOrEmpty(googleConfig["ClientSecret"]))
{
    var clientId = googleConfig["ClientId"];
    Console.WriteLine($" Google Authentication configured with ClientId: {(clientId != null && clientId.Length > 20 ? clientId.Substring(0, 20) : clientId)}...");
}
else
{
    Console.WriteLine(" Google Authentication not configured. Set GoogleAuth:ClientId and GoogleAuth:ClientSecret in appsettings.json");
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();
