using MediatR;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Dtos;

namespace OnlineShop.CatalogService.Application.Common.Features.Categories.Queries.GetCategories;

public record GetCategoreisQuery() : IRequest<List<CategoryDto>>;