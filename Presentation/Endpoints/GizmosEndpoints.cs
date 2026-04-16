using GizmoGateway.Application.Services;
using GizmoGateway.Domain.Entities;

namespace GizmoGateway.Presentation.Endpoints;

public static class GizmosEndpoints
{
    public static void MapGizmos(this WebApplication app)
    {
        app.MapGet("/", () => "The Gizmo Gateway API is running. Go to /swagger for documentation.");

        app.MapGet("/gizmos", async (GizmoService svc) =>
            Results.Ok(await svc.GetAllAsync()));

        app.MapGet("/gizmos/{id:guid}", async (GizmoService svc, Guid id) =>
        {
            var g = await svc.GetByIdAsync(id);
            return g is null ? Results.NotFound() : Results.Ok(g);
        });

        app.MapPost("/gizmos", async (GizmoService svc, Gizmo gizmo) =>
        {
            var created = await svc.CreateAsync(gizmo);
            return Results.Created($"/gizmos/{created.Id}", created);
        });
    }
}
