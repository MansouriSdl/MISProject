using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockAssignmentFeatures.Commands
{
    public class UpdateBureauStockAssignmentAvailabilityRequest : IRequest<int>
    {
        public UpdateBureauStockAssignmentAvailabilityRequest(List<Guid> ids)
        {
            Ids = ids;
        }

        public List<Guid> Ids { get; set; }
    }
}
