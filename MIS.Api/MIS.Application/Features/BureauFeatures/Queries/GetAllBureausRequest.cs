using MediatR;
using MIS.Application.Models.Responses;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauFeatures.Queries
{
    public class GetAllBureausRequest : IRequest<IEnumerable<BureauResponse>>
    {
    }
}
