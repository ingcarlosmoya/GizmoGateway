namespace GizmoGateway.Domain.Entities;

public record Gizmo(
    Guid Id,
    string Name,
    string Category,
    Manufacturer Manufacturer,
    string? Description,
    decimal MSRP
);
