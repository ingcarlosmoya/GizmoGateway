using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GizmoGateway.Tests.Integration.Presentation.Endpoints;

public class PaginationEdgeCaseTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PaginationEdgeCaseTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ZeroOrNegativePage_EitherBadRequestOrDefaultsToPage1(int page)
    {
        using var client = _factory.CreateClient();

        var resp = await client.GetAsync($"/api/gizmos?page={page}");

        if (resp.StatusCode == HttpStatusCode.BadRequest)
        {
            // acceptable: API rejects non-positive page
            resp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        else
        {
            // acceptable: API treats it as page 1
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var pageNumber = doc.RootElement.GetProperty("pageNumber").GetInt32();
            pageNumber.Should().Be(1);
        }
    }

    [Fact]
    public async Task ExcessivePageSize_IsCappedAt50OrRejected()
    {
        using var client = _factory.CreateClient();

        var resp = await client.GetAsync($"/api/gizmos?page=1&pageSize=1000");

        if (resp.StatusCode == HttpStatusCode.BadRequest)
        {
            // Current behavior returns 400 for too large pageSize
            resp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        else
        {
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var items = doc.RootElement.GetProperty("items");
            items.GetArrayLength().Should().BeLessThanOrEqualTo(50);
        }
    }

    [Fact]
    public async Task PageBeyondResults_Returns200_WithEmptyList()
    {
        using var client = _factory.CreateClient();

        var resp = await client.GetAsync($"/api/gizmos?page=999&pageSize=10");
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var items = doc.RootElement.GetProperty("items");
        items.GetArrayLength().Should().Be(0);
    }

    [Fact]
    public async Task ZeroPageSize_HandledGracefully_EitherDefaultOrBadRequest()
    {
        using var client = _factory.CreateClient();

        var resp = await client.GetAsync($"/api/gizmos?page=1&pageSize=0");

        if (resp.StatusCode == HttpStatusCode.BadRequest)
        {
            resp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        else
        {
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var pageSize = doc.RootElement.GetProperty("pageSize").GetInt32();
            pageSize.Should().BeGreaterThanOrEqualTo(1);
        }
    }
}
