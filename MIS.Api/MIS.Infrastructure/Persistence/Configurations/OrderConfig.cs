using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(s => s.User)
               .WithMany(d => d.Orders)
               .HasForeignKey(s => s.UserId);

            builder.HasOne(s => s.Supplier)
               .WithMany(d => d.Orders)
               .HasForeignKey(s => s.SupplierId);

            builder.HasOne(s => s.Equipment)
               .WithMany(d => d.Orders)
               .HasForeignKey(s => s.EquipmentId);

            builder.HasOne(o => o.Attachment)
                .WithOne(a => a.Order)
                .HasForeignKey<Order>(o => o.AttachmentId);
        }
    }
}
