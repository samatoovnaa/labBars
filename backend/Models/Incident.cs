namespace backend.Models;

public class Incident
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "New";
    public int CategoryId { get; set; }
    public int? AssigneeUserId { get; set; } // Может быть пустым
}