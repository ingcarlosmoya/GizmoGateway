using GizmoGateway.Domain.Entities;
namespace GizmoGateway.Domain.Interfaces;

public interface IGizmoRepository
{
    Task<Gizmo?> GetByIdAsync(Guid id);
    Task<IEnumerable<Gizmo>> GetAllAsync();
    Task<Gizmo> AddAsync(Gizmo gizmo);
}
