using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.EquipmentTypeFeatures.Commands
{
    public class DeleteEquipmentTypeRequest : IRequest<MessageResponse>
    {
        public DeleteEquipmentTypeRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
