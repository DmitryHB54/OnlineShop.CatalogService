using AutoMapper;
using MediatR;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProductSpecifiations;

public class GetProductsSpecificationsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper) : IRequestHandler<GetProductSpecificationsQuery, ProductSpecificationDto>
{
    public async Task<ProductSpecificationDto> Handle(GetProductSpecificationsQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithDetailsAsync(request.ProductId, cancellationToken);
        
        if (product is null || !product.IsActive)
            throw new NotFoundException(nameof(Product), request.ProductId);

        var specs = product.Specifications
            .OrderBy(s => s.DisplayOrder)
            .Select(mapper.Map<SpecificationDto>)
            .ToList();

        return new ProductSpecificationDto(product.Id, product.Name, specs);
    }
}