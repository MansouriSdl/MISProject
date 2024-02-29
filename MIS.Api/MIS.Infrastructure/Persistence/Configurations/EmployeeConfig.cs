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
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .HasMaxLength(50);

            builder.Property(e => e.IdentityCard)
                .HasMaxLength(10);

            builder.Property(e => e.WorkId)
                .HasMaxLength(10);

            builder.HasOne(u => u.EmployeeType)
                .WithMany(a => a.Employees)
                .HasForeignKey(u => u.EmployeeTypeId);

            builder.HasOne(u => u.Service)
               .WithMany(a => a.Employees)
               .HasForeignKey(u => u.ServiceId)
               .IsRequired(false);
        }
    }
}
