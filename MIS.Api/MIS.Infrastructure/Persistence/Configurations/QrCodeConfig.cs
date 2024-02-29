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
    public class QrCodeConfig : IEntityTypeConfiguration<QrCode>
    {
        public void Configure(EntityTypeBuilder<QrCode> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Designation)
                .HasMaxLength(20);


        }
    }
}
