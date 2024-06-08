using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities;
using WMS.Infrastructure.Persistence.Contants;

namespace WMS.Infrastructure.Persistence.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        builder.Property(p => p.Name)
            .HasMaxLength(ConfigurationConstants.STANDARD_TEXT)
            .IsRequired();
        builder.Property(d => d.Description)
            .HasMaxLength(ConfigurationConstants.LARGE_TEXT)
            .IsRequired(false);
    }
}
