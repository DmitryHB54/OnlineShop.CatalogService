using AutoFixture;
using FluentAssertions;
using NSubstitute;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Commands.CreateCategory;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;
using OnlineShop.CatalogService.Tests.Common;

namespace OnlineShop.CatalogService.Tests.Application.Features.Categories;
public class CreateCategoryCommandHandlerTests
{
private readonly IFixture _fixture;
private readonly ICategoryRepository _categoryRepository;
private readonly IUnitOfWork _unitOfWork;
private readonly CreateCategoryCommandHandler _handler;

public CreateCategoryCommandHandlerTests()
{
    _fixture = FixtureExtensions.CreateFixture();

    _categoryRepository = _fixture.Freeze<ICategoryRepository>();
    _unitOfWork = _fixture.Freeze<IUnitOfWork>();

    _handler = _fixture.Create<CreateCategoryCommandHandler>();
}

[Fact]
public async Task Handle_ShouldAddCategoryAndSaveChanges()
{
    // Arrange
    var command = new CreateCategoryCommand("Смартфоны", "smartphones", null);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    result.Should().NotBeEmpty();

    _categoryRepository.Received(1).Add(Arg.Is<Category>(c =>
        c.Name == "Смартфоны" &&
        c.Slug == "smartphones" &&
        c.ParentId == null));

    await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
}

[Fact]
public async Task Handle_ShouldPassCancellationToken_ToRepository()
{
    // Arrange
    var command = new CreateCategoryCommand("Смартфоны", "smartphones", null);
    using var cts = new CancellationTokenSource();
    var token = cts.Token;

    // Act
    await _handler.Handle(command, token);

    // Assert
    await _unitOfWork.Received(1).SaveChangesAsync(token);
}

[Fact]
public async Task Handle_ShouldNotSaveChanges_WhenRepositoryAddThrows()
{
    // Arrange
    var command = new CreateCategoryCommand("Смартфоны", "smartphones", null);

    _categoryRepository
        .When(r => r.Add(Arg.Any<Category>()))
        .Do(_ => throw new InvalidOperationException("DB error"));

    // Act
    var act = () => _handler.Handle(command, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<InvalidOperationException>();

    await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
}

[Fact]
public async Task Handle_ShouldReturnIdOfCreatedCategory()
{
    // Arrange
    Category? capturedCategory = null;
    _categoryRepository
        .When(r => r.Add(Arg.Any<Category>()))
        .Do(call => capturedCategory = call.Arg<Category>());

    var command = new CreateCategoryCommand("Смартфоны", "smartphones", null);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    capturedCategory.Should().NotBeNull();
    result.Should().Be(capturedCategory!.Id);
}
}