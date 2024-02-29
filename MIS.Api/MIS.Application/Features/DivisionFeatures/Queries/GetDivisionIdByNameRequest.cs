using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.DivisionFeatures.Queries
{
    public class GetDivisionIdByNameRequest : IRequest<Guid>
    {
        public GetDivisionIdByNameRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
