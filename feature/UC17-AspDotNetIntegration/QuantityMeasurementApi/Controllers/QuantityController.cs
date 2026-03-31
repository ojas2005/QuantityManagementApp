using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Entities;
using QuantityMeasurementApi.Models;

namespace QuantityMeasurementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
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

    [HttpPost("compare")]
    public async Task<ActionResult<QuantityResponseDto>> Compare([FromBody] CompareRequest request)
    {
        try
        {
            _logger.LogInformation("Comparing {FirstValue}{FirstUnit} and {SecondValue}{SecondUnit}", 
                request.First.Value, request.First.Unit, request.Second.Value, request.Second.Unit);

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

    [HttpPost("convert")]
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

    [HttpPost("add")]
    public async Task<ActionResult<QuantityResponseDto>> Add([FromBody] ArithmeticRequest request)
    {
        try
        {
            var firstDto = new QuantityDTO(request.First.Value, request.First.Unit, request.First.MeasurementType);
            var secondDto = new QuantityDTO(request.Second.Value, request.Second.Unit, request.Second.MeasurementType);

            QuantityMeasurementEntity result;
            if (string.IsNullOrEmpty(request.TargetUnit))
                result = _service.AddQuantities(firstDto, secondDto);
            else
                result = _service.AddQuantities(firstDto, secondDto, request.TargetUnit);

            return Ok(QuantityResponseDto.FromEntity(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding quantities");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("subtract")]
    public async Task<ActionResult<QuantityResponseDto>> Subtract([FromBody] ArithmeticRequest request)
    {
        try
        {
            var firstDto = new QuantityDTO(request.First.Value, request.First.Unit, request.First.MeasurementType);
            var secondDto = new QuantityDTO(request.Second.Value, request.Second.Unit, request.Second.MeasurementType);

            QuantityMeasurementEntity result;
            if (string.IsNullOrEmpty(request.TargetUnit))
                result = _service.SubtractQuantities(firstDto, secondDto);
            else
                result = _service.SubtractQuantities(firstDto, secondDto, request.TargetUnit);

            return Ok(QuantityResponseDto.FromEntity(result));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error subtracting quantities");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("divide")]
    public async Task<ActionResult<QuantityResponseDto>> Divide([FromBody] ArithmeticRequest request)
    {
        try
        {
            var firstDto = new QuantityDTO(request.First.Value, request.First.Unit, request.First.MeasurementType);
            var secondDto = new QuantityDTO(request.Second.Value, request.Second.Unit, request.Second.MeasurementType);

            var result = _service.DivideQuantities(firstDto, secondDto);
            return Ok(QuantityResponseDto.FromEntity(result));
        }
        catch (DivideByZeroException ex)
        {
            return BadRequest(new { error = "Cannot divide by zero" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error dividing quantities");
            return BadRequest(new { error = ex.Message });
        }
    }
}
