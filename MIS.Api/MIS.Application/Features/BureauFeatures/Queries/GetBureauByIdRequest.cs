using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauFeatures.Queries
{
    public class GetBureauByIdRequest : IRequest<BureauResponse>
    {
        public GetBureauByIdRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
