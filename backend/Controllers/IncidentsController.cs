using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

    // Только Диспетчер
    [Authorize(Roles = "Dispatcher")]
    [HttpPost]
    public IActionResult Create([FromBody] CreateIncidentRequest request)
    {
        try
        {
            var result = _service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateIncident failed; categoryId={CategoryId}", request.CategoryId);
            return Problem(statusCode: 500, title: "Внутренняя ошибка сервера");
        }
    }

    // Любой авторизованный
    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    // Любой авторизованный
    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var result = _service.GetById(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    // Только Диспетчер
    [Authorize(Roles = "Dispatcher")]
    [HttpPatch("{id:int}/assignee")]
    public IActionResult Assign(int id, [FromBody] AssignRequest request)
    {
        var result = _service.Assign(id, request);
        if (result == null) return NotFound();
        return Ok(result);
    }

    // Любой, но с проверкой владельца внутри
    [Authorize]
    [HttpPatch("{id:int}/status")]
    public IActionResult ChangeStatus(int id, [FromBody] StatusRequest request)
    {
        try
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(userIdStr ?? "0");

            var result = _service.ChangeStatus(id, request, userId, role);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid(); // Вернет 403 (нет прав)
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