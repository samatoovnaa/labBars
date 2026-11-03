namespace backend.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty; // Логин
    public string PasswordHash { get; set; } = string.Empty; // Хеш пароля!
    public string Role { get; set; } = "Engineer"; // Роль: Dispatcher или Engineer
}