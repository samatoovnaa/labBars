using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/incidents")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentService _service;
    private readonly ILogger<IncidentsController> _logger;

    public IncidentsController(IIncidentService service, ILogger<IncidentsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateIncidentRequest request)
    {
        try
        {
            // Временный сбой для ЛР8 
            // if (request.Title.Contains("Сбой")) throw new Exception("Учебный сбой!");
            
            var result = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result); // 201
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateIncident failed; categoryId={CategoryId}", request.CategoryId);
            return Problem(statusCode: 500, title: "Внутренняя ошибка сервера");
        }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll()); // 200
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);
        if (result == null) return NotFound(); // 404
        return Ok(result); // 200
    }

    [HttpPatch("{id:int}/assignee")]
    public IActionResult Assign(int id, [FromBody] AssignRequest request)
    {
        var result = _service.Assign(id, request);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPatch("{id:int}/status")]
    public IActionResult ChangeStatus(int id, [FromBody] StatusRequest request)
    {
        try
        {
            var result = _service.ChangeStatus(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { detail = ex.Message }); // 409
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { detail = ex.Message }); // 400
        }
    }
}