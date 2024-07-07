using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Domain.Entities;

namespace WMS.Infrastructure.Persistence.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));
        builder.HasKey(x => x.Id);

        builder.HasMany(c => c.Sales)
            .WithOne(s => s.Customer)
            .HasForeignKey(s => s.CustomerId);

        builder.Property(p => p.FirstName)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(p => p.LastName)
            .HasMaxLength(255)
            .IsRequired(false);
        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p => p.Address)
            .HasMaxLength(500)
            .IsRequired(false);
    }
}
