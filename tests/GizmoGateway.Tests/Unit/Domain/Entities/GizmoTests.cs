using System;
using FluentAssertions;
using GizmoGateway.Domain.Entities;
using Moq;
using Xunit;

namespace GizmoGateway.Tests.Unit.Domain.Entities;

public class GizmoTests
{
    [Fact]
    public void Gizmo_CanBeInstantiated_And_HasValueEquality_IncludingManufacturer()
    {
        var id = Guid.NewGuid();
        var m = new Mock<Manufacturer>(Guid.NewGuid(), "GizmoWorks", "Quality gizmos"){ CallBase = true }.Object;

        var g1 = new Gizmo(id, "Gizmo 1", "Wearables", m, "Desc", 9.99m);
        var g2 = new Gizmo(id, "Gizmo 1", "Wearables", m, "Desc", 9.99m);
        var gDifferent = new Gizmo(id, "Gizmo 1", "Wearables", m, "Desc", 19.99m);

        g1.Should().BeEquivalentTo(g2);
        g1.Should().Be(g2);
        g1.Should().NotBe(gDifferent);

        g1.Id.Should().Be(id);
        g1.Name.Should().Be("Gizmo 1");
        g1.Category.Should().Be("Wearables");
        g1.Manufacturer.Should().Be(m);
        g1.Description.Should().Be("Desc");
        g1.MSRP.Should().Be(9.99m);
    }

    [Fact]
    public void Records_AreValueBased_WhenManufacturerDiffers_ThenGizmoNotEqual()
    {
        var id = Guid.NewGuid();
        var m1 = new Mock<Manufacturer>(Guid.NewGuid(), "A", null){ CallBase = true }.Object;
        var m2 = new Mock<Manufacturer>(Guid.NewGuid(), "B", null){ CallBase = true }.Object;

        var g1 = new Gizmo(id, "G", "C", m1, null, 1m);
        var g2 = new Gizmo(id, "G", "C", m2, null, 1m);

        g1.Should().NotBe(g2);
    }
}
