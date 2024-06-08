using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities;
using WMS.Infrastructure.Persistence.Contants;

namespace WMS.Infrastructure.Persistence.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category));
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        builder.Property(c => c.Name)
            .HasMaxLength(ConfigurationConstants.STANDARD_TEXT)
            .IsRequired();
        builder.Property(c => c.Description)
            .HasMaxLength(ConfigurationConstants.LARGE_TEXT)
            .IsRequired(false);
    }
}
