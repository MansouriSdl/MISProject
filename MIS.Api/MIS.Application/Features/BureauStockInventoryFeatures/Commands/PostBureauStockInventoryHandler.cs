using AutoMapper;
using MediatR;
using MIS.Application.Models.Requests;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockInventoryFeatures.Commands
{
    public class PostBureauStockInventoryHandler : IRequestHandler<PostBureauStockInventory, BureauStockInventoryResponse>
    {

        private readonly IBureauStockInventoryRepository _bureauEquipmentAssignmentRepository;

        public PostBureauStockInventoryHandler(IBureauStockInventoryRepository bureauEquipmentAssignmentRepository)
        {
            _bureauEquipmentAssignmentRepository = bureauEquipmentAssignmentRepository;
        }

        public async Task<BureauStockInventoryResponse> Handle(PostBureauStockInventory request, CancellationToken cancellationToken)
        {
            var postBureauEquipmentAssignmentRequest = new PostBureauStockInventoryRequest()
            {
                UserId = request.UserId,
                AssignmentDate = request.InventoryDate,
                StockId = request.StockId,
                IsCanceled = request.IsAvailable,
                State = request.State,
                QrCode = request.QrCode
            };
            return await _bureauEquipmentAssignmentRepository.PostBureauEquipmentAssignment(postBureauEquipmentAssignmentRequest);
        }

    }
}
