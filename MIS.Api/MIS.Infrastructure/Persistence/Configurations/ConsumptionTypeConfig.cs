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
    public class ConsumptionTypeConfig : IEntityTypeConfiguration<ConsumptionType>
    {
        public void Configure(EntityTypeBuilder<ConsumptionType> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(150);


        }
    }
}

