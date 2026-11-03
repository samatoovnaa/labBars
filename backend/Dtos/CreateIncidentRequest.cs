using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class CreateIncidentRequest
{
    [Required]
    [StringLength(80, MinimumLength = 5, ErrorMessage = "Тема должна быть от 5 до 80 символов")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Описание должно быть от 10 до 500 символов")]
    public string Description { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "CategoryId должен быть больше 0")]
    public int CategoryId { get; set; }
}