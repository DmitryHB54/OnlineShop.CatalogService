namespace OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;

public record ProductDetailsDto(
    Guid Id,
    string Name,
    string Slug,
    string? Description,
    decimal Price,
    string Currency,
    Guid CategoryId,
    string CategoryName,
    string BrandId,
    string BrandName,
    bool isActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IReadOnlyList<SpecificationDto> Specifications);

public record SpecificationDto(Guid Id, string Key, string Value, int DisplayOrder);