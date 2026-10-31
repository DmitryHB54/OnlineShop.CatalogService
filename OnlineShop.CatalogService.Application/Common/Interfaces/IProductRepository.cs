using OnlineShop.CatalogService.Application.Models;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Interfaces;
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Product>> GetPagedAsync(ProductFilter filter, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithSlugAsync(string slug, CancellationToken cancellationToken = default);
    void AddSpecification(ProductSpecification specification);
    void RemoveSpecification(ProductSpecification specification);
    void RemoveSpecifications(IEnumerable<ProductSpecification> specifications);
    void Add(Product product);
    void Remove(Product product);
}