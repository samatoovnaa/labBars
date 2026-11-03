using backend.Data;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем контроллеры
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 2. Настраиваем Swagger, чтобы в нем появилась кнопка "Authorize" (для токенов)
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Вставьте сюда ваш JWT токен (без слова Bearer)."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// 3. Подключение к базе данных PostgreSQL (EF Core)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// 4. Регистрация твоего сервиса
builder.Services.AddScoped<IIncidentService, IncidentService>();

// 5. Настройка JWT (ЛР11)
builder.Configuration["Jwt:Key"] = "super_secret_key_1234567890_super_secret!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 6. ОЧЕНЬ ВАЖНО: Подключаем аутентификацию и авторизацию (строго в таком порядке!)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// 7. Твои служебные эндпоинты из ЛР7
app.MapGet("/health", () => Results.Ok("Healthy"));
app.MapGet("/info", () => Results.Ok(new
{
    App = "Risk & Incident Management API",
    Environment = app.Environment.EnvironmentName,
    Lab = "ЛР7 — каркас Web API"
}));

app.Run();