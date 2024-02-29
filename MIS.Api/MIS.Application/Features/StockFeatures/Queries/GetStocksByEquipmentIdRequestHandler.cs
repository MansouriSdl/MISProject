using MediatR;
using MIS.Application.Models.Responses;
using MIS.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIS.Application.Features.StockFeatures.Queries
{
    public class GetStocksByEquipmentIdRequestHandler : IRequestHandler<GetStocksByEquipmentIdRequest, IEnumerable<NameResponse>>
    {

        private readonly IStockRepository _stockRepository;

        public GetStocksByEquipmentIdRequestHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Task<IEnumerable<NameResponse>> Handle(GetStocksByEquipmentIdRequest request, CancellationToken cancellationToken)
        {
            return _stockRepository.GetStocksByEquipmentId(request.EquipmentId);
        }
    }
}
