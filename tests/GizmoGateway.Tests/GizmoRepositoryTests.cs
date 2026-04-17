using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using GizmoGateway.Domain.Entities;
using GizmoGateway.Infrastructure.Persistence.Mock;
using Xunit;

namespace GizmoGateway.Tests;

public class GizmoRepositoryTests
{
    [Fact]
    public async Task GizmoRepositoryMock_HandlesEmptyInternalList()
    {
        var repo = new GizmoRepositoryMock();

        // Clear internal private store via reflection
        var field = typeof(GizmoRepositoryMock).GetField("_store", BindingFlags.Instance | BindingFlags.NonPublic);
        field.Should().NotBeNull();
        field!.SetValue(repo, new List<Gizmo>());

        var all = await repo.GetAllAsync(1, 10);
        all.Should().NotBeNull();
        all.Items.Should().BeEmpty();
        all.TotalCount.Should().Be(0);

        var byCategory = await repo.GetByCategoryAsync("Wearables", 1, 10);
        byCategory.Should().NotBeNull();
        byCategory.Items.Should().BeEmpty();
        byCategory.TotalCount.Should().Be(0);
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetAll_DoesNotThrow_ForPageSizeZero()
    {
        var repo = new GizmoRepositoryMock();
        Func<Task> call = async () => await repo.GetAllAsync(1, 0);
        await call.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetByCategory_DoesNotThrow_ForPageSizeZero()
    {
        var repo = new GizmoRepositoryMock();
        Func<Task> call = async () => await repo.GetByCategoryAsync("Wearables", 1, 0);
        await call.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetAll_DoesNotThrow_ForNegativePageSize()
    {
        var repo = new GizmoRepositoryMock();
        Func<Task> call = async () => await repo.GetAllAsync(1, -1);
        await call.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetByCategory_DoesNotThrow_ForNegativePageSize()
    {
        var repo = new GizmoRepositoryMock();
        Func<Task> call = async () => await repo.GetByCategoryAsync("Wearables", 1, -1);
        await call.Should().NotThrowAsync<Exception>();
    }
}
