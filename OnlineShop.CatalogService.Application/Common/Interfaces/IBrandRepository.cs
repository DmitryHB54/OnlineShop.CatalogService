using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Interfaces;
public interface IBrandRepository
{
    Task<Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Brand?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<List<Brand>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithSlugAsync(string slug, CancellationToken cancellationToken = default);
    void Add(Brand brand);
    void Remove(Brand brand);
}