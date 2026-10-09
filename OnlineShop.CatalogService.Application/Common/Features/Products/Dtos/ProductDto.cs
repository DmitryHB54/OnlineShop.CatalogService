namespace OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;

public record ProductDto(
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
    DateTime CreatedAt);