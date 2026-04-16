using GizmoGateway.Presentation.Endpoints;
using GizmoGateway.Domain.Interfaces;
using GizmoGateway.Infrastructure.Persistence.Mock;
using GizmoGateway.Application.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency registry - register infrastructure implementations and application services
builder.Services.AddScoped<IGizmoRepository, GizmoRepositoryMock>();
builder.Services.AddScoped<GizmoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGizmos();
app.Run();
