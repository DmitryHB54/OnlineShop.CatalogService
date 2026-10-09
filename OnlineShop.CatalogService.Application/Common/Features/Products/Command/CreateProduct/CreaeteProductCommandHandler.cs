using MediatR;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Command.CreateProduct;

public class CreaeteProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Slug,
            request.Price,
            request.CategoryId,
            request.BrandId,
            request.Description,
            request.Currency);

        if(request.Specifications is { Count: > 0 } )
        {
            foreach (var spec in request.Specifications)
            {
                product.AddSpecification(spec.Key, spec.Value, spec.DisplayOrder);
            }
        };

        productRepository.Add(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}