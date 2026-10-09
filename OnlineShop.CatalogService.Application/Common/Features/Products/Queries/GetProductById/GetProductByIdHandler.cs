using AutoMapper;
using MediatR;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProductById;

public class GetProductByIdHandler(IProductRepository productRepository,
    IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
{
    public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

        if (product is null || !product.IsActive)
            throw new NotFoundException(nameof(Product), request.Id);

        return mapper.Map<ProductDetailsDto>(product);
    }
}