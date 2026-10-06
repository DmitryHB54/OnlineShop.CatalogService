using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Persistence.Repositories.Configurations;
public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("brands");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(200);
        builder.Property(x => x.LogoUrl).HasMaxLength(500);

        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Ignore(x => x.DomainEvents);
    }
}