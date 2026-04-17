using GizmoGateway.Application.Services;
using GizmoGateway.Domain.Entities;
using System.Text.Json;

namespace GizmoGateway.Presentation.Endpoints;

public static class GizmosEndpoints
{
    public static void MapGizmos(this WebApplication app)
    {
        app.MapGet("/", () => "The Gizmo Gateway API is running. Go to /swagger for documentation.");

        app.MapGet("/api/gizmos", async (GizmoService svc, string? category, int page = 1, int pageSize = 10) =>
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            if (page < 1)
            {
                var err = JsonSerializer.Serialize(new { error = "Page must be at least 1." }, jsonOptions);
                return Results.Content(err, "application/json", statusCode: 400);
            }

            if (pageSize < 1 || pageSize > 50)
            {
                var err = JsonSerializer.Serialize(new { error = "PageSize must be between 1 and 50." }, jsonOptions);
                return Results.Content(err, "application/json", statusCode: 400);
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                var result = await svc.GetByCategoryAsync(category, page, pageSize);
                var json = JsonSerializer.Serialize(result, jsonOptions);
                return Results.Content(json, "application/json");
            }

            var all = await svc.GetAllAsync(page, pageSize);
            var allJson = JsonSerializer.Serialize(all, jsonOptions);
            return Results.Content(allJson, "application/json");
        });

        app.MapGet("/api/gizmos/{id:guid}", async (GizmoService svc, Guid id) =>
        {
            var g = await svc.GetByIdAsync(id);
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return g is null ? Results.NotFound() : Results.Content(JsonSerializer.Serialize(g, jsonOptions), "application/json");
        });
    }
}
