using MediatR;
using OnlineShop.CatalogService.Application.Common.Features.Brands.Dtos;

namespace OnlineShop.CatalogService.Application.Common.Features.Brands.Queries.GetBrandById;

public record GetBrandByIdQuery(Guid Id) : IRequest<BrandDto>;