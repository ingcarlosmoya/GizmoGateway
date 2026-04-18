using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GizmoGateway.Tests.Integration.Presentation.Endpoints;

public class GizmosEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GizmosEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetById_Returns200_ForValidId()
    {
        using var client = _factory.CreateClient();

        // Known ID from GizmoRepositoryMock
        var knownId = new Guid("fce2d040-ccc9-4327-bbb4-ad5648bc7a1f");
        var respOk = await client.GetAsync($"/api/gizmos/{knownId}");
        respOk.StatusCode.Should().Be(HttpStatusCode.OK);

        var json = await respOk.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        var name = root.GetProperty("name").GetString();
        name.Should().Be("Mock Gizmo 1");
    }

    [Fact]
    public async Task GetById_Returns404_ForRandomGuid()
    {
        using var client = _factory.CreateClient();

        // Random GUID should return 404
        var randomId = Guid.NewGuid();
        var respNotFound = await client.GetAsync($"/api/gizmos/{randomId}");
        respNotFound.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetGizmos_InvalidCategory_ReturnsEmptyItems()
    {
        using var client = _factory.CreateClient();

        var invalidCategory = "NonExistentCategory";
        var page = 1;
        var pageSize = 2;

        var resp = await client.GetAsync($"/api/gizmos?category={Uri.EscapeDataString(invalidCategory)}&page={page}&pageSize={pageSize}");
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // Verify paged response structure
        root.GetProperty("pageNumber").GetInt32().Should().Be(page);
        root.GetProperty("pageSize").GetInt32().Should().Be(pageSize);

        var items = root.GetProperty("items");
        items.GetArrayLength().Should().Be(0);
    }

    [Fact]
    public async Task GetGizmos_InvalidPage_ReturnsBadRequest()
    {
        using var client = _factory.CreateClient();

        var category = "Wearables";
        var invalidPage = -1;
        var invalidPageSize = -1;

        var resp = await client.GetAsync($"/api/gizmos?category={Uri.EscapeDataString(category)}&page={invalidPage}&pageSize={invalidPageSize}");
       
        var json = await resp.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // Verify paged response structure
        root.GetProperty("error").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetGizmos_InvalidPageSize_ReturnsBadRequest()
    {
        using var client = _factory.CreateClient();

        var category = "Wearables";
        var page = 1;
        var invalidPageSize = -1;

        var resp = await client.GetAsync($"/api/gizmos?category={Uri.EscapeDataString(category)}&page={page}&pageSize={invalidPageSize}");
       
        var json = await resp.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // Verify error message
        root.GetProperty("error").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetGizmos_QueryParameters_AreHandled()
    {
        using var client = _factory.CreateClient();

        var category = "Wearables";
        var page = 1;
        var pageSize = 2;

        var resp = await client.GetAsync($"/api/gizmos?category={Uri.EscapeDataString(category)}&page={page}&pageSize={pageSize}");
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // Verify paged response structure
        root.GetProperty("pageNumber").GetInt32().Should().Be(page);
        root.GetProperty("pageSize").GetInt32().Should().Be(pageSize);

        var items = root.GetProperty("items");
        items.GetArrayLength().Should().BeLessThanOrEqualTo(pageSize);

        // Verify all returned items belong to requested category (case-insensitive)
        foreach (var item in items.EnumerateArray())
        {
            var itemCategory = item.GetProperty("category").GetString();
            itemCategory.Should().NotBeNull();
            itemCategory!.Equals(category, StringComparison.OrdinalIgnoreCase).Should().BeTrue();
        }
    }
}