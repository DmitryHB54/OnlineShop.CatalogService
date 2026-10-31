using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    Guid CategoryId,
    Guid BrandId) : IRequest;