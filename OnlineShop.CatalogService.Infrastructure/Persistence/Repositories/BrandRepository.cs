using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Persistence.Repositories;

public class BrandRepository(CatalogDbContext dbContext) : IBrandRepository
{
    public Task<Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => dbContext.Brands.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    public Task<Brand?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        => dbContext.Brands.FirstOrDefaultAsync(b => b.Slug == slug, cancellationToken);
    public Task<List<Brand>> GetAllAsync(CancellationToken cancellationToken)
        => dbContext.Brands
            .AsNoTracking()
                .OrderBy(b => b.Name)
                    .ToListAsync(cancellationToken);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => dbContext.Brands.AnyAsync(b => b.Id == id, cancellationToken);
    public Task<bool> ExistsWithSlugAsync(string slug, CancellationToken cancellationToken = default)
        => dbContext.Brands.AnyAsync(b => b.Slug == slug);

    public void Add(Brand brand) => dbContext.Brands.Add(brand);
    public void Remove(Brand brand) => dbContext.Brands.Remove(brand);
}