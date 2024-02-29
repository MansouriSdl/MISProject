using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.DivisionFeatures.Commands
{
    public class UpdateDivisionRequest : IRequest<MessageResponse>
    {
        public UpdateDivisionRequest(Guid id, DivisionRequest divisionRequest)
        {
            Id = id;
            DivisionRequest = divisionRequest;
        }

        public Guid Id { get; set; }
        public DivisionRequest DivisionRequest { get; set; }
    }
}
