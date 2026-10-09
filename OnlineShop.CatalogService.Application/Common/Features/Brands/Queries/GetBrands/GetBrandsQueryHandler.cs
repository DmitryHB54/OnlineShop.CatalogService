using AutoMapper;
using MediatR;
using OnlineShop.CatalogService.Application.Common.Features.Brands.Dtos;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Brands.Queries.GetBrands;
public class GetBrandsQueryHandler(
    IBrandRepository brandRepository,
    IMapper mapper) : IRequestHandler<GetBrandsQuery, List<BrandDto>>
{
    public async Task<List<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        List<Brand> brands = await brandRepository.GetAllAsync(cancellationToken);
        return mapper.Map<List<BrandDto>>(brands);
    }
}