using backend.Dtos;

namespace backend.Services;

public interface IIncidentService
{
    IncidentResponse Create(CreateIncidentRequest request);
    IEnumerable<IncidentResponse> GetAll();
    IncidentResponse? GetById(int id);
    IncidentResponse? Assign(int id, AssignRequest request);
    
    // Вот здесь обязательно должно быть 4 параметра!
    IncidentResponse? ChangeStatus(int id, StatusRequest request, int userId, string? role);
}