using MediatR;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Application.Common.Exceptions;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.Id);

        product.UpdateBasicInfo(request.Name, request.Description);
        product.ChangePrice(request.Price);
        product.ChangeCategory(request.CategoryId);
        product.ChangeBrand(request.BrandId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}