using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceFeatures.Commands
{
    public class ServiceRequest : IRequest<MessageResponse>
    {
        public ServiceRequest(string name, Guid divisionId)
        {
            Name = name;
            DivisionId = divisionId;
        }

        public string Name { get; set; }
        public Guid DivisionId { get; set; }
    }
}
