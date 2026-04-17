using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using GizmoGateway.Infrastructure.Persistence.Mock;
using Xunit;

namespace GizmoGateway.Tests;

public class GizmoServiceTests
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
}
