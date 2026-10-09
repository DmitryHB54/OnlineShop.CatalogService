using MediatR;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProductById;
public record GetProductByIdQuery(Guid Id) : IRequest<ProductDetailsDto>;