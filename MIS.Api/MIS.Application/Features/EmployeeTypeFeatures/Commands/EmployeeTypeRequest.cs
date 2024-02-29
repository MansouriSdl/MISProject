using MediatR;
using MIS.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Features.EmployeeTypeFeatures.Commands
{
    public class EmployeeTypeRequest : IRequest<MessageResponse>
    {
        public EmployeeTypeRequest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
