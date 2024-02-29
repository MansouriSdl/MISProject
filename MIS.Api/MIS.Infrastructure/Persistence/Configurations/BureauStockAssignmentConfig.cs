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
    public class BureauStockAssignmentConfig : IEntityTypeConfiguration<BureauStockAssignment>
    {
        public void Configure(EntityTypeBuilder<BureauStockAssignment> builder)
        {

            builder.HasKey(e => e.Id);

            builder.HasOne(s => s.Bureau)
                .WithMany(d => d.BureauStockAssignments)
                .HasForeignKey(s => s.BureauId);

            builder.HasOne(s => s.Stock)
                .WithMany(d => d.BureauStockAssignments)
                .HasForeignKey(s => s.StockId);

            builder.HasOne(s => s.User)
                .WithMany(d => d.BureauStockAssignments)
                .HasForeignKey(s => s.UserId);


        }
    }
}
