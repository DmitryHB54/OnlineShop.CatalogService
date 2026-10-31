namespace OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;

public record ProductSpecificationDto(
    Guid ProductId,
    string ProductName,
    IReadOnlyList<SpecificationDto> Specifications);