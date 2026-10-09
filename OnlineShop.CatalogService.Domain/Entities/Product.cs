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
    public Product(string name, string slug, decimal price, Guid categoryId,
        Guid brandId, string? description = null, string currency = "BYN")
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidatePrice(price);

        Name = name; Slug = slug;
        Price = price; CategoryId = categoryId;
        BrandId = brandId; Description = description;
        Currency = currency; CreatedAt = DateTime.UtcNow;
    }

    public void UpdateBasicInfo(string name, string? description)
    {
        ValidateName(name);

        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePrice(decimal newPrice)
    {
        ValidatePrice(newPrice);

        Price = newPrice;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeCategory(Guid categoryId)
    {
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeBrand(Guid brandId)
    {
        BrandId = brandId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    // Specifications

    public void AddSpecification(string key, string value, int displayOrder = 0)
    {
        var spec = new ProductSpecification(Id, key, value, displayOrder);
        _specifications.Add(spec);
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void RemoveSpecification(Guid specificationId)
    {
        var spec = _specifications.FirstOrDefault(s => s.Id == specificationId);
        if (spec is null)
            return;

        _specifications.Remove(spec);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearSpecification()
    {
        _specifications.Clear();
        UpdatedAt = DateTime.UtcNow;
    }

    // Validation

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
    }

    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug cannot be empty", nameof(slug));
    }

    private static void ValidatePrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price cannot be negative", nameof(price));
    }
}