using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceFeatures.Commands
{
    public class UpdateServiceRequest : IRequest<MessageResponse>
    {
        public UpdateServiceRequest(Guid id, ServiceRequest serviceRequest)
        {
            Id = id;
            ServiceRequest = serviceRequest;
        }

        public Guid Id { get; set; }
        public ServiceRequest ServiceRequest { get; set; }
    }
}
