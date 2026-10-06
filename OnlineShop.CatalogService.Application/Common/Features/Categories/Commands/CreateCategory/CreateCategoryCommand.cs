using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string Slug,
    Guid? ParentId = null) : IRequest<Guid>;