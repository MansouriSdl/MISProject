using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.EmployeeTypeFeatures.Commands
{
    public class DeleteEmployeeTypeRequestHandler : IRequestHandler<DeleteEmployeeTypeRequest, MessageResponse>
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public DeleteEmployeeTypeRequestHandler(IEmployeeTypeRepository employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
        }

        public Task<MessageResponse> Handle(DeleteEmployeeTypeRequest request, CancellationToken cancellationToken)
        {
            return _employeeTypeRepository.DeleteEmployeeType(request.Id);
        }
    }
}
