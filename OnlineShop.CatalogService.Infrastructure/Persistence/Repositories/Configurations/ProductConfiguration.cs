using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Persistence.Repositories.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Description).HasMaxLength(4000);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Currency).IsRequired().HasMaxLength(3);

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.BrandId);

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Specifications)
            .WithOne()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Specifications)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore(x => x.DomainEvents);
    }
}