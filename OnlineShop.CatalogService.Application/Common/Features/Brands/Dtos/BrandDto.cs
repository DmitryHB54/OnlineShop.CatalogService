namespace OnlineShop.CatalogService.Application.Common.Features.Brands.Dtos;
public record BrandDto(
    Guid Id,
    string Name,
    string Slug,
    string? LogoUrl,
    DateTime CreatedAt);