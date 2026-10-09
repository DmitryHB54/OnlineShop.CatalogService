using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Application.Models;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Persistence.Repositories;

public class ProductRepository(CatalogDbContext dbContext) : IProductRepository
{
    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    public Task<Product?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        => dbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Specifications)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    public Task<bool> ExistsWithSlugAsync(string slug, CancellationToken cancellationToken = default)
        => dbContext.Products.AnyAsync(p => p.Slug == slug, cancellationToken);
    public void Add(Product product) => dbContext.Products.Add(product);
    public void Remove(Product product) => dbContext.Products.Remove(product);

    public async Task<PagedResult<Product>> GetPagedAsync(ProductFilter filter, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .AsQueryable();

        // Filters

        if (filter.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

        if (filter.BrandId.HasValue)
            query = query.Where(p => p.BrandId == filter.BrandId.Value);

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);

        if(!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLower();
            query = query.Where(p =>
                EF.Functions.ILike(p.Name, $"%{search}%") ||
                EF.Functions.ILike(p.Description ?? string.Empty, $"%{search}%"));
        }

        if (filter.IsActive.HasValue)
            query = query.Where(p => p.IsActive == filter.IsActive.Value);

        // Sorting

        query = filter.SortBy switch
        {
            ProductSortBy.Name => query.OrderBy(p => p.Name),
            ProductSortBy.PriceAsc => query.OrderBy(p => p.Price),
            ProductSortBy.PriceDesc => query.OrderByDescending(p => p.Price),
            ProductSortBy.Newest => query.OrderBy(p => p.CreatedAt),
            _ => query.OrderBy(p => p.Name)
        };

        // Pagination
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(filter.Skip)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        return PagedResult<Product>.Create(items, totalCount, filter.Page, filter.PageSize);
    }
}