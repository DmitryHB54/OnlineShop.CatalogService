using OnlineShop.CatalogService.Domain.Commom;

namespace OnlineShop.CatalogService.Domain.Entities;
public class ProductSpecification : Entity
{
    public Guid ProductId { get; private set; }
    public string Key { get; private set; } = null!;
    public string Value { get; private set; } = null!;
    public int DisplayOrder { get; private set; }

    private ProductSpecification() { }

    public ProductSpecification(Guid productId, string key, string value, int displayOrder = 0)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be empty", nameof(key));
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty", nameof(value));

        ProductId = productId;
        Key = key;
        Value = value;
        DisplayOrder = displayOrder;
    }
}