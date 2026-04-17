using GizmoGateway.Application.Services;
using GizmoGateway.Domain.Entities;

namespace GizmoGateway.Presentation.Endpoints;

public static class GizmosEndpoints
{
    public static void MapGizmos(this WebApplication app)
    {
        app.MapGet("/", () => "The Gizmo Gateway API is running. Go to /swagger for documentation.");

        app.MapGet("/api/gizmos", async (GizmoService svc, string? category, int page = 1, int pageSize = 10) =>
        {
            if (page < 1) return Results.BadRequest("Page must be at least 1.");
            if (pageSize < 1 || pageSize > 50) return Results.BadRequest("PageSize must be between 1 and 50.");

            if (!string.IsNullOrWhiteSpace(category))
            {
                var result = await svc.GetByCategoryAsync(category, page, pageSize);
                return Results.Ok(result);
            }

            var all = await svc.GetAllAsync(page, pageSize);
            return Results.Ok(all);
        });

        app.MapGet("/api/gizmos/{id:guid}", async (GizmoService svc, Guid id) =>
        {
            var g = await svc.GetByIdAsync(id);
            return g is null ? Results.NotFound() : Results.Ok(g);
        });
    }
}
