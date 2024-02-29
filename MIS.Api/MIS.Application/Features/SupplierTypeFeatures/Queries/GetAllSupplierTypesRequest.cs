using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierTypeFeatures.Queries
{
    public class GetAllSupplierTypesRequest : IRequest<IEnumerable<SupplierTypeResponse>>
    {
    }
}
