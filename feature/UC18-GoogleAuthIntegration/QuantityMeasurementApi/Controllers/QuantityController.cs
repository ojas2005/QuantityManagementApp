using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs;
using QuantityMeasurementApi.Models;

namespace QuantityMeasurementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]  // 👈 Require authentication for all endpoints
public class QuantityController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;
    private readonly ILogger<QuantityController> _logger;

    public QuantityController(
        IQuantityMeasurementService service,
        ILogger<QuantityController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Compare two quantities (Requires authentication)
    /// </summary>
    [HttpPost("compare")]
    [Authorize]
    public async Task<ActionResult<QuantityResponseDto>> Compare([FromBody] CompareRequest request)
    {
        try
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation("User {UserId} comparing {First} and {Second}", 
                userId, request.First, request.Second);

            var firstDto = new QuantityDTO(request.First.Value, request.First.Unit, request.First.MeasurementType);
            var secondDto = new QuantityDTO(request.Second.Value, request.Second.Unit, request.Second.MeasurementType);

            var result = _service.CompareQuantities(firstDto, secondDto);
            
            return Ok(QuantityResponseDto.FromEntity(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error comparing quantities");
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Convert a quantity (Requires authentication)
    /// </summary>
    [HttpPost("convert")]
    [Authorize]
    public async Task<ActionResult<QuantityResponseDto>> Convert([FromBody] ConvertRequest request)
    {
        try
        {
            var sourceDto = new QuantityDTO(request.Source.Value, request.Source.Unit, request.Source.MeasurementType);
            var result = _service.ConvertQuantity(sourceDto, request.TargetUnit);
            
            return Ok(QuantityResponseDto.FromEntity(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting quantity");
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Add two quantities (Requires authentication)
    /// </summary>
    [HttpPost("add")]
    [Authorize]
    public async Task<ActionResult<QuantityResponseDto>> Add([FromBody] ArithmeticRequest request)
    {
        try
        {
            var firstDto = new QuantityDTO(request.First.Value, request.First.Unit, request.First.MeasurementType);
            var secondDto = new QuantityDTO(request.Second.Value, request.Second.Unit, request.Second.MeasurementType);

            var result = string.IsNullOrEmpty(request.TargetUnit)
                ? _service.AddQuantities(firstDto, secondDto)
                : _service.AddQuantities(firstDto, secondDto, request.TargetUnit);

            return Ok(QuantityResponseDto.FromEntity(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding quantities");
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Subtract two quantities (Requires authentication)
    /// </summary>
    [HttpPost("subtract")]
    [Authorize]
    public async Task<ActionResult<QuantityResponseDto>> Subtract([FromBody] ArithmeticRequest request)
    {
        try
        {
            var firstDto = new QuantityDTO(request.First.Value, request.First.Unit, request.First.MeasurementType);
            var secondDto = new QuantityDTO(request.Second.Value, request.Second.Unit, request.Second.MeasurementType);

            var result = string.IsNullOrEmpty(request.TargetUnit)
                ? _service.SubtractQuantities(firstDto, secondDto)
                : _service.SubtractQuantities(firstDto, secondDto, request.TargetUnit);

            return Ok(QuantityResponseDto.FromEntity(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error subtracting quantities");
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Divide two quantities (Requires authentication)
    /// </summary>
    [HttpPost("divide")]
    [Authorize]
    public async Task<ActionResult<QuantityResponseDto>> Divide([FromBody] ArithmeticRequest request)
    {
        try
        {
            var firstDto = new QuantityDTO(request.First.Value, request.First.Unit, request.First.MeasurementType);
            var secondDto = new QuantityDTO(request.Second.Value, request.Second.Unit, request.Second.MeasurementType);

            var result = _service.DivideQuantities(firstDto, secondDto);
            return Ok(QuantityResponseDto.FromEntity(result));
        }
        catch (DivideByZeroException)
        {
            return BadRequest(new { error = "Cannot divide by zero" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error dividing quantities");
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Get user's operation history (Requires authentication)
    /// </summary>
    [HttpGet("history")]
    [Authorize]
    public async Task<ActionResult<List<QuantityResponseDto>>> GetHistory(
        [FromQuery] string? operation = null,
        [FromQuery] string? type = null)
    {
        try
        {
            // TODO: Implement user-specific history
            return Ok(new List<QuantityResponseDto>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting history");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}
