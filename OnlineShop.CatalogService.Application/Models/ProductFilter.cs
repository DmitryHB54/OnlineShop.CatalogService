namespace OnlineShop.CatalogService.Application.Models;

public enum ProductSortBy
{
    Name,
    PriceAsc,
    PriceDesc,
    Newest
}

public class ProductFilter : PaginationParams
{
    public Guid? CategoryId { get; set; }
    public Guid? BrandId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Search { get; set; }
    public bool? IsActive { get; set; } = true;
    public ProductSortBy SortBy { get; set; } = ProductSortBy.Name;
}