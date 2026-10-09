using MediatR;
using AutoMapper;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Application.Models;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProducts;

public class GetProductsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper) : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        ProductFilter filter = request.ToFilter();

        PagedResult<Product> pagedProducts = await productRepository.GetPagedAsync(filter, cancellationToken);

        List<ProductDto> dtos = mapper.Map<List<ProductDto>>(pagedProducts.Items);
        
        return PagedResult<ProductDto>.Create(
            dtos,
            pagedProducts.TotalCount,
            pagedProducts.Page,
            pagedProducts.PageSize);
    }
}