using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.DivisionFeatures.Commands
{
    public class DeleteDivisionRequest : IRequest<MessageResponse>
    {
        public DeleteDivisionRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

    }
}
