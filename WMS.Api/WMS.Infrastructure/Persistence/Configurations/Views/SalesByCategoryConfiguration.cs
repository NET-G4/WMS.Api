using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain.Entities.Views;

namespace WMS.Infrastructure.Persistence.Configurations.Views
{
    internal class SalesByCategoryConfiguration : IEntityTypeConfiguration<SalesByCategory>
    {
        public void Configure(EntityTypeBuilder<SalesByCategory> builder)
        {
            builder
                .ToView(nameof(SalesByCategory))
                .HasKey(el => el.Id);
        }
    }
}
