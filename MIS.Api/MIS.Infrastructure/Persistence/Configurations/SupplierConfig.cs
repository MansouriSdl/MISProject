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
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(e => e.Id);

            

            builder.Property(o => o.CompanyName)
                .HasMaxLength(200);
            builder.Property(o => o.PhoneNumber)
               .HasMaxLength(15);
            builder.Property(o => o.BankAccountNumber)
               .HasMaxLength(30);
            builder.Property(o => o.CommanCompanyIndentifier)
               .HasMaxLength(30);
            builder.HasOne(s => s.SupplierType)
                 .WithMany(s => s.Suppliers)
                 .HasForeignKey(s => s.SupplierTypeId);
            
            
        }
    }
}
