using MIS.Application.Features.EquipmentFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        Task<IEnumerable<EquipmentResponse>> GetAllEquipments();
        Task<MessageResponse> AddEquipment(EquipmentRequest request);
        Task<MessageResponse> UpdateEquipment(UpdateEquipmentRequest request);
        Task<MessageResponse> DeleteEquipment(Guid id);
    }
}
