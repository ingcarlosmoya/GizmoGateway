using GizmoGateway.Domain.Entities;
using GizmoGateway.Domain.Common;

namespace GizmoGateway.Domain.Interfaces;

public interface IGizmoRepository
{
    Task<Gizmo?> GetByIdAsync(Guid id);
    Task<PagedResponse<Gizmo>> GetAllAsync(int page, int pageSize);
    Task<PagedResponse<Gizmo>> GetByCategoryAsync(string category, int page, int pageSize);
}
