namespace OnlineShop.CatalogService.Application.Common.Interfaces;
public interface IUnitOfWork
{
    Task<int> SsveChangesAsync(CancellationToken cancellationToken = default);
}