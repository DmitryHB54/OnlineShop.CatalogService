using MediatR;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.RemoveSpecification;

public class RemoveSpecificationCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveSpecificationCommand>
{
    public async Task Handle(RemoveSpecificationCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithDetailsAsync(request.ProductId, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        ProductSpecification? spec = product.Specifications.FirstOrDefault(s => s.Id == request.SpecificationId);
        if (spec is null)
            throw new NotFoundException(nameof(ProductSpecification), request.SpecificationId);

        product.RemoveSpecification(request.SpecificationId);
        productRepository.RemoveSpecification(spec);

        await unitOfWork.SaveChangesAsync();
    }
}