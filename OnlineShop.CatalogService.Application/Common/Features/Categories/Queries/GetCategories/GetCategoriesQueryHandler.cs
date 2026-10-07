using MediatR;
using AutoMapper;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Dtos;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    : IRequestHandler<GetCategoreisQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetCategoreisQuery request, CancellationToken cancellationToken)
    {
        List<Category> categories = await categoryRepository.GetAllAsync(cancellationToken);
        return mapper.Map<List<CategoryDto>>(categories);
    }
}