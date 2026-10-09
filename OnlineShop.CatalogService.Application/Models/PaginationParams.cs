namespace OnlineShop.CatalogService.Application.Models;
public class PaginationParams
{
    public const int MaxPageSize = 100;
    public const int DefaultPageSize = 20;

    private int _pageSize = DefaultPageSize;
    private int _page = 1;

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch
        {
            < 1 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => value
        };
    }

    public int Skip => (Page - 1) * PageSize;
}