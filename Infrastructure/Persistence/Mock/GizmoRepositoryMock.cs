using GizmoGateway.Domain.Entities;
using GizmoGateway.Domain.Interfaces;

namespace GizmoGateway.Infrastructure.Persistence.Mock;

public class GizmoRepositoryMock : IGizmoRepository
{
    private readonly List<Gizmo> _store;

    public GizmoRepositoryMock()
    {
        var m1 = new Manufacturer { Id = new Guid("c9cc0530-36e8-417f-b468-1d4867475799"), Name = "Acme Corp", Description = "Acme manufacturer" };
        var m2 = new Manufacturer { Id = new Guid("97918d7c-e02f-4de6-8c49-b026b98dcd67"), Name = "GizmoWorks", Description = "Quality gizmos" };
        var m3 = new Manufacturer { Id = new Guid("779bd724-3350-458a-983f-e0f79d136bde"), Name = "WidgetCo", Description = "Widgets and gizmos" };

        _store = new List<Gizmo>
        {
            new Gizmo { Id = new Guid("fce2d040-ccc9-4327-bbb4-ad5648bc7a1f"), Name = "Mock Gizmo 1", Manufacturer = m1, Description = "Sample gizmo", MSRP = 9.99m },
            new Gizmo { Id = new Guid("adb0d131-4037-4751-b658-2c2ed92469ca"), Name = "Mock Gizmo 2", Manufacturer = m2, Description = "Another sample", MSRP = 19.99m },
            new Gizmo { Id = new Guid("7c0f6cd2-dc8f-4a33-b56f-ca658aa32e0f"), Name = "Mock Gizmo 3", Manufacturer = m3, Description = "Extra sample", MSRP = 29.99m },
            new Gizmo { Id = new Guid("6f972263-978f-4dec-bc25-d13b79cbeff0"), Name = "Mock Gizmo 4", Manufacturer = m1, Description = "More sample data", MSRP = 39.99m },
            new Gizmo { Id = new Guid("c7e0789a-b645-4f21-ad3f-c933ef2292ed"), Name = "Mock Gizmo 5", Manufacturer = m2, Description = "Additional sample", MSRP = 49.99m },
            new Gizmo { Id = new Guid("abe3ce93-c4e7-4146-bd6f-7e12a2e4ee5f"), Name = "Mock Gizmo 6", Manufacturer = m3, Description = "Sample entry six", MSRP = 59.99m },
            new Gizmo { Id = new Guid("cf9e6d33-3252-48be-a53f-0fc305dd0895"), Name = "Mock Gizmo 7", Manufacturer = m1, Description = "Sample entry seven", MSRP = 69.99m },
            new Gizmo { Id = new Guid("7f7671a8-cb7a-4cba-98a1-5b793748ff37"), Name = "Mock Gizmo 8", Manufacturer = m2, Description = "Sample entry eight", MSRP = 79.99m },
            new Gizmo { Id = new Guid("34ee08c6-b016-4161-8fc6-8f8e70580784"), Name = "Mock Gizmo 9", Manufacturer = m3, Description = "Sample entry nine", MSRP = 89.99m },
            new Gizmo { Id = new Guid("98cd860d-2f5c-4950-a9b2-90518ce08e68"), Name = "Mock Gizmo 10", Manufacturer = m1, Description = "Sample entry ten", MSRP = 99.99m },
            new Gizmo { Id = new Guid("a165ad16-239e-4e15-bf75-99377510411a"), Name = "Mock Gizmo 11", Manufacturer = m2, Description = "Sample entry eleven", MSRP = 109.99m },
            new Gizmo { Id = new Guid("d732dc56-3651-4b06-9bb1-06761069e8ba"), Name = "Mock Gizmo 12", Manufacturer = m3, Description = "Sample entry twelve", MSRP = 119.99m },
            new Gizmo { Id = new Guid("3aa34b3f-12d0-4795-9b46-244eabe0ab4f"), Name = "Mock Gizmo 13", Manufacturer = m1, Description = "Sample entry thirteen", MSRP = 129.99m },
            new Gizmo { Id = new Guid("6838e241-cf0d-4639-86c5-fc7b9e8d4faa"), Name = "Mock Gizmo 14", Manufacturer = m2, Description = "Sample entry fourteen", MSRP = 139.99m },
            new Gizmo { Id = new Guid("971c4a67-24b0-4206-9d22-08f9259e9862"), Name = "Mock Gizmo 15", Manufacturer = m3, Description = "Sample entry fifteen", MSRP = 149.99m }
        };
    }

    public Task<Gizmo> AddAsync(Gizmo gizmo)
    {
        gizmo.Id = Guid.NewGuid();
        _store.Add(gizmo);
        return Task.FromResult(gizmo);
    }

    public Task<IEnumerable<Gizmo>> GetAllAsync() => Task.FromResult<IEnumerable<Gizmo>>(_store);

    public Task<Gizmo?> GetByIdAsync(Guid id) => Task.FromResult(_store.FirstOrDefault(x => x.Id == id));
}
