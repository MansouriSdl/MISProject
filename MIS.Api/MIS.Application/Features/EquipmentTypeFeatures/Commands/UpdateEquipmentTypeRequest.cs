using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.EquipmentTypeFeatures.Commands
{
    public class UpdateEquipmentTypeRequest : IRequest<MessageResponse>
    {
        public UpdateEquipmentTypeRequest(Guid id, EquipmentTypeRequest equipmentTypeRequest)
        {
            Id = id;
            EquipmentTypeRequest = equipmentTypeRequest;
        }

        public Guid Id { get; set; }
        public EquipmentTypeRequest EquipmentTypeRequest { get; set; }
    }
}
