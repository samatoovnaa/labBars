using backend.Dtos;
using backend.Models;

namespace backend.Services;

public interface IIncidentService
{
    IncidentResponse Create(CreateIncidentRequest request);
    IEnumerable<IncidentResponse> GetAll();
    IncidentResponse? GetById(int id);
    IncidentResponse? Assign(int id, AssignRequest request);
    IncidentResponse? ChangeStatus(int id, StatusRequest request);
}