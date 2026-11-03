using backend.Dtos;
using backend.Models;

namespace backend.Services;

public class IncidentService : IIncidentService
{
    private readonly List<Incident> _incidents = new();
    private int _nextId = 1;

    public IncidentResponse Create(CreateIncidentRequest request)
    {
        var incident = new Incident
        {
            Id = _nextId++,
            Number = $"INC-{DateTime.Now.Year}-{_nextId:D3}",
            Title = request.Title,
            Description = request.Description,
            CategoryId = request.CategoryId,
            Status = "New",
            AssigneeUserId = null // Изначально пусто
        };
        
        _incidents.Add(incident);
        return MapToResponse(incident);
    }

    public IEnumerable<IncidentResponse> GetAll()
    {
        return _incidents.Select(MapToResponse);
    }

    public IncidentResponse? GetById(int id)
    {
        var incident = _incidents.FirstOrDefault(i => i.Id == id);
        return incident == null ? null : MapToResponse(incident);
    }

    public IncidentResponse? Assign(int id, AssignRequest request)
    {
        var incident = _incidents.FirstOrDefault(i => i.Id == id);
        if (incident == null) return null;

        incident.AssigneeUserId = request.AssigneeUserId;
        return MapToResponse(incident);
    }

    public IncidentResponse? ChangeStatus(int id, StatusRequest request)
    {
        var incident = _incidents.FirstOrDefault(i => i.Id == id);
        if (incident == null) return null;

        // Проверка правила: нельзя в InProgress без исполнителя
        if (request.Status == "InProgress" && incident.AssigneeUserId == null)
        {
            throw new InvalidOperationException("Нельзя перевести в работу без назначенного исполнителя.");
        }

        var allowedStatuses = new[] { "New", "InProgress", "Closed", "Cancelled" };
        if (!allowedStatuses.Contains(request.Status))
        {
            throw new ArgumentException("Недопустимый статус.");
        }

        incident.Status = request.Status;
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