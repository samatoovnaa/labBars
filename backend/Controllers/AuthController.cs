using backend.Data;
using backend.Dtos;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<Employee> _hasher = new();

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    // Временный метод, чтобы создать пользователей (потом можно удалить)
    [HttpPost("register")]
    public IActionResult Register(LoginRequest request)
    {
        var user = new Employee
        {
            Name = request.Username,
            Username = request.Username,
            Role = request.Username == "dispatcher" ? "Dispatcher" : "Engineer"
        };
        // Хешируем пароль!
        user.PasswordHash = _hasher.HashPassword(user, request.Password);
        
        _context.Employees.Add(user);
        _context.SaveChanges();
        return Ok("Пользователь создан");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = _context.Employees.FirstOrDefault(u => u.Username == request.Username);
        if (user == null) return Unauthorized("Неверный логин или пароль");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed) return Unauthorized("Неверный логин или пароль");

        // Генерируем JWT токен
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "super_secret_key_1234567890_super_secret!");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { Token = tokenHandler.WriteToken(token) });
    }
}