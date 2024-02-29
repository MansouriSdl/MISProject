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
    public class StockConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Designation)
                .HasMaxLength(150);

            builder.HasOne(s => s.Equipment)
                .WithMany(e => e.Stocks)
                .HasForeignKey(s => s.EquipmentId);

            builder.HasOne(s => s.User)
               .WithMany(e => e.Stocks)
               .HasForeignKey(s => s.UserId);

        }
    }
}
