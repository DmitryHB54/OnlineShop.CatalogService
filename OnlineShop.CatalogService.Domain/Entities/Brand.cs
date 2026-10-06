using OnlineShop.CatalogService.Domain.Commom;

namespace OnlineShop.CatalogService.Domain.Entities;
public class Brand : Entity
{
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string? LogoUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Brand() { }
    public Brand(string name, string slug, string? logoUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug cannot be empty", nameof(slug));

        Name = name;
        Slug = slug;
        LogoUrl = logoUrl;
        CreatedAt = DateTime.UtcNow;
    }
}