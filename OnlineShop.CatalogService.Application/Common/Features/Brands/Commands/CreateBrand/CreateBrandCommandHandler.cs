using MediatR;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler(
    IBrandRepository brandRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateBrandCommand, Guid>
{
    public async Task<Guid> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = new Brand(request.Name, request.Slug, request.LogoUrl);

        brandRepository.Add(brand);
        await unitOfWork.SaveChangesAsync();

        return brand.Id;
    }
}