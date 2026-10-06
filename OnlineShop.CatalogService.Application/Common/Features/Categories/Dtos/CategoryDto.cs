namespace OnlineShop.CatalogService.Application.Common.Features.Categories.Dtos;

public record CategoryDto(
    Guid Id,
    string Name,
    string Slug,
    Guid? ParentId,
    DateTime CreatedAt);