using backend.Data;
using backend.Dtos;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class IncidentService : IIncidentService
{
    private readonly AppDbContext _context;

    public IncidentService(AppDbContext context)
    {
        _context = context;
    }

    public IncidentResponse Create(CreateIncidentRequest request)
    {
        var incident = new Incident
        {
            Number = $"INC-{DateTime.Now.Ticks.ToString().Substring(10)}",
            Title = request.Title,
            Description = request.Description,
            CategoryId = request.CategoryId,
            Status = "New",
            CreatedByUserId = 1, // Пока ставим первого юзера (для теста)
            AssigneeUserId = null
        };
        
        _context.Incidents.Add(incident);
        _context.SaveChanges(); // СОХРАНЯЕМ В БАЗУ!
        
        return MapToResponse(incident);
    }

    public IEnumerable<IncidentResponse> GetAll()
    {
        return _context.Incidents.Select(MapToResponse).ToList();
    }

    public IncidentResponse? GetById(int id)
    {
        var incident = _context.Incidents.FirstOrDefault(i => i.Id == id);
        return incident == null ? null : MapToResponse(incident);
    }

    public IncidentResponse? Assign(int id, AssignRequest request)
    {
        var incident = _context.Incidents.FirstOrDefault(i => i.Id == id);
        if (incident == null) return null;

        incident.AssigneeUserId = request.AssigneeUserId;
        _context.SaveChanges(); // СОХРАНЯЕМ В БАЗУ!
        return MapToResponse(incident);
    }

    public IncidentResponse? ChangeStatus(int id, StatusRequest request)
    {
        var incident = _context.Incidents.FirstOrDefault(i => i.Id == id);
        if (incident == null) return null;

        if (request.Status == "InProgress" && incident.AssigneeUserId == null)
            throw new InvalidOperationException("Нельзя перевести в работу без исполнителя.");

        var allowedStatuses = new[] { "New", "InProgress", "Closed", "Cancelled" };
        if (!allowedStatuses.Contains(request.Status))
            throw new ArgumentException("Недопустимый статус.");

        incident.Status = request.Status;
        _context.SaveChanges(); // СОХРАНЯЕМ В БАЗУ!
        return MapToResponse(incident);
    }

    private static IncidentResponse MapToResponse(Incident i)
    {
        return new IncidentResponse
        {
            Id = i.Id,
            Number = i.Number,
            Title = i.Title,
            Description = i.Description,
            Status = i.Status,
            CategoryId = i.CategoryId,
            AssigneeUserId = i.AssigneeUserId
        };
    }
}