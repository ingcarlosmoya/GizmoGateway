using GizmoGateway.Presentation.Endpoints;
using GizmoGateway.Domain.Interfaces;
using GizmoGateway.Infrastructure.Persistence.Mock;
using GizmoGateway.Application.Services;
using GizmoGateway.Presentation.Middlewares;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<GlobalExceptionHandler>();

// Dependency registry - register infrastructure implementations and application services
builder.Services.AddScoped<IGizmoRepository, GizmoRepositoryMock>();
builder.Services.AddScoped<GizmoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var handler = context.RequestServices.GetRequiredService<GlobalExceptionHandler>();
        await handler.HandleAsync(context);
    });
});

app.MapGizmos();
app.Run();
