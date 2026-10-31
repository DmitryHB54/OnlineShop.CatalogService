using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
     string Name,
    string Slug,
    decimal Price,
    Guid CategoryId,
    Guid BrandId,
    string? Description = null,
    string Currency = "BYN",
    List<CreateProductSpecificationDto>? Specifications = null) : IRequest<Guid>;

public record CreateProductSpecificationDto(
    string Key,
    string Value,
    int DisplayOrder = 0);