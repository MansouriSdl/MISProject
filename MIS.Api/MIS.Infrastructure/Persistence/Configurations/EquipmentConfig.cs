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
    public class EquipmentConfig : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(150);

            builder.Property(e => e.Description)
               .HasMaxLength(255);

            builder.Property(e => e.SerialNumber)
               .HasMaxLength(20);

            builder.HasOne(et => et.ConsumptionType)
                .WithMany(ct => ct.Equipments)
                .HasForeignKey(et => et.ConsumptionTypeId);

            builder.HasOne(e => e.EquipmentType)
                .WithMany(et => et.Equipments)
                .HasForeignKey(e => e.EquipmentTypeId);


        }
    }
}
