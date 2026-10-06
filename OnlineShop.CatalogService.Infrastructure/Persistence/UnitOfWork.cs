using OnlineShop.CatalogService.Application.Common.Interfaces;

namespace OnlineShop.CatalogService.Infrastructure.Persistence;

public class UnitOfWork(CatalogDbContext dbContext) : IUnitOfWork
{
    public Task<int> SsveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}