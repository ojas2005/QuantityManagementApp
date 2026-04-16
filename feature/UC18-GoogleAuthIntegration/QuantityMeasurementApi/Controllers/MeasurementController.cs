using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementApi.Controllers;

/// <summary>
/// Measurement history controller — matches the Angular frontend's expected API shape:
///   POST /api/Measurement        — save a completed operation
///   GET  /api/Measurement/history — fetch all past operations for history UI
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MeasurementController : ControllerBase
{
    private readonly QuantityDbContext _db;
    private readonly ILogger<MeasurementController> _logger;

    public MeasurementController(QuantityDbContext db, ILogger<MeasurementController> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// Save a completed measurement operation sent from the Angular calculator.
    /// The frontend sends a flat MeasurementRecord payload.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Save([FromBody] MeasurementRecordDto dto)
    {
        try
        {
            var entity = new QuantityMeasurementEntity
            {
                OperationType    = dto.OperationType?.ToUpper() ?? "UNKNOWN",
                FirstValue       = dto.FirstValue,
                FirstUnit        = dto.FirstUnit,
                FirstMeasurementType = dto.FirstMeasurementType,
                SecondValue      = dto.SecondValue,
                SecondUnit       = dto.SecondUnit,
                ResultValue      = dto.ResultValue,
                ResultUnit       = dto.ResultUnit,
                HasError         = false,
                Timestamp        = DateTime.UtcNow
            };

            _db.QuantityMeasurements.Add(entity);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Saved measurement: {Op}", entity.OperationType);
            return Ok(new { success = true, id = entity.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving measurement");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    /// <summary>
    /// GET /api/Measurement/history — returns all operations, newest first.
    /// Supports optional ?operation= and ?type= filters (matching Angular history component).
    /// </summary>
    [HttpGet("history")]
    public async Task<IActionResult> GetHistory(
        [FromQuery] string? operation = null,
        [FromQuery] string? type = null)
    {
        try
        {
            var query = _db.QuantityMeasurements.AsQueryable();

            if (!string.IsNullOrEmpty(operation))
                query = query.Where(e => e.OperationType.ToLower() == operation.ToLower());

            if (!string.IsNullOrEmpty(type))
                query = query.Where(e => e.FirstMeasurementType != null &&
                                         e.FirstMeasurementType.ToLower() == type.ToLower());

            var records = await query
                .OrderByDescending(e => e.Timestamp)
                .Take(200)   // cap at 200 most-recent entries
                .Select(e => new
                {
                    id                  = e.Id,
                    operationType       = e.OperationType,
                    firstValue          = e.FirstValue,
                    firstUnit           = e.FirstUnit,
                    firstMeasurementType = e.FirstMeasurementType,
                    secondValue         = e.SecondValue,
                    secondUnit          = e.SecondUnit,
                    resultValue         = e.ResultValue,
                    resultUnit          = e.ResultUnit,
                    description         = BuildDescription(e),
                    timestamp           = e.Timestamp
                })
                .ToListAsync();

            return Ok(records);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching measurement history");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    private static string BuildDescription(QuantityMeasurementEntity e)
    {
        return e.OperationType?.ToUpper() switch
        {
            "CONVERT"  => $"{e.FirstValue} {e.FirstUnit} → {e.ResultValue} {e.ResultUnit}",
            "ADD"      => $"{e.FirstValue} {e.FirstUnit} + {e.SecondValue} {e.SecondUnit} = {e.ResultValue} {e.ResultUnit}",
            "SUBTRACT" => $"{e.FirstValue} {e.FirstUnit} − {e.SecondValue} {e.SecondUnit} = {e.ResultValue} {e.ResultUnit}",
            "MULTIPLY" => $"{e.FirstValue} {e.FirstUnit} × {e.SecondValue} {e.SecondUnit} = {e.ResultValue} {e.ResultUnit}",
            "DIVIDE"   => $"{e.FirstValue} {e.FirstUnit} ÷ {e.SecondValue} {e.SecondUnit} = {e.ResultValue}",
            "COMPARE"  => $"{e.FirstValue} {e.FirstUnit} {(e.ResultValue == 1 ? "==" : "!=")} {e.SecondValue} {e.SecondUnit}",
            _          => $"{e.OperationType} operation"
        };
    }
}

/// <summary>Matches the flat MeasurementRecord shape the Angular frontend posts.</summary>
public class MeasurementRecordDto
{
    public string? OperationType       { get; set; }
    public double  FirstValue          { get; set; }
    public string? FirstUnit           { get; set; }
    public string? FirstMeasurementType{ get; set; }
    public double? SecondValue         { get; set; }
    public string? SecondUnit          { get; set; }
    public double  ResultValue         { get; set; }
    public string? ResultUnit          { get; set; }
    public string? Description         { get; set; }
}
