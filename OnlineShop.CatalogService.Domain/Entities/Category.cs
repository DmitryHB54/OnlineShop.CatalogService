using OnlineShop.CatalogService.Domain.Commom;

namespace OnlineShop.CatalogService.Domain.Entities;
public class Category : Entity
{
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public Guid? ParentId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Category() { }
    public Category(string name, string slug, Guid? parentId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Name cannot be empty", nameof(slug));

        Name = name;
        Slug = slug;
        ParentId = parentId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Name cannot be empty", nameof(newName));

        Name = newName;
    }
}