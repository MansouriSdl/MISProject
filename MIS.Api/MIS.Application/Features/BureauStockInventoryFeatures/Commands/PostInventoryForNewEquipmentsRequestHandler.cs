using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.BureauStockInventoryFeatures.Commands
{
    public class PostInventoryForNewEquipmentsRequestHandler : IRequestHandler<PostInventoryForNewEquipmentsRequest, BureauStockInventoryResponse>
    {
        private readonly IBureauStockInventoryRepository _bureauStockInventoryRepository;

        public PostInventoryForNewEquipmentsRequestHandler(IBureauStockInventoryRepository bureauStockInventoryRepository)
        {
            _bureauStockInventoryRepository = bureauStockInventoryRepository;
        }

        public Task<BureauStockInventoryResponse> Handle(PostInventoryForNewEquipmentsRequest request, CancellationToken cancellationToken)
        {
            return _bureauStockInventoryRepository.PostInventoryForNewEquipments(request);
        }
    }
}
