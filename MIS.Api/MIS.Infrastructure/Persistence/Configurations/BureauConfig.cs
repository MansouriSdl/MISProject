using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MIS.Domain.Commons;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Configurations
{
    public class BureauConfig : IEntityTypeConfiguration<Bureau>
    {
        public void Configure(EntityTypeBuilder<Bureau> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(150);

            builder.Property(e => e.Abbreviation)
                .HasMaxLength(10);

            builder.HasOne(s => s.Service)
                .WithMany(d => d.Bureaus)
                .HasForeignKey(s => s.ServiceId);


        }
    }
}
