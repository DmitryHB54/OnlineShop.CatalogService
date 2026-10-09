using MediatR;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Command.ActivateProduct;

public class ActivateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<ActivateProductCommand>
{
    public async Task Handle(ActivateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.Id);

        product.Activate();
        await unitOfWork.SaveChangesAsync();
    }
}