namespace GizmoGateway.Domain.Entities;

public class Gizmo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // Manufacturer entity reference
    public Manufacturer Manufacturer { get; set; } = new Manufacturer();

    // Optional description
    public string? Description { get; set; }

    // MSRP as a decimal
    public decimal MSRP { get; set; }
}
