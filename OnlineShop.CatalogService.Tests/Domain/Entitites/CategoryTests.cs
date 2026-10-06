using FluentAssertions;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Tests.Domain.Entitites;
public class CategoryTests
{
    // Constructor: Name
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Constructor_ShouldThrow_WhenNameIsEmpty(string? name)
    {
        var act = () => new Category(name!, "valid-slug");
        act.Should().Throw<ArgumentException>()
            .WithParameterName("name");
    }

    // Constructor: Slug
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Constructor_ShouldThrow_WhenSlugIsEmpty(string? slug)
    {
        var act = () => new Category("Valid Name", slug!);
        act.Should().Throw<ArgumentException>()
            .WithParameterName("slug");
    }

    // Constructor: successfull case
    [Fact]
    public void Constructor_ShouldSetProperties_WhenInputIsValid()
    {
        var category = new Category("Смартфоны", "smartphones");

        category.Id.Should().NotBeEmpty();
        category.Name.Should().Be("Смартфоны");
        category.Slug.Should().Be("smartphones");
        category.ParentId.Should().BeNull();
        category.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Fact]
    public void Constructor_ShouldSetParentId_WhenProvided()
    {
        var parentId = Guid.NewGuid();

        var category = new Category("Смартфоны", "smartphones", parentId);

        category.ParentId.Should().Be(parentId);
    }

    [Fact]
    public void Constructor_ShouldGenerateUniqueIds()
    {
        var category1 = new Category("Name 1", "slug-1");
        var category2 = new Category("Name 2", "slug-2");

        category1.Id.Should().NotBe(category2.Id);
    }

    // Rename
    [Fact]
    public void Rename_ShouldChangeName()
    {
        var category = new Category("Смартфоны", "smartphones");

        category.Rename("Мобильные телефоны");

        category.Name.Should().Be("Мобильные телефоны");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Rename_ShouldThrow_WhenNewNameIsEmpty(string? newName)
    {
        var category = new Category("Смартфоны", "smartphohes");

        var act = () => category.Rename(newName!);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Rename_ShouldNotChangeId()
    {
        var category = new Category("Смартфоны", "smartphohes");
        var originalId = category.Id;

        category.Rename("Мобильные телефоны");

        category.Id.Should().Be(originalId);
    }
}