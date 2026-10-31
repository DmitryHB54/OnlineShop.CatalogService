using MediatR;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateSpecification;

public class UpdateSpecificationCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateSpecificationCommand>
{
    public async Task Handle(UpdateSpecificationCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdWithDetailsAsync(request.ProductId, cancellationToken);
        
        if (product is null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        var spec = product.Specifications.FirstOrDefault(s => s.Id == request.SpecificationId);

        if(spec is null)
            throw new NotFoundException(nameof(ProductSpecification), request.SpecificationId);

        spec.Update(request.Key, request.Value, request.DisplayOrder);

        await unitOfWork.SaveChangesAsync();
    }
}