using MediatR;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.ReplaceSpecifications;

public class ReplaceSpecificationCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ReplaceSpecificationsCommand>
{
    public async Task Handle(ReplaceSpecificationsCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithDetailsAsync(request.ProductId, cancellationToken);
        if (product is null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        var oldSpecs = product.Specifications.ToList();
        if(oldSpecs.Count > 0)
        {
            productRepository.RemoveSpecifications(oldSpecs);
        }
        product.ClearSpecifications();

        foreach (var spec in request.Specifications)
        {
            var newSpec = new ProductSpecification(
                product.Id,
                spec.Key,
                spec.Value,
                spec.DisplayOrder);
            productRepository.AddSpecification(newSpec);
        }

        await unitOfWork.SaveChangesAsync();
    }
}