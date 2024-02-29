using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceFeatures.Commands
{
    public class GetServiceIdByUserIdRequest : IRequest<Guid>
    {
        public GetServiceIdByUserIdRequest(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
