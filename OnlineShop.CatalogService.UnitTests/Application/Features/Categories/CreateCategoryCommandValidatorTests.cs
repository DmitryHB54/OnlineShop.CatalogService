using FluentValidation.TestHelper;
using NSubstitute;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Commands.CreateCategory;
using OnlineShop.CatalogService.Application.Common.Interfaces;

namespace OnlineShop.CatalogService.Tests.Application.Features.Categories;
public class CreateCategoryCommandValidatorTests
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CreateCategoryCommandValidator _validator;
    public CreateCategoryCommandValidatorTests()
    {
        _categoryRepository = Substitute.For<ICategoryRepository>();

        _categoryRepository
            .ExistsWithSlugAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _validator = new CreateCategoryCommandValidator(_categoryRepository);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_HaveError_WhenNameIsEmpty(string? name)
    {
        var command = new CreateCategoryCommand(name!, "valid-slug");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Should_HaveError_WhenNameExceedsMaxLength()
    {
        var name = new string('a', 201);
        var command = new CreateCategoryCommand(name, "valid-slug");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task Should_NotHaveError_WhenIsValid()
    {
        var command = new CreateCategoryCommand("Смартфоны", "valid-slug");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    //--- Slug format

    [Theory]
    [InlineData("INVALID")]
    [InlineData("Invalid Slug")]
    [InlineData("invalid_slug")]
    [InlineData("СМАРТФОНЫ")]
    [InlineData("with.dots")]
    [InlineData("with/slash")]
    public async Task Should_HaveError_WhenSlugFormatIsInvalid(string slug)
    {
        var command = new CreateCategoryCommand("Valid Name", slug);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Slug);
    }

    [Theory]
    [InlineData("smartphones")]
    [InlineData("home-appliances")]
    [InlineData("tv-4k")]
    [InlineData("123")]
    [InlineData("a")]
    public async Task Should_NotHaveError_WhenSlugFormatIsValid(string slug)
    {
        var command = new CreateCategoryCommand("Valid Name", slug);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Slug);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Should_HaveError_WhenSlugIsEmpty(string slug)
    {
        var command = new CreateCategoryCommand("Valid Name", slug!);

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Slug);
    }

    //--- Slug uniqueness

    [Fact]
    public async Task Should_HaveError_WhenSlugAlreadyExsists()
    {
        _categoryRepository
            .ExistsWithSlugAsync("smartphones", Arg.Any<CancellationToken>())
            .Returns(true);

        var command = new CreateCategoryCommand("Смартфоны", "smartphones");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Slug)
            .WithErrorMessage("Category with this slug already exists");
    }

    [Fact]
    public async Task Should_NotHaveError_WhenSlugIsUnique()
    {
        _categoryRepository
            .ExistsWithSlugAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);

        var command = new CreateCategoryCommand("Смартфоны", "smartphones");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Slug);
    }

    //--- Complex test

    [Fact]
    public async Task Should_NotHaveErrors_WhenCommandIsValid()
    {
        var command = new CreateCategoryCommand("Смартфоны", "smartphones");

        var result = await _validator.TestValidateAsync(command);
        
        result.ShouldNotHaveAnyValidationErrors();
    }
}