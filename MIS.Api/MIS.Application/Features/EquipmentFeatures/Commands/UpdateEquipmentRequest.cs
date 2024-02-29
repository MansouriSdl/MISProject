using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.EquipmentFeatures.Commands
{
    public class UpdateEquipmentRequest : IRequest<MessageResponse>
    {
        public UpdateEquipmentRequest(Guid id, EquipmentRequest equipmentRequest)
        {
            Id = id;
            EquipmentRequest = equipmentRequest;
        }

        public Guid Id { get; set; }
        public EquipmentRequest EquipmentRequest { get; set; }
    }
}
