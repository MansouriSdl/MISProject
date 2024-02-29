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
    class ServiceEquipmentRequestConfig : IEntityTypeConfiguration<ServiceEquipmentRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceEquipmentRequest> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Observation)
               .HasMaxLength(255);

            builder.HasOne(s => s.Service)
                .WithMany(d => d.ServiceEquipmentRequests)
                .HasForeignKey(s => s.ServiceId);

            builder.HasOne(s => s.Equipment)
                .WithMany(d => d.ServiceEquipmentRequests)
                .HasForeignKey(s => s.EquipmentId);


            builder.HasOne(s => s.User)
                .WithMany(d => d.ServiceEquipmentRequests)
                .HasForeignKey(s => s.UserId);

            builder.HasOne(d => d.Order)
                .WithOne(o => o.ServiceEquipmentRequest)
                .HasForeignKey<ServiceEquipmentRequest>(d => d.OrderId);




        }
    }
}
