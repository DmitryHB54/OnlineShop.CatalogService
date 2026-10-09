using AutoMapper;
using MediatR;
using OnlineShop.CatalogService.Application.Common.Features.Brands.Dtos;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Brands.Queries.GetBrandById;

public class GetBrandByIdQueryHandler(
    IBrandRepository brandRepository,
    IMapper mapper) : IRequestHandler<GetBrandByIdQuery, BrandDto>
{
    public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await brandRepository.GetByIdAsync(request.Id, cancellationToken);
        if (brand is null)
            throw new NotFoundException(nameof(Brand), request.Id);

        return mapper.Map<BrandDto>(brand);
    }
}