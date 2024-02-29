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
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(u => u.Employee)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey<ApplicationUser>(u => u.EmployeeId);


            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(14);
            builder.Property(e => e.Email)
                .HasMaxLength(150);
        }
    }
}
