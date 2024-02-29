using AutoMapper;
using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.SupplierFeatures.Commands
{
    public class UpdateSupplierRequestHandler : IRequestHandler<UpdateSupplierRequest, MessageResponse>
    {
        private readonly ISupplierRepository _supplierRepository;

        public UpdateSupplierRequestHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public Task<MessageResponse> Handle(UpdateSupplierRequest request, CancellationToken cancellationToken)
        {
            return _supplierRepository.UpdateSupplier(request);
        }
    }
}
