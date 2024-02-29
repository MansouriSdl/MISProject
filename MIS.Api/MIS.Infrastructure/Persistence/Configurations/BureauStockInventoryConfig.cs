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
    public class BureauStockInventoryConfig : IEntityTypeConfiguration<BureauStockInventory>
    {
        public void Configure(EntityTypeBuilder<BureauStockInventory> builder)
        {

            builder.HasKey(e => e.Id);

            builder.HasOne(s => s.Bureau)
                .WithMany(d => d.BureauStockInventories)
                .HasForeignKey(s => s.BureauId);

            builder.HasOne(s => s.Stock)
                .WithMany(d => d.BureauStockInventories)
                .HasForeignKey(s => s.StockId);

            builder.HasOne(s => s.QrCode)
                .WithMany(d => d.BureauStockInventories)
                .HasForeignKey(s => s.QrCodeId);

            builder.HasOne(s => s.User)
                .WithMany(d => d.BureauStockInventories)
                .HasForeignKey(s => s.UserId);

            builder.HasOne(i => i.Supplier)
                 .WithMany(s => s.BureauStockInventories)
                 .HasForeignKey(i => i.SupplierId)
                 .IsRequired(false);

            builder.Property(i => i.Designation)
                .HasMaxLength(50);

            builder.Property(i => i.MarketReference)
                .HasMaxLength(50);
            builder.Property(i => i.MarketObject)
               .HasMaxLength(100);


        }
    }
}
