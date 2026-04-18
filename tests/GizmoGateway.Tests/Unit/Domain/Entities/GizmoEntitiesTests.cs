using System;
using FluentAssertions;
using GizmoGateway.Domain.Entities;
using Xunit;

namespace GizmoGateway.Tests.Unit.Domain.Entities;

public class GizmoEntitiesTests
{
    [Fact]
    public void Manufacturer_CanBeInstantiated_And_HasValueEquality()
    {
        var id = Guid.NewGuid();
        var m1 = new Manufacturer(id, "Acme", "Maker of stuff");
        var m2 = new Manufacturer(id, "Acme", "Maker of stuff");

        m1.Should().BeEquivalentTo(m2);
        m1.Should().Be(m2); // record value equality

        m1.Id.Should().Be(id);
        m1.Name.Should().Be("Acme");
        m1.Description.Should().Be("Maker of stuff");
    }

    [Fact]
    public void Gizmo_CanBeInstantiated_And_HasValueEquality_IncludingManufacturer()
    {
        var id = Guid.NewGuid();
        var m = new Manufacturer(Guid.NewGuid(), "GizmoWorks", "Quality gizmos");

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
        var m1 = new Manufacturer(Guid.NewGuid(), "A", null);
        var m2 = new Manufacturer(Guid.NewGuid(), "B", null);

        var g1 = new Gizmo(id, "G", "C", m1, null, 1m);
        var g2 = new Gizmo(id, "G", "C", m2, null, 1m);

        g1.Should().NotBe(g2);
    }
}
