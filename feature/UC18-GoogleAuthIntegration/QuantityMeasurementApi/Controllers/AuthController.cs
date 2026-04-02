using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;
using ModelLayer.Models;
using QuantityMeasurementApi.Services;
using RepositoryLayer.Interfaces;

namespace QuantityMeasurementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly IGoogleAuthService _googleAuthService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordService passwordService,
        IGoogleAuthService googleAuthService,
        IConfiguration configuration,
        ILogger<AuthController> logger)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordService = passwordService;
        _googleAuthService = googleAuthService;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            _logger.LogInformation("Registration attempt for email: {Email}", request.Email);

            var exists = await _userRepository.ExistsAsync(request.Email, request.Username);
            if (exists)
            {
                return BadRequest(new AuthResponse
                {
                    Success = false,
                    Message = "Email or username already exists"
                });
            }

            var user = new User
            {
                Email = request.Email.ToLower(),
                Username = request.Username.ToLower(),
                FullName = request.FullName,
                PasswordHash = _passwordService.HashPassword(request.Password),
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var createdUser = await _userRepository.CreateAsync(user);
            var token = _jwtService.GenerateToken(createdUser);

            return Ok(new AuthResponse
            {
                Success = true,
                Token = token,
                User = new UserDto
                {
                    Id = createdUser.Id,
                    Email = createdUser.Email,
                    Username = createdUser.Username,
                    FullName = createdUser.FullName,
                    Role = createdUser.Role,
                    CreatedAt = createdUser.CreatedAt
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            return StatusCode(500, new AuthResponse
            {
                Success = false,
                Message = "An error occurred during registration"
            });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(request.EmailOrUsername.ToLower())
                ?? await _userRepository.GetByUsernameAsync(request.EmailOrUsername.ToLower());

            if (user == null)
            {
                return Unauthorized(new AuthResponse
                {
                    Success = false,
                    Message = "Invalid credentials"
                });
            }

            if (!user.IsActive)
            {
                return Unauthorized(new AuthResponse
                {
                    Success = false,
                    Message = "Account is deactivated"
                });
            }

            if (string.IsNullOrEmpty(user.PasswordHash) || 
                !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized(new AuthResponse
                {
                    Success = false,
                    Message = "Invalid credentials"
                });
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponse
            {
                Success = true,
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    FullName = user.FullName,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return StatusCode(500, new AuthResponse
            {
                Success = false,
                Message = "An error occurred during login"
            });
        }
    }

    [HttpPost("google-login")]
    public async Task<ActionResult<AuthResponse>> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        try
        {
            var googleUser = await _googleAuthService.ValidateGoogleTokenAsync(request.IdToken);
            if (googleUser == null || !googleUser.EmailVerified)
            {
                return Unauthorized(new AuthResponse
                {
                    Success = false,
                    Message = "Invalid Google token"
                });
            }

            var user = await _userRepository.GetByEmailAsync(googleUser.Email);
            if (user == null)
            {
                user = new User
                {
                    Email = googleUser.Email,
                    Username = googleUser.Email.Split('@')[0],
                    FullName = googleUser.Name,
                    ProfilePicture = googleUser.Picture,
                    Role = "User",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                await _userRepository.CreateAsync(user);
            }
            else if (string.IsNullOrEmpty(user.GoogleId))
            {
                user.GoogleId = googleUser.Id;
                user.LastLoginAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponse
            {
                Success = true,
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    FullName = user.FullName,
                    ProfilePicture = user.ProfilePicture,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Google login");
            return StatusCode(500, new AuthResponse
            {
                Success = false,
                Message = "An error occurred during Google login"
            });
        }
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback([FromQuery] string code)
    {
        try
        {
            _logger.LogInformation("Google callback received");

            if (string.IsNullOrEmpty(code))
            {
                return BadRequest(new { error = "No authorization code received" });
            }

            // Exchange code for tokens
            var client = new HttpClient();
            var tokenRequest = new Dictionary<string, string>
            {
                ["code"] = code,
                ["client_id"] = _configuration["GoogleAuth:ClientId"] ?? "",
                ["client_secret"] = _configuration["GoogleAuth:ClientSecret"] ?? "",
                ["redirect_uri"] = "http://localhost:5000/api/Auth/google-callback",
                ["grant_type"] = "authorization_code"
            };

            var content = new FormUrlEncodedContent(tokenRequest);
            var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Token exchange failed: {Response}", responseContent);
                return BadRequest(new { error = "Token exchange failed" });
            }

            var tokenData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
            var idToken = tokenData?.GetValueOrDefault("id_token")?.ToString();

            if (string.IsNullOrEmpty(idToken))
            {
                return BadRequest(new { error = "No ID token received" });
            }

            var googleUser = await _googleAuthService.ValidateGoogleTokenAsync(idToken);
            if (googleUser == null)
            {
                return BadRequest(new { error = "Invalid Google token" });
            }

            // Find or create user
            var user = await _userRepository.GetByEmailAsync(googleUser.Email);
            if (user == null)
            {
                user = new User
                {
                    Email = googleUser.Email,
                    Username = googleUser.Email.Split('@')[0],
                    FullName = googleUser.Name,
                    ProfilePicture = googleUser.Picture,
                    Role = "User",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                await _userRepository.CreateAsync(user);
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var jwtToken = _jwtService.GenerateToken(user);

            return Ok(new
            {
                success = true,
                token = jwtToken,
                user = new
                {
                    user.Id,
                    user.Email,
                    user.Username,
                    user.FullName,
                    user.ProfilePicture,
                    user.Role,
                    user.CreatedAt
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Google callback");
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
