using MediatR;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.ServiceFeatures.Queries
{
    public class GetServicesByDivisionId : IRequest<IEnumerable<Service>>
    {
        public Guid DivisionId { get; set; }

        public GetServicesByDivisionId(Guid divisionId)
        {
            DivisionId = divisionId;
        }
    }
}
