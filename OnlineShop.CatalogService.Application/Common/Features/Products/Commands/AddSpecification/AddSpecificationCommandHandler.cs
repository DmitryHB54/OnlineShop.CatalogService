using MediatR;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.AddSpecification;
public class AddSpecificationCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AddSpecificationCommand, Guid>
{
    public async Task<Guid> Handle(AddSpecificationCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithDetailsAsync(request.ProductId, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        product.AddSpecification(request.Key, request.Value, request.DisplayOrder);

        var newSpec = product.Specifications.Last();

        productRepository.AddSpecification(newSpec);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newSpec.Id;
    }
}