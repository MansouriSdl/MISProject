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

namespace MIS.Application.Features.BureauFeatures.Commands
{
    public class UpdateBureauRequestHandler : IRequestHandler<UpdateBureauRequest, MessageResponse>
    {
        private readonly IBureauRepository _bureauRepository;

        public UpdateBureauRequestHandler(IBureauRepository bureauRepository)
        {
            _bureauRepository = bureauRepository;
        }

        public Task<MessageResponse> Handle(UpdateBureauRequest request, CancellationToken cancellationToken)
        {
            return _bureauRepository.UpdateBureau(request);
        }
    }
}
