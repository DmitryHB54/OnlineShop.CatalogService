using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Persistence.Repositories;

public class BrandRepository(CatalogDbContext dbContext) : IBrandRepository
{
    public Task<Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => dbContext.Brands.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        => dbContext.Brands.AnyAsync(b => b.Id == id, cancellationToken);
    public void Add(Brand brand) => dbContext.Brands.Add(brand);
    
}