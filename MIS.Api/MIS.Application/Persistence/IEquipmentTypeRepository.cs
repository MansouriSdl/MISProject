using MIS.Application.Features.EquipmentTypeFeatures.Commands;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Persistence
{
    public interface IEquipmentTypeRepository : IRepository<EquipmentType>
    {
        Task<IEnumerable<EquipmentTypeResponse>> GetAllEquipmentTypes();
        Task<MessageResponse> AddEquipmentType(EquipmentTypeRequest request);
        Task<MessageResponse> UpdateEquipmentType(UpdateEquipmentTypeRequest request);
        Task<MessageResponse> DeleteEquipmentType(Guid id);
    }
}
