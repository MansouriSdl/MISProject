using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierFeatures.Queries
{
    public class GetSuppliersByTypeRequest : IRequest<IEnumerable<NameResponse>>
    {
        public GetSuppliersByTypeRequest(Guid typeId)
        {
            TypeId = typeId;
        }

        public Guid TypeId { get; set; }
    }
}
