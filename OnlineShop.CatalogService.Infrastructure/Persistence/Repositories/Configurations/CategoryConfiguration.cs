using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Infrastructure.Persistence.Repositories.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => x.Slug).IsUnique();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Ignore(x => x.DomainEvents);
    }
}