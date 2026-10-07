using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Dtos;
using OnlineShop.CatalogService.IntegrationTests.Infrastructure;
using System.Net;
using System.Net.Http.Json;

namespace OnlineShop.CatalogService.IntegrationTests;
public class CategoriesControllerIntegrationTests : BaseIntegrationTest
{
    public CategoriesControllerIntegrationTests(IntegrationTestWebApplicationFactory factory)
        :base(factory)
    {
    }

    [Fact]
    public async Task Create_ShouldPersistCategory_ToRealDatabase()
    {
        var command = new { Name = "Смартфоны", Slug = "smartphones", ParentId = (Guid?)null };

        var response = await Client.PostAsJsonAsync("/api/categories", command);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Create_ShouldReturn400_WhenSlugAlreadyExists()
    {
        var command1 = new {  Name = "Смартфоны", Slug = "smartphones", ParentId = (Guid?)null };
        await Client.PostAsJsonAsync("/api/categories", command1);

        var command2 = new{ Name = "Другие смартфоны", Slug = "smartphones", ParentId = (Guid?)null};

        var response = await Client.PostAsJsonAsync("/api/categories", command2);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problem!.Errors.Should().ContainKey("Slug");
    }

    [Fact]
    public async Task GetAll_ShouldReturnCategories_OrdereByName()
    {
        await Client.PostAsJsonAsync("/api/categories", new { Name = "Z-категория", Slug = "z-cat", ParentId = (Guid?)null });
        await Client.PostAsJsonAsync("/api/categories", new { Name = "A-категория", Slug = "a-cat", ParentId = (Guid?)null });

        var response = await Client.GetAsync("/api/categories");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var categories = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
        categories.Should().HaveCount(2);
        categories![0].Name.Should().Be("A-категория");
        categories[1].Name.Should().Be("Z-категория");
    }
}