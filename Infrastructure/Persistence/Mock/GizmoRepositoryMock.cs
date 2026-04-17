using System;
using System.Linq;
using GizmoGateway.Domain.Entities;
using GizmoGateway.Domain.Interfaces;
using GizmoGateway.Domain.Common;

namespace GizmoGateway.Infrastructure.Persistence.Mock;

public class GizmoRepositoryMock : IGizmoRepository
{
    private readonly List<Gizmo> _store;

    public GizmoRepositoryMock()
    {
        var m1 = new Manufacturer(new Guid("c9cc0530-36e8-417f-b468-1d4867475799"), "Acme Corp", "Acme manufacturer");
        var m2 = new Manufacturer(new Guid("97918d7c-e02f-4de6-8c49-b026b98dcd67"), "GizmoWorks", "Quality gizmos");
        var m3 = new Manufacturer(new Guid("779bd724-3350-458a-983f-e0f79d136bde"), "WidgetCo", "Widgets and gizmos");

        _store = new List<Gizmo>
        {
            new Gizmo(new Guid("fce2d040-ccc9-4327-bbb4-ad5648bc7a1f"), "Mock Gizmo 1", "Wearables", m1, "Sample gizmo", 9.99m),
            new Gizmo(new Guid("adb0d131-4037-4751-b658-2c2ed92469ca"), "Mock Gizmo 2", "Home Tech", m2, "Another sample", 19.99m),
            new Gizmo(new Guid("7c0f6cd2-dc8f-4a33-b56f-ca658aa32e0f"), "Mock Gizmo 3", "Prototyping", m3, "Extra sample", 29.99m),
            new Gizmo(new Guid("6f972263-978f-4dec-bc25-d13b79cbeff0"), "Mock Gizmo 4", "Wearables", m1, "More sample data", 39.99m),
            new Gizmo(new Guid("c7e0789a-b645-4f21-ad3f-c933ef2292ed"), "Mock Gizmo 5", "Home Tech", m2, "Additional sample", 49.99m),
            new Gizmo(new Guid("abe3ce93-c4e7-4146-bd6f-7e12a2e4ee5f"), "Mock Gizmo 6", "Prototyping", m3, "Sample entry six", 59.99m),
            new Gizmo(new Guid("cf9e6d33-3252-48be-a53f-0fc305dd0895"), "Mock Gizmo 7", "Wearables", m1, "Sample entry seven", 69.99m),
            new Gizmo(new Guid("7f7671a8-cb7a-4cba-98a1-5b793748ff37"), "Mock Gizmo 8", "Home Tech", m2, "Sample entry eight", 79.99m),
            new Gizmo(new Guid("34ee08c6-b016-4161-8fc6-8f8e70580784"), "Mock Gizmo 9", "Prototyping", m3, "Sample entry nine", 89.99m),
            new Gizmo(new Guid("98cd860d-2f5c-4950-a9b2-90518ce08e68"), "Mock Gizmo 10", "Wearables", m1, "Sample entry ten", 99.99m),
            new Gizmo(new Guid("a165ad16-239e-4e15-bf75-99377510411a"), "Mock Gizmo 11", "Home Tech", m2, "Sample entry eleven", 109.99m),
            new Gizmo(new Guid("d732dc56-3651-4b06-9bb1-06761069e8ba"), "Mock Gizmo 12", "Prototyping", m3, "Sample entry twelve", 119.99m),
            new Gizmo(new Guid("3aa34b3f-12d0-4795-9b46-244eabe0ab4f"), "Mock Gizmo 13", "Wearables", m1, "Sample entry thirteen", 129.99m),
            new Gizmo(new Guid("6838e241-cf0d-4639-86c5-fc7b9e8d4faa"), "Mock Gizmo 14", "Home Tech", m2, "Sample entry fourteen", 139.99m),
            new Gizmo(new Guid("971c4a67-24b0-4206-9d22-08f9259e9862"), "Mock Gizmo 15", "Prototyping", m3, "Sample entry fifteen", 149.99m)
        };
    }

    public Task<PagedResponse<Gizmo>> GetAllAsync(int page, int pageSize)
    {
        var total = _store.Count;
        var items = _store.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var response = new PagedResponse<Gizmo>(items, page, pageSize, total);
        return Task.FromResult(response);
    }

    public Task<PagedResponse<Gizmo>> GetByCategoryAsync(string category, int page, int pageSize)
    {
        var filtered = _store.Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase)).ToList();
        var total = filtered.Count;
        var items = filtered.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var response = new PagedResponse<Gizmo>(items, page, pageSize, total);
        return Task.FromResult(response);
    }

    public Task<Gizmo?> GetByIdAsync(Guid id) => Task.FromResult(_store.FirstOrDefault(x => x.Id == id));
}
