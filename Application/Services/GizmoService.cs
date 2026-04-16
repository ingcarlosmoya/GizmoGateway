using GizmoGateway.Domain.Entities;
using GizmoGateway.Domain.Interfaces;

namespace GizmoGateway.Application.Services;

public class GizmoService
{
    private readonly IGizmoRepository _repo;

    public GizmoService(IGizmoRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Gizmo>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Gizmo?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public Task<Gizmo> CreateAsync(Gizmo gizmo) => _repo.AddAsync(gizmo);
}
