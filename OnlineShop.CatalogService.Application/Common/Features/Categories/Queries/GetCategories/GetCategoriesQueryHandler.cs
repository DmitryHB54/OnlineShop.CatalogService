using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Dtos;
using OnlineShop.CatalogService.Application.Common.Interfaces;

namespace OnlineShop.CatalogService.Application.Common.Features.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    : IRequestHandler<GetCategoreisQuery, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetCategoreisQuery request, CancellationToken cancellationToken)
    {
        return await categoryRepository.Query()
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ProjectTo<CategoryDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}