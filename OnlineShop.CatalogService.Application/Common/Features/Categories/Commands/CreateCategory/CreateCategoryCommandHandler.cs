using MediatR;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name, request.Slug, request.ParentId);

        categoryRepository.Add(category);
        await unitOfWork.SsveChangesAsync(cancellationToken);

        return category.Id;
    }
}