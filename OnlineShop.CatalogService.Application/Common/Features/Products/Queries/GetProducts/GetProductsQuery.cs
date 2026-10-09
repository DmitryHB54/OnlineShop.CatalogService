using MediatR;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;
using OnlineShop.CatalogService.Application.Models;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProducts;
public class GetProductsQuery : IRequest<PagedResult<ProductDto>>
{
    public Guid? CategoryId { get; init; }
    public Guid? BrandId { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public string? Search { get; init; }
    public bool? IsActive { get; init; } = true;
    public ProductSortBy SortBy { get; init; } = ProductSortBy.Name;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public ProductFilter ToFilter() => new()
    {
        CategoryId = CategoryId,
        BrandId = BrandId,
        MinPrice = MinPrice,
        MaxPrice = MaxPrice,
        Search = Search,
        IsActive = IsActive,
        SortBy = SortBy,
        Page = Page,
        PageSize = PageSize
    };

}