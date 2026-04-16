using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTOs;
using ModelLayer.Models;

namespace QuantityMeasurementApi.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    ClaimsPrincipal? ValidateToken(string token);
    string GenerateRefreshToken();
}

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<JwtService> _logger;

    public JwtService(IConfiguration configuration, ILogger<JwtService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string GenerateToken(User user)
    {
        try
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            
            // Robust secret reading: handles empty strings (not just null) from appsettings.json
            var secretKey = jwtSettings["Secret"];
            if (string.IsNullOrEmpty(secretKey)) secretKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secretKey)) secretKey = _configuration["Jwt__Key"];
            if (string.IsNullOrEmpty(secretKey)) secretKey = "DevelopmentSecretKeyForTestingOnly1234567890!@#$%ABCDEF";

            // Diagnostics: log the source of the secret (not the value itself)
            Console.WriteLine($"🔑 JwtService.GenerateToken: Secret length={secretKey.Length}, IsNullOrEmpty={string.IsNullOrEmpty(jwtSettings["Secret"])}");

            var issuer = jwtSettings["Issuer"] ?? "QuantityMeasurementApi";
            var audience = jwtSettings["Audience"] ?? "QuantityMeasurementClients";
            var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating JWT token");
            throw;
        }
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        try
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            
            var secretKey = jwtSettings["Secret"];
            if (string.IsNullOrEmpty(secretKey)) secretKey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secretKey)) secretKey = _configuration["Jwt__Key"];
            if (string.IsNullOrEmpty(secretKey)) secretKey = "DevelopmentSecretKeyForTestingOnly1234567890!@#$%ABCDEF";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"] ?? "QuantityMeasurementApi",
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"] ?? "QuantityMeasurementClients",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
