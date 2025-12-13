using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreHub.Core.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreHub.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.Address, Address =>
            {
                Address.WithOwner();
                Address.Property(a => a.City).IsRequired().HasMaxLength(180);

            });
            builder.HasMany(o => o.OrderItem).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(o => o.PaymentStatus).HasConversion(new EnumToStringConverter<PaymentStatus>());
        }
    }
}
