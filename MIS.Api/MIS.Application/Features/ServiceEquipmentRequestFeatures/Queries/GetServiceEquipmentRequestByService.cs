using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.DivisionEquipmentRequestFeatures.Queries
{
    public class GetServiceEquipmentRequestByService : IRequest<IEnumerable<ServiceEquipmentRequestResponse>>
    {
        public GetServiceEquipmentRequestByService(Guid userId, FilterRequest filterRequest)
        {
            UserId = userId;
            FilterRequest = filterRequest;
        }

        public Guid UserId { get; set; }
        public FilterRequest FilterRequest { get; set; }
    }
}
