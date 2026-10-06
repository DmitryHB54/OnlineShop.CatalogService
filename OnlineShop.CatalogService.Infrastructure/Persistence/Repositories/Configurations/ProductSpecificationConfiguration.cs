using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Persistence.Repositories.Configurations;
public class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        builder.ToTable("product_specifications");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Key).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Value).IsRequired().HasMaxLength(1000);

        builder.HasIndex(x => x.ProductId);

        builder.Ignore(x => x.DomainEvents);
    }
}