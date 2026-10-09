using MediatR;
using OnlineShop.CatalogService.Application.Common.Features.Brands.Dtos;

namespace OnlineShop.CatalogService.Application.Common.Features.Brands.Queries.GetBrands;

public record GetBrandsQuery : IRequest<List<BrandDto>>;