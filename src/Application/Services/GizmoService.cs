using GizmoGateway.Domain.Entities;
using GizmoGateway.Domain.Interfaces;
using GizmoGateway.Domain.Common;

namespace GizmoGateway.Application.Services;

public class GizmoService
{
    private readonly IGizmoRepository _repo;

    public GizmoService(IGizmoRepository repo)
    {
        _repo = repo;
    }

    public Task<PagedResponse<Gizmo>> GetAllAsync(int page, int pageSize) => _repo.GetAllAsync(page, pageSize);
    public Task<PagedResponse<Gizmo>> GetByCategoryAsync(string category, int page, int pageSize) => _repo.GetByCategoryAsync(category, page, pageSize);
    public Task<Gizmo?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);
}
