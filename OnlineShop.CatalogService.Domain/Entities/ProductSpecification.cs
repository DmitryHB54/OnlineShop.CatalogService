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
        ValidateKey(key);
        ValidateValue(value);

        ProductId = productId;
        Key = key;
        Value = value;
        DisplayOrder = displayOrder;
    }

    public void Update(string key, string value, int displayOrder)
    {
        ValidateKey(key);
        ValidateValue(value);

        Key = key;
        Value = value;
        DisplayOrder = displayOrder;
    }

    private void ValidateKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be empty", nameof(key));
    }

    private void ValidateValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be empty", nameof(value));
    }
}