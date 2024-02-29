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
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(e => e.Id);


            builder.Property(o => o.Description)
                .HasMaxLength(150);
            builder.Property(o => o.Type)
               .HasMaxLength(20);
            builder.Property(o => o.ContentType)
               .HasMaxLength(20);
            builder.Property(o => o.Extention)
               .HasMaxLength(5);
        }
    }
}

