using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauFeatures.Commands
{
    public class DeleteBureauRequest : IRequest<MessageResponse>
    {
        public DeleteBureauRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
