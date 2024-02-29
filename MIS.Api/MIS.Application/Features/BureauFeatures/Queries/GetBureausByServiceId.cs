using MediatR;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauFeatures.Queries
{
    public class GetBureausByServiceId : IRequest<IEnumerable<Bureau>>
    {
        public Guid ServiceId;

        public GetBureausByServiceId(Guid serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
