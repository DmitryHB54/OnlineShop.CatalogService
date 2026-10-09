
namespace OnlineShop.CatalogService.Application.Models;
public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
    public int TotalCount { get; init; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => 
        PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);

    public bool HasNext => Page < TotalPages;
    public bool HasPrevious => Page > 1;
    public static PagedResult<T> Empty(int page, int pageSize)
        => new() { Items = Array.Empty<T>(), TotalCount = 0, Page = page, PageSize = pageSize };

    public static PagedResult<T> Create(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
        => new() { Items = items, TotalCount = totalCount, Page = page, PageSize = pageSize };
}