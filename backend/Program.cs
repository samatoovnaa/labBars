using backend.Data;
using Microsoft.EntityFrameworkCore;
using backend.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IIncidentService, IncidentService>(); 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IIncidentService, IncidentService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Простейшие служебные эндпоинты (Minimal API)
app.MapGet("/health", () => Results.Ok("Healthy"));
app.MapGet("/info", () => Results.Ok(new
{
    App = "Risk & Incident Management API",
    Environment = app.Environment.EnvironmentName,
    Lab = "ЛР7 — каркас Web API"
}));

app.Run();
