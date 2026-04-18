using System;
using FluentAssertions;
using GizmoGateway.Domain.Entities;
using Xunit;

namespace GizmoGateway.Tests.Unit.Domain.Entities;

public class ManufacturerTests
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
    public void Manufacturers_WithDifferentIds_AreNotEqual()
    {
        var m1 = new Manufacturer(Guid.NewGuid(), "Acme", "Maker of stuff");
        var m2 = new Manufacturer(Guid.NewGuid(), "Acme", "Maker of stuff");

        m1.Should().NotBe(m2);
    }
}