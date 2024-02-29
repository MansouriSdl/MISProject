using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceFeatures.Commands
{
    public class DeleteServiceRequest : IRequest<MessageResponse>
    {
        public DeleteServiceRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
