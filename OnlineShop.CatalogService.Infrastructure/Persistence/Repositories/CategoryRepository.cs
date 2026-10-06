using Microsoft.EntityFrameworkCore;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Persistence.Repositories;

public class CategoryRepository(CatalogDbContext dbContext) : ICategoryRepository
{
    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    public Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        => dbContext.Categories.FirstOrDefaultAsync(c => c.Slug == slug, cancellationToken);
    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        => dbContext.Categories.AsNoTracking().ToListAsync();
    public Task<bool> ExistsWithSlugAsync(string slug, CancellationToken cancellationToken = default)
        => dbContext.Categories.AnyAsync(c => c.Slug == slug, cancellationToken);
    public void Add(Category category) => dbContext.Categories.Add(category);
    public void Remove(Category category) => dbContext.Categories.Remove(category);
}