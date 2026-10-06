using OnlineShop.CatalogService.Domain.Commom;

namespace OnlineShop.CatalogService.Domain.Entities;
public class Product : Entity
{
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public string Currency { get; private set; } = "BYN";
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Guid BrandId { get; private set; }
    public Brand Brand { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private readonly List<ProductSpecification> _specifications = [];
    public IReadOnlyCollection<ProductSpecification> Specifications => _specifications.AsReadOnly();

    private Product() { }
    public Product(
        string name,
        string slug,
        decimal price,
        Guid categoryId,
        Guid brandId,
        string? description = null,
        string currency = "BYN")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug cannot be empty", nameof(slug));
        if (price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(price));

        Name = name;
        Slug = slug;
        Price = price;
        CategoryId = categoryId;
        BrandId = brandId;
        Description = description;
        Currency = currency;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Price cannot be negative", nameof(newPrice));

        Price = newPrice;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}