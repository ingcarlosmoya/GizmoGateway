using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GizmoGateway.Infrastructure.Persistence.Mock;
using GizmoGateway.Application.Services;
using Xunit;

namespace GizmoGateway.Tests.Unit.Infrastructure.Persistence.Mock;

public class GizmoRepositoryMockTests
{
    private readonly GizmoRepositoryMock _repo = new();

    [Fact]
    public async Task GetByCategoryAsync_ReturnsOnlyRequestedCategory_CaseInsensitive()
    {
        var response = await _repo.GetByCategoryAsync("wearables", 1, 100);

        response.Should().NotBeNull();
        response.Items.Should().NotBeNull();
        response.Items.Should().OnlyContain(g => string.Equals(g.Category, "wearables", StringComparison.OrdinalIgnoreCase));
        response.TotalCount.Should().Be(response.Items.Count());
    }

    [Fact]
    public async Task GetAllAsync_Pagination_Works()
    {
        var response = await _repo.GetAllAsync(1, 2);

        response.Should().NotBeNull();
        response.Items.Should().HaveCount(2);
        response.PageNumber.Should().Be(1);
        response.PageSize.Should().Be(2);
    }

    [Fact]
    public async Task GetByCategoryAsync_NonExistingCategory_ReturnsEmptyPagedResponse()
    {
        var response = await _repo.GetByCategoryAsync("NonExistingCategory", 1, 10);

        response.Should().NotBeNull();
        response.Items.Should().BeEmpty();
        response.TotalCount.Should().Be(0);
        response.PageNumber.Should().Be(1);
        response.PageSize.Should().Be(10);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsGizmo_ForValidId()
    {
        var svc = new GizmoService(_repo);

        var knownId = new Guid("fce2d040-ccc9-4327-bbb4-ad5648bc7a1f");
        var gizmo = await svc.GetByIdAsync(knownId);

        gizmo.Should().NotBeNull();
        gizmo!.Id.Should().Be(knownId);
        gizmo.Name.Should().StartWith("Mock Gizmo");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_ForUnknownId()
    {
        var svc = new GizmoService(_repo);

        var randomId = Guid.NewGuid();
        var gizmo = await svc.GetByIdAsync(randomId);

        gizmo.Should().BeNull();
    }
}
