using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierTypeFeatures.Commands
{
    public class SupplierTypeRequest : IRequest<MessageResponse>
    {
        public SupplierTypeRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
