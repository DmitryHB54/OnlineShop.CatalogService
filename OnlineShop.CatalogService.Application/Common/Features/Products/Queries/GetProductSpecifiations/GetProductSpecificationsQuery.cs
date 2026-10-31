using MediatR;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProductSpecifiations;

public record GetProductSpecificationsQuery(Guid ProductId) : IRequest<ProductSpecificationDto>;