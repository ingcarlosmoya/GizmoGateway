using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FluentAssertions;
using GizmoGateway.Domain.Entities;
using GizmoGateway.Infrastructure.Persistence.Repositories;
using Xunit;

namespace GizmoGateway.Tests.Integration.Infrastructure.Persistence;

public class GizmoRepositoryMockTests
{
    [Fact]
    public async Task GizmoRepositoryMock_GetAllAsync_DoesNotThrow_ForNegativePageSize()
    {
        var repo = new GizmoRepositoryMock();
        Func<Task> call = async () => await repo.GetAllAsync(1, -1);
        await call.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetAllAsync_DoesNotThrow_ForPageSizeZero()
    {
        var repo = new GizmoRepositoryMock();
        Func<Task> call = async () => await repo.GetAllAsync(1, 0);
        await call.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetAllAsync_HandlesEmptyInternalList()
    {
        var repo = new GizmoRepositoryMock();

        var field = typeof(GizmoRepositoryMock).GetField("_store", BindingFlags.Instance | BindingFlags.NonPublic);
        field.Should().NotBeNull();
        field!.SetValue(repo, new List<Gizmo>());

        var all = await repo.GetAllAsync(1, 10);
        all.Should().NotBeNull();
        all.Items.Should().BeEmpty();
        all.TotalCount.Should().Be(0);
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetAllAsync_ReturnsCorrectTotalCountAndItems()
    {
        var repo = new GizmoRepositoryMock();
        var all = await repo.GetAllAsync(1, 10);
        all.Should().NotBeNull();
        all.Items.Should().NotBeEmpty();
        all.TotalCount.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetByCategoryAsync_DoesNotThrow_ForNegativePageSize()
    {
        var repo = new GizmoRepositoryMock();
        Func<Task> call = async () => await repo.GetByCategoryAsync("Wearables", 1, -1);
        await call.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetByCategoryAsync_DoesNotThrow_ForPageSizeZero()
    {
        var repo = new GizmoRepositoryMock();
        Func<Task> call = async () => await repo.GetByCategoryAsync("Wearables", 1, 0);
        await call.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetByCategoryAsync_HandlesEmptyInternalList()
    {
        var repo = new GizmoRepositoryMock();

        var field = typeof(GizmoRepositoryMock).GetField("_store", BindingFlags.Instance | BindingFlags.NonPublic);
        field.Should().NotBeNull();
        field!.SetValue(repo, new List<Gizmo>());

        var byCategory = await repo.GetByCategoryAsync("Wearables", 1, 10);
        byCategory.Should().NotBeNull();
        byCategory.Items.Should().BeEmpty();
        byCategory.TotalCount.Should().Be(0);
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetByCategoryAsync_ReturnsCorrectTotalCountAndItems()
    {
        var repo = new GizmoRepositoryMock();
        var byCategory = await repo.GetByCategoryAsync("Wearables", 1, 10);
        byCategory.Should().NotBeNull();
        byCategory.Items.Should().NotBeEmpty();
        byCategory.TotalCount.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetByIdAsync_ReturnsCorrectGizmoForExistingId()
    {
        var repo = new GizmoRepositoryMock();
        var all = await repo.GetAllAsync(1, 10);
        var existingGizmo = all.Items.First();
        var result = await repo.GetByIdAsync(existingGizmo.Id);
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(existingGizmo);
    }

    [Fact]
    public async Task GizmoRepositoryMock_GetByIdAsync_ReturnsNullForNonExistentId()
    {
        var repo = new GizmoRepositoryMock();
        var result = await repo.GetByIdAsync(Guid.NewGuid());
        result.Should().BeNull();
    }

}