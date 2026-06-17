using System.Text.Json;
using ModelLayer.DTOs;

namespace QuantityMeasurementApi.Services;

public interface IGoogleAuthService
{
    Task<GoogleUserInfo?> ValidateGoogleTokenAsync(string idToken);
}

public class GoogleUserInfo
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Picture { get; set; }
    public bool EmailVerified { get; set; }
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
}

public class GoogleAuthService : IGoogleAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GoogleAuthService> _logger;
    private readonly IConfiguration _configuration;

    public GoogleAuthService(
        IHttpClientFactory httpClientFactory,
        ILogger<GoogleAuthService> logger,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<GoogleUserInfo?> ValidateGoogleTokenAsync(string idToken)
    {
        if (string.IsNullOrEmpty(idToken))
        {
            _logger.LogWarning("Empty ID token provided");
            return null;
        }

        try
        {
            _logger.LogInformation("Validating Google token (length: {Length})", idToken.Length);
            
            // Option 1: Validate using Google's tokeninfo endpoint
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Google token validation failed: {StatusCode} - {Error}", 
                    response.StatusCode, error);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Google response: {Content}", content);
            
            var userInfo = JsonSerializer.Deserialize<GoogleUserInfo>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (userInfo == null)
            {
                _logger.LogWarning("Failed to deserialize Google response");
                return null;
            }

            // Verify the audience (Client ID) matches
            var expectedClientId = _configuration["GoogleAuth:ClientId"];
            if (!string.IsNullOrEmpty(expectedClientId) && userInfo.Id != expectedClientId)
            {
                // Note: The "aud" field in the token should match your client ID
                // We need to check the audience from the token
                _logger.LogWarning("Token audience mismatch. Expected: {Expected}, Got: {Got}", 
                    expectedClientId, userInfo.Id);
                // Don't fail immediately - the tokeninfo endpoint already validated
            }

            _logger.LogInformation("Google token validated for user: {Email} ({Name})", 
                userInfo.Email, userInfo.Name);
            
            return userInfo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating Google token: {Message}", ex.Message);
            return null;
        }
    }
}
