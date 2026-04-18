using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GizmoGateway.Infrastructure.Persistence.Repositories;
using GizmoGateway.Application.Services;
using GizmoGateway.Domain.Interfaces;
using GizmoGateway.Domain.Entities;
using GizmoGateway.Domain.Common;
using Moq;
using Xunit;

namespace GizmoGateway.Tests.Unit.Application.Services;

public class GizmoServiceTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnPagedResponse_WhenDataExists()
    {
        // Arrange
        var mockRepo = new Mock<IGizmoRepository>();
        var m1 = new Mock<Manufacturer>(Guid.NewGuid(), "M", null);
        var m2 = new Mock<Manufacturer>(Guid.NewGuid(), "M", null);

        var g1 = new Mock<Gizmo>(Guid.NewGuid(), "G1", "Cat", m1.Object, null, 1m);
        var g2 = new Mock<Gizmo>(Guid.NewGuid(), "G2", "Cat", m2.Object, null, 2m);

        var items = new[] { g1.Object, g2.Object };
        var paged = new Mock<PagedResponse<Gizmo>>(items, 1, 2, items.Length);
        mockRepo.Setup(r => r.GetAllAsync(1, 2)).ReturnsAsync(paged.Object);

        var svc = new GizmoService(mockRepo.Object);

        // Act
        var result = await svc.GetAllAsync(1, 2);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(2);
        result.TotalCount.Should().Be(items.Length);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyPagedResponse_WhenNoDataExists()
    {
        // Arrange
        var mockRepo = new Mock<IGizmoRepository>();
        var paged = new Mock<PagedResponse<Gizmo>>(Array.Empty<Gizmo>(), 1, 10, 0);
        mockRepo.Setup(r => r.GetAllAsync(1, 10)).ReturnsAsync(paged.Object);

        var svc = new GizmoService(mockRepo.Object);

        // Act
        var result = await svc.GetAllAsync(1, 10);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.TotalCount.Should().Be(0);
    }

    [Fact]
    public async Task GetByCategoryAsync_ShouldHandleCaseInsensitivity()
    {
        // Arrange
        var mockRepo = new Mock<IGizmoRepository>();
        var m = new Mock<Manufacturer>(Guid.NewGuid(), "M", null);
        var g1 = new Mock<Gizmo>(Guid.NewGuid(), "G1", "Wearables", m.Object, null, 1m);
        var g2 = new Mock<Gizmo>(Guid.NewGuid(), "G2", "Wearables", m.Object, null, 2m);
        var items = new[] { g1.Object, g2.Object };
        var paged = new Mock<PagedResponse<Gizmo>>(items, 1, 10, items.Length);
        mockRepo.Setup(r => r.GetByCategoryAsync(It.Is<string>(s => string.Equals(s, "wearables", StringComparison.OrdinalIgnoreCase)), 1, 10)).ReturnsAsync(paged.Object);

        var svc = new GizmoService(mockRepo.Object);

        // Act
        var result = await svc.GetByCategoryAsync("WEARables", 1, 10);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().OnlyContain(g => string.Equals(g.Category, "wearables", StringComparison.OrdinalIgnoreCase));
        result.TotalCount.Should().Be(items.Length);
    }

    [Fact]
    public async Task GetByCategoryAsync_ShouldReturnEmptyPagedResponse_WhenNoItemsMatchCategory()
    {
        // Arrange
        var mockRepo = new Mock<IGizmoRepository>();
        var paged = new Mock<PagedResponse<Gizmo>>(Array.Empty<Gizmo>(), 1, 10, 0);
        mockRepo.Setup(r => r.GetByCategoryAsync(It.IsAny<string>(), 1, 10)).ReturnsAsync(paged.Object);

        var svc = new GizmoService(mockRepo.Object);

        // Act
        var result = await svc.GetByCategoryAsync("NonExistingCategory", 1, 10);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnGizmo_WhenIdExists()
    {
        // Arrange
        var mockRepo = new Mock<IGizmoRepository>();
        var id = Guid.NewGuid();
        var mfr = new Mock<Manufacturer>(Guid.NewGuid(), "M", null);
        var gizmo = new Mock<Gizmo>(id, "G1", "Cat", mfr.Object, null, 1m);
        mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(gizmo.Object);

        var svc = new GizmoService(mockRepo.Object);

        // Act
        var result = await svc.GetByIdAsync(id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(id);
        result.Name.Should().Be("G1");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IGizmoRepository>();
        var id = Guid.NewGuid();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Gizmo?)null);

        var svc = new GizmoService(mockRepo.Object);

        // Act
        var result = await svc.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }
}