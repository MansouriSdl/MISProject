using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauFeatures.Commands
{
    public class UpdateBureauRequest : IRequest<MessageResponse>
    {
        public UpdateBureauRequest(Guid id, BureauRequest bureauRequest)
        {
            Id = id;
            BureauRequest = bureauRequest;
        }

        public Guid Id { get; set; }
        public BureauRequest BureauRequest { get; set; }
    }
}
